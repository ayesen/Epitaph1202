using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    //Attempt One With Random Points
    public float aiRadius;
    public float aiTimer;
    private Transform targetPos;
    private float timer;
    
    //Attempt Two With Set Patrol Points
    public Transform[] points;
    private int destPoint = 0;
    private int randPoint = 0;
    private NavMeshAgent aiAgent;
    public Transform[] spiderPoints;
    public bool isSpider;
    public bool isPatrol;
    public static Transform selfPos;
    public bool randPatrol;
    public bool planPatrol;
    void Start()
    {
        selfPos = GetComponent<Transform>();
        timer = aiTimer;
        aiAgent = GetComponent<NavMeshAgent>();
        aiAgent.autoBraking = false;
        if (isPatrol)
        {
            GotoNextPoint();
        }

    }

    void Update()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.

        if (isSpider)
        {
            timer += Time.deltaTime;
            if (timer >= aiTimer)
            {
                Vector3 newPos = RandomNavSphere(transform.position, aiRadius, -1);
                aiAgent.SetDestination(newPos);
                timer = 0;
            }
            if (isPatrol)
            {
                if (!aiAgent.pathPending && aiAgent.remainingDistance < 0.5f)
                {
                    GotoNextPoint();
                }
            }
        }
    }

    public void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        if (planPatrol)
        {
            aiAgent.destination = points[destPoint].position;
            print("Heading to point: " + destPoint);
            destPoint = (destPoint + 1) % points.Length;
        }
        else if (randPatrol)
        {
            aiAgent.destination = points[randPoint].position;
            print("Heading to point: " + randPoint);
            randPoint = Random.Range(0, 10);
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }
    
    public static Vector3 SpiderGo(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Vector3.zero;
        randDirection += origin;
        NavMeshHit navHit;
        int spiderDirection = Random.Range(0, 2);
        if (spiderDirection == 0)
        {
            print("Go Horizontal");
            randDirection = new Vector3(Random.Range(-5f, 5f), selfPos.position.y, selfPos.position.z) * dist;
            NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
            
        }
        else
        {
            print("Go Vertical");
            randDirection = new Vector3(selfPos.position.x, selfPos.position.y, Random.Range(-5f, 5f)) *  dist;
            NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
            
        }
        return navHit.position;
    }
}
