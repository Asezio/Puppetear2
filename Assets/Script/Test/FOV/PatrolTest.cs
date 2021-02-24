using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolTest : MonoBehaviour
{
    [SerializeField] private float speed = 25f;
    [SerializeField] private Vector3[] waypointList;
    [SerializeField] private float[] waitTimeList;
    private int wayPointIndex;
    [SerializeField] private Transform pfFieldofView;
    [SerializeField] Vector3 aimDirection;
    [SerializeField] private Transform player;

    [SerializeField] private float fov = 90f;
    [SerializeField] private float viewDistance = 50f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float detectRange;
    [SerializeField] private LayerMask thingLayers;

    [SerializeField] private float boxOffset;
    [SerializeField] private float boxWaitTimer;

    public FOV fieldOfView;

    [SerializeField] private float waitTimer;
    private Vector3 lastMoveDir;

    public enum State
    {
        Waiting,
        Moving,
        Drinking,
        MoveToMachine,
        WaitMachine,
    }

    public State state;
    private void Start()
    {
        if (waitTimeList.Length != 0)
        {
            waitTimer = waitTimeList[0];
        }

        lastMoveDir = aimDirection;
        state = State.Waiting;

        fieldOfView = Instantiate(pfFieldofView, null).GetComponent<FOV>();
        fieldOfView.SetFoV(fov);
        fieldOfView.SetViewDistance(viewDistance);
    }

    private void Update()
    {
        HandleMovement();
        FindTargetPlayer();

        if (fieldOfView != null)
        {
            fieldOfView.SetOrigin(transform.position);
            fieldOfView.SetAimDirection(GetAimDir());
        }

        Debug.DrawLine(transform.position, transform.position + GetAimDir() * 0.5f);
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
                    if (thing.gameObject.tag == "drinker")
                    {
                        Debug.Log("hit drinker");
                        // drink animation
                        fieldOfView.gameObject.SetActive(false);
                    }

                    //go and check the electric box
                    if (thing.gameObject.tag == "electricBox")
                    {
                        Debug.Log("Electric Box");
                        fieldOfView.gameObject.SetActive(false);
                    }

                    if (thing.gameObject.tag == "machine" && thing.GetComponent<Machine>().machState == true)
                    {
                        state = State.MoveToMachine;
                        break;
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
                Debug.Log("move");

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
                }
                break;

            case State.MoveToMachine:
                foreach (var thing in hitThing)
                {
                    Debug.Log("exploringBox");
                    Vector3 boxDir = (thing.transform.position - transform.position).normalized;
                    lastMoveDir = boxDir;
                    float distanceBefore = Vector3.Distance(transform.position, thing.transform.position);
                    transform.position = transform.position + boxDir * speed * Time.deltaTime;
                    float distanceAfter = Vector3.Distance(transform.position, boxDir);
                    if (distanceAfter < boxOffset || distanceBefore <= distanceAfter)
                    {
                        //Debug.Log(boxWaitTimer);
                        if (thing.GetComponent<Machine>() != null)
                        {
                            thing.GetComponent<Machine>().machState = false;
                        }

                        state = State.WaitMachine;
                    }
                }
                break;

            case State.WaitMachine:
                boxWaitTimer -= Time.deltaTime;

                if (boxWaitTimer <= 0)
                {
                    state = State.Moving;
                }
                break;


        }
    }

    private void FindTargetPlayer()
    {
        if (Vector3.Distance(GetPosition(), player.position) < viewDistance)
        {

            // Player inside viewDistance
            Vector3 dirToPlayer = (player.position - GetPosition()).normalized;
            if (Vector3.Angle(GetAimDir(), dirToPlayer) < fov / 2f)
            {
                RaycastHit2D raycastHit2D = Physics2D.Raycast(GetPosition(), dirToPlayer, viewDistance, playerLayer);
                if (raycastHit2D.collider != null)
                {
                    Debug.Log(raycastHit2D.collider.name);
                }
            }
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public Vector3 GetAimDir()
    {
        return lastMoveDir;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}
