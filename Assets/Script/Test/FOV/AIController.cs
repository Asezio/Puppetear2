using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [Header("Moving Speed")]
    [SerializeField] private float speed = 25f;

    [Header("Waypoints Setting")]
    private int wayPointIndex;
    [SerializeField] private Vector3[] waypointList;
    [SerializeField] private float[] waitTimeList;
    [SerializeField] Vector3 aimDirection;
    private Vector3 lastMoveDir;
    private float waitTimer;

    [Header("FOV Setting")]
    [SerializeField] private Transform pfFieldofView;
    [SerializeField] private float fov = 90f;
    [SerializeField] private float viewDistance = 50f;
    public FOV fieldOfView;

    [Header("Player Setting")]
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask playerLayer;

    [Header("Detect Setting")]
    [SerializeField] private float detectRange;
    [SerializeField] private LayerMask thingLayers;

    [Header("Interactive Machine")]
    private Machine machine;
    [SerializeField] private float machineWaitTimer;

    [Header("State")]
    public State state;

    public enum State
    {
        Waiting,
        Moving,
        Drinking,
        MoveToMachine,
    }
    // Start is called before the first frame update
    void Start()
    {
        if (waitTimeList.Length != 0)
        {
            waitTimer = waitTimeList[0];
        }

        lastMoveDir = aimDirection;

        fieldOfView = Instantiate(pfFieldofView, null).GetComponent<FOV>();
        fieldOfView.SetFoV(fov);
        fieldOfView.SetViewDistance(viewDistance);

        //SetState(State.Moving);
    }

    // Update is called once per frame
    void Update()
    {
        //    if (fieldOfView != null)
        //    {
        //        fieldOfView.SetOrigin(transform.position);
        //        fieldOfView.SetAimDirection(GetAimDir());
        //    }

        //    FindTargetPlayer();
        LookForActiveMachine();

        //    Debug.DrawLine(transform.position, transform.position + GetAimDir() * 0.5f);
        //}

    void SetState(State newState)
    {
        state = newState;

        switch (state)
        {
            case State.Waiting:
                StartCoroutine(OnWaiting());
                break;
            case State.Moving:
                StartCoroutine(OnMoving());
                break;
            case State.MoveToMachine:
                StartCoroutine(OnMoveToMachine(machine.transform.position));
                break;
        }
    }

    IEnumerator OnWaiting()
    {
        while (state == State.Waiting)
        {
            yield return new WaitForSeconds(waitTimer);
            SetState(State.Moving);
        }
    }

    IEnumerator OnMoving()
    {
        while (state == State.Moving)
        {
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
                    SetState(State.Waiting);
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator OnMoveToMachine(Vector3 machinePosition)
    {
        while (state == State.MoveToMachine)
        {
            Vector3 moveDir = (machinePosition - transform.position).normalized;
            lastMoveDir = moveDir;
            float distanceBefore = Vector3.Distance(transform.position, machinePosition);
            transform.position = transform.position + moveDir * speed * Time.deltaTime;
            float distanceAfter = Vector3.Distance(transform.position, machinePosition);

            float arriveDistance = 0.2f;
            if (distanceAfter < arriveDistance || distanceBefore <= distanceAfter)
            {
                yield return new WaitForSeconds(machineWaitTimer);
                SetState(State.Moving);
            }
        }
    }

    //private void FindTargetPlayer()
    //{
    //    if (Vector3.Distance(GetPosition(), player.position) < viewDistance)
    //    {

    //        // Player inside viewDistance
    //        Vector3 dirToPlayer = (player.position - GetPosition()).normalized;
    //        if (Vector3.Angle(GetAimDir(), dirToPlayer) < fov / 2f)
    //        {
    //            RaycastHit2D raycastHit2D = Physics2D.Raycast(GetPosition(), dirToPlayer, viewDistance, playerLayer);
    //            if (raycastHit2D.collider != null)
    //            {
    //                Debug.Log(raycastHit2D.collider.name);
    //            }
    //        }
    //    }
    //}

     void LookForActiveMachine()
    {
        Collider2D[] surroundingColliders = Physics2D.OverlapCircleAll(transform.position, detectRange, thingLayers);
        foreach (Collider2D item in surroundingColliders)
        {
            if (item.GetComponent<Machine>() != null)
            {
                machine = item.GetComponent<Machine>();
                if (machine.machState == true)
                {
                    //StopAllCoroutines();
                    SetState(State.MoveToMachine);                
                }
            }
        }
    }

    //public Vector3 GetPosition()
    //{
    //    return transform.position;
    //}

    //public Vector3 GetAimDir()
    //{
    //    return lastMoveDir;
    //}

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
        }
}
