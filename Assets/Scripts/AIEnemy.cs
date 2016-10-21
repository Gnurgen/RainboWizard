using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class AIEnemy : MonoBehaviour
{
    public enum State
    {
        Idle,
        Chasing,
        Attacking,
        Fleeing,
        Healing
    };

    public State state;

    private CharacterController controller;
    private Transform playerTransform;

    private bool isRunning;

    //Used to limit how often rotation is changed, for fewer calls and less erratic running.
    //---
    private bool directionOnCooldown;
    private float cooldownTimer;
    //---

    private float healCooldown;
    private float healTimer;

    private float walkSpeed = 3f;
    private float runSpeed = 7f;
    private float turnSpeed = 5f;
    private float rotationMargin = 1f;

    public float chaseDistance = 20.0f;
    public float attackDistance = 2.0f;

    public int maxHealth = 20;
    public int currentHealth;
    public int lowHealth = 10;

    private Vector3 runPosition;

    void Awake()
    {
        healTimer = 1f;
        healCooldown = 1f;
        controller = GetComponent<CharacterController>();

        state = State.Idle;

        directionOnCooldown = false;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = maxHealth;
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
                if (Vector3.Distance(transform.position, playerPosition) > attackDistance)
                {
                    state = State.Chasing;
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
