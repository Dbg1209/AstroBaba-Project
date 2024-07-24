using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMechaController : MonoBehaviour
{
    //Enemy Movement

    public NavMeshAgent navMeshAgent;
    public float startWaitTime = 4;
    public float timeToRotate = 2;
    public float speedWalk = 3;
    public float speedRun = 5;

    //Enemy Animation

    public Animator animator;
    
   
    //Enemy View Radious
    public float viewRadius = 15;
    public float viewAngle = 90;
    public LayerMask playerMask;
    public LayerMask obstacleMask;
    public float meshResolution = 1f;
    public int edgeIterations = 4; 
    public float edgeDistance = 0.5f;

    //Waypoints
    public Transform[] waypoints;
    int m_currentWaypointIndex;

    //Player Position
    Vector3 playerLastPosition = Vector3.zero;
    Vector3 m_playerPosition;

    //Variables to check
    public float m_waitTime;
    float m_TimeToRotate; 
    bool m_playerInRange;
    bool m_playeNear;
    bool m_isPatrol;
    bool m_caughtPlayer;


    void Start()
    {
        m_playerPosition = Vector3.zero;
        m_isPatrol = true;
        m_caughtPlayer = false;
        m_playerInRange = false;
        m_waitTime = startWaitTime;
        m_TimeToRotate = timeToRotate;

        animator = GetComponent<Animator>();

        m_currentWaypointIndex = 0;
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speedWalk;
        navMeshAgent.SetDestination(waypoints[m_currentWaypointIndex].position);
    }

    // Update is called once per frame
    void Update()
    {
        EnviromentView();
        if (!m_isPatrol)
        {
            Chasing();
        }
        else
        {
            Patroling();
        }
    }

    private void Chasing()
    {
        m_playeNear = false;
        playerLastPosition = Vector3.zero; 
        Debug.Log("Just Started Chasing");

        if(!m_caughtPlayer)
        {
            Move(speedRun);
            navMeshAgent.SetDestination(m_playerPosition);
            Debug.Log("First if, Run to player");
        }
        else // (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            if(m_waitTime <= 0 && !m_caughtPlayer && Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 6f) 
            {
                m_isPatrol =true;
                m_playeNear = false;
                Move(speedWalk);
                m_TimeToRotate = timeToRotate;
                m_waitTime = startWaitTime;
                navMeshAgent.SetDestination(waypoints[m_currentWaypointIndex].position);

                Debug.Log("Back to Patrol");
            }
            else
            {
                if(Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 2.5f)
                {
                    Stop();
                    m_waitTime -= Time.deltaTime;
                    Debug.Log("Should Stop and back to Patrol");

                }
            }
        }
    }
    private void Patroling()
    {
        if (m_playeNear)
        {
            if (m_TimeToRotate <= 0)
            {
                Move(speedWalk);
                LookingPlayer(playerLastPosition);
                animator.SetBool("isMoving", true);
            }
            else
            {
                Stop();
                m_TimeToRotate -= Time.deltaTime;
                animator.SetBool("isMoving", false);  
            }
        }
        else
        {
            m_playeNear = false;
            playerLastPosition = Vector3.zero;
            navMeshAgent.SetDestination(waypoints[m_currentWaypointIndex].position);
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if(m_waitTime <= 0)
                {
                    NextPoint();
                    Move(speedWalk);
                    m_waitTime = startWaitTime;
                    
                    animator.SetBool("isMoving", true);
                }
                else 
                {
                    Stop();
                    m_waitTime -= Time.deltaTime;
                    
                    animator.SetBool("isMoving", false);   
                }
            }
        }
    }
    void Move (float speed)
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speed;
        Debug.Log("Move");
        animator.SetBool("isMoving", true);
    }

    void Stop()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;
    }

    public void NextPoint()
    {
        m_currentWaypointIndex = (m_currentWaypointIndex + 1) % waypoints.Length;
        navMeshAgent.SetDestination(waypoints[m_currentWaypointIndex].position);
        animator.SetBool("isMoving", true);
        Debug.Log("Next Point");
    }
    void CaughtPlayer()
    {
        m_caughtPlayer = true;
    }

    void LookingPlayer (Vector3 player)
    {
        navMeshAgent.SetDestination(player);
        if(Vector3.Distance(transform.position, player) <= 0.3)
        {   
            if(m_waitTime <= 0)
            {
                m_playeNear = false;
                Move(speedWalk);
                navMeshAgent.SetDestination(waypoints[m_currentWaypointIndex].position);
                m_waitTime = startWaitTime;
                m_TimeToRotate = timeToRotate;
            }
            else
            {
                Stop();
                m_waitTime -= Time.deltaTime;
            }
        }
    }

    void EnviromentView()
    {
         Collider [] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);

        for (int i = 0; i < playerInRange.Length; i++)
        {
            Transform player = playerInRange[i].transform;
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward, dirToPlayer) < viewAngle /2 )
            {
                float dstToPlayer = Vector3.Distance(transform.position, player.position);
                if(!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleMask))
                {
                    m_playerInRange = true;
                    m_isPatrol = false;
                }
                else
                {
                    m_playerInRange = false;
                    m_isPatrol = true;
                }
            }
            if (Vector3.Distance(transform.position, player.position) > viewRadius)
            {
                m_playerInRange = false;
            }
            if (m_playerInRange)
            {
                m_playerPosition = player.transform.position;
            }
        }
    }
    
}
