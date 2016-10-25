using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AbilityManager))]
public class AIEnemy : MonoBehaviour
{
    public enum EnemyType
    {
        None,
        Melee,
        Ranged
    }
    public enum State
    {
        Idle,
        Chasing,
        Attacking,
        Fleeing,
        Healing
    };

    public State state;
    public EnemyType enemyType;

    private CharacterController controller;
    private AbilityManager abilityManager;
    private Transform playerTransform;

    //Used to limit how often rotation is changed, for fewer calls and less erratic running.
    //---
    private bool directionOnCooldown;
    private float cooldownTimer;
    //---

    // Note: It would be cleaner code to have Heal as a seperate script.
    private float walkSpeed = 3f;
    private float runSpeed = 7f;
    private float turnSpeed = 5f;
    private float rotationMargin = 1f;

    [Range(20f, 100f)]
    public float chaseDistance = 50f;
    private float attackDistance;
    //Buffer to movement; mob will move a slight bit closer than chase distance, allowing the mob to attack even if the player makes minor movement.
    private float wiggleDistance;

    private int maxHealth = 20;
    private int currentHealth;
    private int lowHealth = 10;

    private float healCooldown = 1f;
    private float healTimer = 1f;

    private Vector3 runPosition;

    //public var
    public float enemyDamage = 5f;
    [Range(0f, 2f)]
    public float enemyCooldown = 2f;
    public float enemySpeed = 5f;
    public float enemyAttackRange = 2f;

    void Awake()
    {
        //Variables.
        healTimer = 1f;
        healCooldown = 1f;
        directionOnCooldown = false;
        currentHealth = maxHealth;
        state = State.Idle;
        attackDistance = enemyAttackRange;

        if (enemyType == EnemyType.Melee)
        {
            var ability = transform.gameObject.AddComponent<MeleeAbility>();
            ability.damage = enemyDamage;
            ability.cooldown = enemyCooldown;
            ability.range = enemyAttackRange;
        }
        else if (enemyType == EnemyType.Ranged) {
            var ability = transform.gameObject.AddComponent<FireballAbility>();
            ability.damage = enemyDamage;
            ability.cooldown = enemyCooldown;
            ability.speed = enemySpeed;
            ability.range = enemyAttackRange;
            ability.prefab = Resources.Load("Fireball") as GameObject;
        }
        else
        {
            enemyType = EnemyType.None;
            attackDistance = 0;
        }

        wiggleDistance = attackDistance / 4;

        //Components
        controller = GetComponent<CharacterController>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        abilityManager = GetComponent<AbilityManager>();
    }

    void Update()
    {
        Vector3 playerPosition = playerTransform.position;

        // [Flee] If within the player's radius and low on health.
        if (Vector3.Distance(transform.position, playerTransform.position) <= chaseDistance && currentHealth <= lowHealth)
        {
            state = State.Fleeing;
        }
        // [Heal] If outside the player's radius and under max health.
        else if (Vector3.Distance(transform.position, playerTransform.position) > chaseDistance && currentHealth < maxHealth)
        {
            state = State.Healing;
        }
        else if (currentHealth > lowHealth)
        {
            if (Vector3.Distance(transform.position, playerPosition) < chaseDistance)
            {
                // [Chase] If within the player's radius and not low on health.
                if (Vector3.Distance(transform.position, playerPosition) > attackDistance && state == State.Attacking)
                {
                    state = State.Chasing;
                }
                else if (Vector3.Distance(transform.position, playerPosition) > (attackDistance - wiggleDistance))
                {
                    //Allow some wiggle room to continue attacking the player, even if it starts moving.
                    if (state != State.Attacking)
                    {
                        state = State.Chasing;
                    }
                }
                // [Attack] If close to the player.
                else
                {
                    state = State.Attacking;
                }
            }
            // [Idle] When there's nothing else to do.
            else
            {
                state = State.Idle;
            }
        }
        PerformAction();
    }

    private void PerformAction()
    {
        if (state == State.Idle)
        {
            //Idle logic.
        }
        else if (state == State.Chasing)
        {
            //Chasing logic.
            RotateTowards(playerTransform.position);
            controller.SimpleMove(transform.forward * walkSpeed);
        }
        else if (state == State.Fleeing)
        {
            //Fleeing logic.
            RotateAwayFrom(runPosition);
            controller.SimpleMove(transform.forward * runSpeed);
        }
        else if (state == State.Attacking)
        {
            //Attacking logic.
            RotateTowards(playerTransform.position);
            abilityManager.attack(playerTransform.gameObject);
            Debug.Log("Attacking.");

        }
        else if (state == State.Healing)
        {
            if (Time.time > healTimer && state == State.Healing)
            {
                currentHealth++;
                healTimer = Time.time + healCooldown;
            }
            if (currentHealth == maxHealth)
            {
                state = State.Idle;
            }
        }
    }

    private void RotateAwayFrom(Vector3 position)
    {
        if (!directionOnCooldown)
        {
            directionOnCooldown = true;
            cooldownTimer = Time.deltaTime + 2f;
            Vector3 facing = position - transform.position;

            //if (facing.magnitude < rotationMargin) { return; }

            // Rotate AWAY from the player...
            Quaternion awayRotation = Quaternion.LookRotation(facing);
            Vector3 euler = awayRotation.eulerAngles;
            euler.y -= 180 + Random.Range(-10, 11);
            //euler.y = Random.Range(0, 361);
            awayRotation = Quaternion.Euler(euler);

            // Rotate the game object.
            transform.rotation = Quaternion.Slerp(transform.rotation, awayRotation, turnSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
        else if (directionOnCooldown)
        {
            if (cooldownTimer < Time.deltaTime)
                ;
            {
                directionOnCooldown = false;
            }
        }
    }

    private void RotateTowards(Vector3 position)
    {
        if (!directionOnCooldown)
        {
            directionOnCooldown = true;
            cooldownTimer = Time.deltaTime + 2f;
            Vector3 facing = position - transform.position;
            if (facing.magnitude < rotationMargin)
            {
                return;
            }

            // Rotate TOWARDS from the player...
            Quaternion awayRotation = Quaternion.LookRotation(facing);
            Vector3 euler = awayRotation.eulerAngles;
            awayRotation = Quaternion.Euler(euler);

            // Rotate the game object.
            transform.rotation = Quaternion.Slerp(transform.rotation, awayRotation, turnSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
        else if (directionOnCooldown)
        {
            if (cooldownTimer < Time.deltaTime)
                ;
            {
                directionOnCooldown = false;
            }
        }

    }
}
