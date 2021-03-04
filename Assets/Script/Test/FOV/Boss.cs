using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : AIBase
{
    [Header("Waypoints1 Setting")]
    [SerializeField] private Vector3[] waypointList1;
    [SerializeField] private float[] waitTimeList1;

    [Header("Waypoints2 Setting")]
    public Vector3[] waypointList2;
    [SerializeField] private float[] waitTimeList2;

    [Header("Waypoints3 Setting")]
    public Vector3[] waypointList3;
    [SerializeField] private float[] waitTimeList3;

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

    [SerializeField]private float waitTimer;
    private int wayPointIndex;

    public static bool isChanged = true;

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
        health = 3;

    }

    protected override void Update()
    {
        base.Update();

        if (health == 3)
        {
            anim.SetInteger("health", 3);
            if (isChanged == true)
            {
                //anim.SetTrigger("isHit");
                wayPointIndex = 0;
                if (waitTimeList1.Length != 0)
                {
                    waitTimer = waitTimeList1[0];
                }
            }
            isChanged = false;
            HandleMovement(waypointList1, waitTimeList1);
        }

        if (health == 2)
        {
            anim.SetInteger("health", 2);
            if (isChanged == true)
            {
                
                wayPointIndex = 0;
                if (waitTimeList2.Length != 0)
                {
                    waitTimer = waitTimeList2[0];
                }
            }
            isChanged = false;

            HandleMovement(waypointList2, waitTimeList2);
        }

        if (health == 1)
        {
            anim.SetInteger("health", 1);
            if (isChanged == true)
            {
                wayPointIndex = 0;
                if (waitTimeList3.Length != 0)
                {
                    waitTimer = waitTimeList3[0];
                }
            }
            isChanged = false;
            HandleMovement(waypointList3, waitTimeList3);
        }

        if (health == 0)
        {
            TaskTarget.bossFinAmount++;
        }
    }


    private void HandleMovement(Vector3[] waypointList, float[] waitTimeList)
    {
        Collider2D[] hitThing = Physics2D.OverlapCircleAll(transform.position, detectRange, thingLayers);
        switch (state)
        {
            case State.Waiting:
                anim.SetBool("isIdle", true);
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
                waitTimer -= Time.deltaTime;

                if (waitTimer <= 0f)
                {
                    if (fieldOfView != null)
                    {
                        fieldOfView.gameObject.SetActive(true);
                    }
                    anim.SetBool("isIdle", false);
                    state = State.Moving;
                }
                break;

            case State.Moving:
                //Debug.Log("move");
                anim.SetBool("isWalk", true);
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
                        waitTimer = waitTimeList[wayPointIndex];
                        wayPointIndex = (wayPointIndex + 1) % waypointList.Length;
                        anim.SetBool("isWalk", false);
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
                anim.SetBool("isWalk", true);
                foreach (var thing in hitThing)
                {
                    if (thing.GetComponent<Machine1>() != null)
                    {    
                        Vector3 machDir = (thing.transform.position - transform.position).normalized;
                        lastMoveDir = machDir;
                        transform.position = transform.position + machDir * speed * Time.deltaTime;
                        float distanceAfter = Vector3.Distance(transform.position, thing.transform.position);
                        if (distanceAfter < machineOffset)
                        {
                            thing.GetComponent<Machine1>().isActive = false;
                            anim.SetBool("isWalk", false);
                            state = State.WaitMachine;
                        }
                    }
                }
                break;

            case State.WaitMachine:
                anim.SetBool("isIdle", true);
                machineWaitTimer -= Time.deltaTime;

                if (machineWaitTimer <= 0)
                {
                    anim.SetBool("isIdle", false);
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
