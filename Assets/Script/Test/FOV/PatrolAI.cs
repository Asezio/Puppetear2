using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAI : AIBase
{
    [Header("Waypoints Setting")]
    [SerializeField] private Vector3[] waypointList;
    [SerializeField] private float[] waitTimeList;
    private float waitTimer;
    private int wayPointIndex;

    [Header("Detect Setting")]
    [SerializeField] private float detectRange;
    [SerializeField] private LayerMask thingLayers;

    [Header("Interactive Machine")]
    [SerializeField] private float machineOffset;
    [SerializeField] private float machineWaitTimer;

    [Header("State")]
    public State state;


    public enum State
    {
        Waiting,
        Moving,
        MoveToMachine,
        WaitMachine,
    }

    protected override void Start()
    {
        base.Start();

        if (waitTimeList.Length != 0)
        {
            waitTimer = waitTimeList[0];
        }
    }

    protected override void Update()
    {
        base.Update();

        HandleMovement();

    }

    private void HandleMovement()
    {
        Collider2D[] hitThing = Physics2D.OverlapCircleAll(transform.position, detectRange, thingLayers);
        switch (state)
        {
            case State.Waiting:
                foreach (var thing in hitThing)
                {
                    //if there's a drinker, stop and drink water.
                    if (thing.GetComponent<Drinker>() != null)
                    {
                        //Debug.Log("hit drinker");
                        // drink animation
                        fieldOfView.gameObject.SetActive(false);

                        if (thing.GetComponent<Drinker>().isPoisoned == true)
                        {
                            TaskTarget.poisonFinAmount++;
                            GetComponent<EnemyBase>().Die();
                        }
                    }

                    //go and check the electric box
                    //    if (thing.gameObject.tag == "electricBox")
                    //    {
                    //        Debug.Log("Electric Box");
                    //        fieldOfView.gameObject.SetActive(false);
                    //    }

                    if (thing.GetComponent<Machine1>() != null)
                    {
                        if (thing.GetComponent<Machine1>().isActive == true)
                        {
                            state = State.MoveToMachine;
                            break;
                        }
                    }
                }
                waitTimer -= Time.deltaTime;

                if (waitTimer <= 0f)
                {
                    fieldOfView.gameObject.SetActive(true);
                    state = State.Moving;
                }
                break;

            case State.Moving:
                //Debug.Log("move");

                if (waypointList.Length != 0)
                {
                    Vector3 waypoint = waypointList[wayPointIndex];
                    Vector3 waypointDir = (waypoint - transform.position).normalized;
                    lastMoveDir = waypointDir;
                    float distanceBefore = Vector3.Distance(transform.position, waypoint);
                    transform.position = transform.position + waypointDir * speed * Time.deltaTime;
                    float distanceAfter = Vector3.Distance(transform.position, waypoint);

                    float arriveDistance = 0.1f;
                    if (distanceAfter < arriveDistance || distanceBefore <= distanceAfter)
                    {
                        //Flip();
                        // Go to next waypoint
                        waitTimer = waitTimeList[wayPointIndex];
                        wayPointIndex = (wayPointIndex + 1) % waypointList.Length;
                        state = State.Waiting;
                    }

                    foreach (var thing in hitThing)
                    {
                        if (thing.GetComponent<Machine1>() != null)
                        {
                            if (thing.GetComponent<Machine1>().isActive == true)
                            {
                                state = State.MoveToMachine;
                                break;
                            }

                        }
                    }
                }
                break;

            case State.MoveToMachine:
                foreach (var thing in hitThing)
                {
                    if (thing.GetComponent<Machine1>() != null)
                    {
                        Debug.Log("Machine");
                        Vector3 machDir = (thing.transform.position - transform.position).normalized;
                        lastMoveDir = machDir;
                        transform.position = transform.position + machDir * speed * Time.deltaTime;
                        float distanceAfter = Vector3.Distance(transform.position, thing.transform.position);
                        if (distanceAfter < machineOffset)
                        {
                            thing.GetComponent<Machine1>().isActive = false;
                            state = State.WaitMachine;
                        }
                    }
                }
                break;

            case State.WaitMachine:
                machineWaitTimer -= Time.deltaTime;

                if (machineWaitTimer <= 0)
                {
                    state = State.Moving;
                }
                break;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }

}
