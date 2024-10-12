using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum GuardStates
{
    PATROL,
    INVESTIGATE,
    CHASE,
}
public class enemy : MonoBehaviour

{
    [SerializeField] Transform target;
    NavMeshAgent agent;
    [SerializeField]float speed;
    Rigidbody rb;
    NavMeshPath navPath;
    Queue<Vector3> remainingPoints;
    Vector3 currentTargetPoint;
    GuardStates state = GuardStates.PATROL;
    // Start is called before the first frame update
    void Start()
    {
        navPath = new NavMeshPath();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        remainingPoints = new Queue<Vector3>();

        

    }

    // Update is called once per frame
    void Update()
    {
        //agent.SetDestination(target.position);      
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        Vector3 forwardDirection = transform.forward;
        float dot = Vector3.Dot(forwardDirection, directionToTarget);

        if (dot > 0.5f)
        {
            Debug.Log("he's in front of me");
            state = GuardStates.CHASE;

        }
        if (dot < -0.5f)
        {
            Debug.Log("he's behind me?");
            state = GuardStates.PATROL;
        }
        
        switch (state)
        {
            case GuardStates.CHASE:

                break;
        }


        var new_forward = (currentTargetPoint - transform.position).normalized;
        new_forward.y = 0;
        transform.forward = new_forward;
        float distToPoint = Vector3.Distance(transform.position, currentTargetPoint);
        if (distToPoint < 1) 
        {
            if (remainingPoints.Count > 0)
            {
                 currentTargetPoint = remainingPoints.Dequeue();
                Findpath();
            }
            
        }
        
    }
    private void FixedUpdate()
    {
       rb.velocity = transform.forward * speed;
    }
    private void OnDrawGizmos()
    {
        if (navPath == null)
            return;
        foreach(Vector3 node in navPath.corners)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(node, 0.5f);
        }
    }
    public void UpdateChase()
    {
        
    }
    public void Findpath()
    {
        if (agent.CalculatePath(target.position, navPath))
        {
            Debug.Log("Found path");
            foreach (Vector3 p in navPath.corners)
            {
                remainingPoints.Enqueue(p);
            }

            currentTargetPoint = remainingPoints.Dequeue();
        }
    }
}
