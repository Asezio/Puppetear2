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
    public float machineWaitTimer;
    private float machineWaitMaxTime;

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

        machineWaitMaxTime = machineWaitTimer;
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
                anim.SetBool("isIdle", true);
                foreach (var thing in hitThing)
                {

                    if (thing.GetComponent<Machine1>() != null)
                    {
                        if (thing.GetComponent<Machine1>().isActive == true)
                        {
                            state = State.MoveToMachine;
                            anim.SetBool("isIdle", false);
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
                    
                    state = State.Moving;
                    anim.SetBool("isIdle", false);
                }
                break;

            case State.Moving:
                anim.SetBool("isWalking", true);
                isWalking = true;

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
                        // Go to next waypoint
                        waitTimer = waitTimeList[wayPointIndex];
                        wayPointIndex = (wayPointIndex + 1) % waypointList.Length;
                        anim.SetBool("isWalking", false);
                        isWalking = false;
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
                        Vector3 machDir = (thing.transform.position - transform.position).normalized;
                        lastMoveDir = machDir;
                        transform.position = transform.position + machDir * speed * Time.deltaTime;
                        float distanceAfter = Vector3.Distance(transform.position, thing.transform.position);
                        if (distanceAfter < machineOffset)
                        {
                            thing.GetComponent<Machine1>().isActive = false;
                            state = State.WaitMachine;
                            anim.SetBool("isWalking", false);
                            isWalking = false;
                        }
                    }
                }
                break;

            case State.WaitMachine:
                anim.SetBool("isIdle", true);
                machineWaitTimer -= Time.deltaTime;

                if (machineWaitTimer <= 0)
                {
                    machineWaitTimer = machineWaitMaxTime;
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
