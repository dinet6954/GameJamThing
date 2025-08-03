using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent Agent;

    public Transform Player;

    public LayerMask WhatIsGround, WhatIsPlayer;

    //Patrol behaviour
    public Vector3 WalkPoint;
    bool WalkPointSet;
    public float WalkPointRange;

    //Attack behaviour
    public float TimeBetweenAttacks;
    public float Damage = 70;
    public int EquippedGun;
    bool HasBeenAttacked;

    //States
    public float SightRange, AttackRange;
    public bool PlayerInSightRange, PlayerInAttackRange;
    public FirstPersonController fps;
    public Target tgt;
    public TimeRewind Rwnd;
    public CheckAirborne Air;
    public Health heal;
    [SerializeField] float pause;
    [SerializeField] bool Countdown = true;
    int i = 0;
    bool func = true;

    void Awake()
    {
        Player = GameObject.Find("FirstPersonController").transform;
        Agent = GetComponent<NavMeshAgent>();
        fps = Player.GetComponent<FirstPersonController>();
        tgt = GetComponent<Target>();
        Rwnd = Player.GetComponent<TimeRewind>();
        Air = Player.GetComponent<CheckAirborne>();
        heal = Player.GetComponent<Health>();
        SetDamage();
    }

    void Update()
    {

        bool inSightSphere = Physics.CheckSphere(transform.position, SightRange, WhatIsPlayer);
        bool inAttackSphere = Physics.CheckSphere(transform.position, AttackRange, WhatIsPlayer);

        PlayerInSightRange = false;
        PlayerInAttackRange = false;

        if (Input.GetKeyDown(KeyCode.T))
        {
            Damage = 0;
        }

        if (Input.GetKeyUp(KeyCode.T))
        {
            SetDamage();
        }

        if (inSightSphere)
            {
                if (HasLineOfSightToPlayer(SightRange))
                {
                    PlayerInSightRange = true;
                }
            }

        if (inAttackSphere)
        {
            if (HasLineOfSightToPlayer(AttackRange))
            {
                PlayerInAttackRange = true;
            }
        }

        if (!PlayerInSightRange && !PlayerInAttackRange)
        {
            Patrolling();
        }

        else if (PlayerInSightRange && !PlayerInAttackRange)
        {
            ChasePlayer();
        }
        else if (PlayerInSightRange && PlayerInAttackRange)
        {
            Attack();
        }

        bool HasLineOfSightToPlayer(float range)
        {
            Vector3 origin = transform.position + Vector3.up * 1.6f;
            Vector3 direction = (Player.position + Vector3.up * 0.9f - origin);
            float distance = Mathf.Min(direction.magnitude, range);
            direction.Normalize();

            int mask = WhatIsGround | WhatIsPlayer;

            if (Physics.Raycast(origin, direction, out RaycastHit hit, distance, mask))
            {
                return hit.transform == Player;
            }
            return false;
        }

    }

    void FixedUpdate()
    {
        if (i == 1)
        {
            pause = pause - Time.deltaTime;

            if (pause < 0)
            {
                WalkPointSet = false;
                pause = 5;
                Countdown = true;
                i = 0;
            }
        }
    }

    void Patrolling()
    {
        if (WalkPointSet != true)
        {
            SearchWalkPoint();
        }

        if (WalkPointSet == true)
        {
            Agent.SetDestination(WalkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - WalkPoint;

        //Walkpoint Reached
        if (Countdown == true)
        {
            if (distanceToWalkPoint.magnitude < 1f)
            {
                if (func == true)
                {
                    i = 1;
                    pause = Random.Range(1, 5);
                    Countdown = false;
                }
            }
        }
    }


    void SearchWalkPoint()
    {
        float RandomZ = Random.Range(-WalkPointRange, WalkPointRange);
        float RandomX = Random.Range(-WalkPointRange, WalkPointRange);
        if (RandomZ != -1 || RandomX != -1)
        {
            WalkPoint = new Vector3(transform.position.x + RandomX, transform.position.y, transform.position.z + RandomZ);
            if (Physics.Raycast(WalkPoint, -transform.up, 2f, WhatIsGround))
            {
                WalkPointSet = true;
            }
        }
    }

    void ChasePlayer()
    {
        Agent.SetDestination(Player.position);
    }

    void Attack()
    {
        //Ensures AI doesn't move when attacking
        Agent.SetDestination(transform.position);

        transform.LookAt(Player);

        if (HasBeenAttacked != true)
        {
            //Attack Code
            int Hitchance = Random.Range(0, 100);

            if (Air.Running == true && Air.Airborne == true)
            {
                if (Hitchance <= 20)
                {
                    Debug.Log("Hit 20% chance shot");
                    heal.TakeDamage(Damage);
                }
                else
                {
                    Debug.Log("Missed 20% chance shot");
                }
            }

            if (Air.Running == true && Air.Airborne == false || Air.Running == false && Air.Airborne == true)
            {
                if (Hitchance <= 45)
                {
                    Debug.Log("Hit 45% chance shot");
                    heal.TakeDamage(Damage);
                }
                else
                {
                    Debug.Log("Missed 45% chance shot");
                }
            }

            if (Air.Running == false && Air.Airborne == false)
            {
                if (Hitchance <= 70)
                {
                    Debug.Log("Hit a 70% chance shot");
                    heal.TakeDamage(Damage);
                }
                else
                {
                    Debug.Log("Missed 75% chance shot");
                }
            }

            HasBeenAttacked = true;
            Invoke(nameof(ResetAttack), TimeBetweenAttacks);
        }
    }

    void ResetAttack()
    {
        HasBeenAttacked = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, SightRange);
    }

    public void SetDamage()
    {
        switch (EquippedGun)
        {
            case 1:
                Damage = 40;
                TimeBetweenAttacks = 2;
                break;
            case 2:
                Damage = 70;
                TimeBetweenAttacks = 4;
                break;
            case 3:
                Damage = 10;
                TimeBetweenAttacks = 0.5f;
                break;
        }
    }
}
