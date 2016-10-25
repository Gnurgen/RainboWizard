using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AbilityManager))]
public class AIBoss : MonoBehaviour
{
    public enum BossAbilities
    {
        None,
        One,
        Two,
        Three
    }
    public enum State
    {
        Idle,
        Chasing,
        Attacking,
        Healing
    };

    public State state;
    public BossAbilities bossAbilities;

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
    public float chaseDistance = 60f;
    private float attackDistance;
    //Buffer to movement; mob will move a slight bit closer than chase distance, allowing the mob to attack even if the player makes minor movement.
    private float wiggleDistance;

    private int maxHealth = 20;
    private int currentHealth;
    private int lowHealth = 10;

    private float healCooldown = 1f;
    private float healTimer = 1f;

    private Vector3 runPosition;

    private FireballAbility fireball;
    private MassFireballAbility massFireball;
    private IceMineAbility icemine;
    private TeleportAbility teleport;

    //public var
    public float bossDamage = 10f;
    [Range(0f, 2f)]
    public float bossCooldown = 1f;
    public float bossSpeed = 10f;
    public float bossAoE = 2f;

    void Awake()
    {
        //Variables.
        healTimer = 1f;
        healCooldown = 1f;
        directionOnCooldown = false;
        currentHealth = maxHealth;
        state = State.Idle;

        if (bossAbilities == BossAbilities.One)
        {
            attackDistance = chaseDistance / 4;

            massFireball = transform.gameObject.AddComponent<MassFireballAbility>();
            massFireball.damage = bossDamage * 5f;
            massFireball.cooldown = bossCooldown;
            massFireball.speed = bossSpeed;
            massFireball.range = attackDistance;
            massFireball.prefab = Resources.Load("Fireball") as GameObject;
        }
        else if (bossAbilities == BossAbilities.Two)
        {
            attackDistance = chaseDistance/4;

            massFireball = transform.gameObject.AddComponent<MassFireballAbility>();
            massFireball.damage = bossDamage * 5f;
            massFireball.cooldown = bossCooldown;
            massFireball.speed = bossSpeed;
            massFireball.range = attackDistance;
            massFireball.prefab = Resources.Load("Fireball") as GameObject;

            icemine = transform.gameObject.AddComponent<IceMineAbility>();
            icemine.damage = bossDamage;
            icemine.cooldown = bossCooldown;
            icemine.delay = bossSpeed;
            icemine.explosionRange = bossAoE;
            icemine.range = attackDistance;
            icemine.minePrefab = Resources.Load("Mine") as GameObject;
        }
        else if (bossAbilities == BossAbilities.Three)
        {
            attackDistance = chaseDistance / 4;

            massFireball = transform.gameObject.AddComponent<MassFireballAbility>();
            massFireball.damage = bossDamage * 5f;
            massFireball.cooldown = bossCooldown;
            massFireball.speed = bossSpeed;
            massFireball.range = attackDistance;
            massFireball.prefab = Resources.Load("Fireball") as GameObject;

            icemine = transform.gameObject.AddComponent<IceMineAbility>();
            icemine.damage = bossDamage;
            icemine.cooldown = bossCooldown;
            icemine.delay = bossSpeed;
            icemine.explosionRange = bossAoE;
            icemine.range = attackDistance;
            icemine.minePrefab = Resources.Load("Mine") as GameObject;

            teleport = transform.gameObject.AddComponent<TeleportAbility>();
            teleport.cooldown = 1f;
            teleport.range = attackDistance;
        }
        else
        {
            bossAbilities = BossAbilities.None;
            attackDistance = 0;
        }

        fireball = transform.gameObject.AddComponent<FireballAbility>();
        fireball.damage = bossDamage;
        fireball.cooldown = bossCooldown;
        fireball.speed = bossSpeed;
        fireball.prefab = Resources.Load("Fireball") as GameObject;
        fireball.range = attackDistance;

        wiggleDistance = attackDistance / 3;

        //Components
        controller = GetComponent<CharacterController>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        abilityManager = GetComponent<AbilityManager>();
    }

    void Update()
    {
        Vector3 playerPosition = playerTransform.position;

        // [Heal] If outside the player's radius and under max health.
        if (Vector3.Distance(transform.position, playerTransform.position) > chaseDistance && currentHealth < maxHealth)
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
            if (teleport != null)
            {
                if(Vector3.Distance(transform.position, playerTransform.position) > (teleport.range + attackDistance))
                {
                    teleport.UseAbility(playerTransform.position);
                }
            }
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
