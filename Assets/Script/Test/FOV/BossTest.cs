using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTest : AIBase
{
    [Header("Waypoints1 Setting")]
    [SerializeField] private Vector3[] waypointList1;
    [SerializeField] private float[] waitTimeList1;
    private float waitTimer1;
    private int wayPointIndex1;

    [Header("Waypoints2 Setting")]
    public Vector3[] waypointList2;
    [SerializeField] private float[] waitTimeList2;
    private float waitTimer2;
    private int wayPointIndex2;

    [Header("Waypoints3 Setting")]
    public Vector3[] waypointList3;
    [SerializeField] private float[] waitTimeList3;
    private float waitTimer3;
    private int wayPointIndex3;

    [Header("Detect Setting")]
    [SerializeField] private float detectRange;
    [SerializeField] private LayerMask thingLayers;

    [Header("Interactive Machine")]
    [SerializeField] private float machineOffset;
    [SerializeField] private float machineWaitTimer;

    [Header("State")]
    public State state;

    [Header("Health")]
    public static int health = 3;

    public enum State
    {
        Waiting,
        Moving,
        MoveToMachine,
        WaitMachine,
    }

    private void InitWaypoint(out float waittimerr, float[] waittimelist, out int waypointindex)
    {
        if (waittimelist.Length != 0)
        {
            waittimerr = waittimelist[0];
        }
        else
        {
            waittimerr = 0;
        }

        waypointindex = 0;
    }

    protected override void Start()
    {
        base.Start();

        //InitWaypoint(out waitTimer1, waitTimeList1, out wayPointIndex1);
        //InitWaypoint(out waitTimer2, waitTimeList2, out wayPointIndex2);
        //InitWaypoint(out waitTimer3, waitTimeList3, out wayPointIndex3);
        if (waitTimeList1.Length != 0)
        {
            waitTimer1 = waitTimeList1[0];
        }
        else
        {
            waitTimer1 = 0;
        }

        if (waitTimeList2.Length != 0)
        {
            waitTimer2 = waitTimeList1[0];
        }
        else
        {
            waitTimer2 = 0;
        }

        wayPointIndex2 = 0;

        if (waitTimeList3.Length != 0)
        {
            waitTimer3 = waitTimeList1[0];
        }
        else
        {
            waitTimer3 = 0;
        }

        wayPointIndex3 = 0;

    }


    protected override void Update()
    {
        base.Update();

        if (health == 3)
        {
            HandleMovement(waypointList1, waitTimeList1, waitTimer1, wayPointIndex1);
        }

        if (health == 2)
        {
            HandleMovement(waypointList2, waitTimeList2, waitTimer2, wayPointIndex2);
        }

        if (health == 1)
        {
            HandleMovement(waypointList3, waitTimeList3, waitTimer3, wayPointIndex3);
        }

        if (health == 0)
        {
            TaskTarget.bossFinAmount++;
            Die();
        }

    }

    private void HandleMovement(Vector3[] waypointList, float[] waitTimeList, float waitTimer, int wayPointIndex)
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
                        Debug.Log("hit drinker");
                        // drink animation
                        fieldOfView.gameObject.SetActive(false);

                        if (thing.GetComponent<Drinker>().isPoisoned == true)
                        {
                            TaskTarget.poisonFinAmount++;
                            Die();
                        }
                    }

                    //go and check the electric box
                    if (thing.gameObject.tag == "electricBox")
                    {
                        Debug.Log("Electric Box");
                        fieldOfView.gameObject.SetActive(false);
                    }

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
