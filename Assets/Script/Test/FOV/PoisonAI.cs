using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonAI : AIBase
{
    [Header("Waypoints Setting")]
    [SerializeField] private Vector3[] waypointList;
    [SerializeField] private float[] waitTimeList;
    private float waitTimer;
    private int wayPointIndex;

    [Header("Detect Setting")]
    [SerializeField] private float detectRange;
    [SerializeField] private LayerMask thingLayers;

    [Header("DrinkTime")]
    [SerializeField] private float drinkTimer;
    private float drinkMaxTime;

    [Header("State")]
    public State state;

    private Drinker drinker;


    public enum State
    {
        Waiting,
        Moving,
        Drinking,
        Poisoned,
    }



    protected override void Start()
    {
        base.Start();

        if (waitTimeList.Length != 0)
        {
            waitTimer = waitTimeList[0];
        }

        drinkMaxTime = drinkTimer;

    }

    protected override void Update()
    {
        base.Update();

        HandleMovement();

    }

    private void HandleMovement()
    {
        // Collider2D[] hitThing = Physics2D.OverlapCircleAll(transform.position, detectRange, thingLayers);
        switch (state)
        {
            case State.Waiting:

                anim.SetBool("isIdle", true);

                Collider2D[] hitThing = Physics2D.OverlapCircleAll(transform.position, detectRange, thingLayers);
                foreach (var thing in hitThing)
                {

                    //if there's a drinker, stop and drink water.
                    if (thing.GetComponent<Drinker>() != null)
                    {
                        drinker = thing.GetComponent<Drinker>();

                        if (drinker.GetComponent<ItemStatic>().isPoisoned == false)
                        {
                            state = State.Drinking;
                        }
                        else if (drinker.GetComponent<ItemStatic>().isPoisoned == true)
                        {
                            state = State.Poisoned;
                        }
                    }
                }
                waitTimer -= Time.deltaTime;

                if (waitTimer <= 0f)
                {
                    anim.SetBool("isIdle", false);
                    state = State.Moving;
                }
                break;

            case State.Moving:

                isWalking = true;
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
                        isWalking = false;
                        state = State.Waiting;
                    }
                }
                break;

            case State.Drinking:
                if (fieldOfView != null)
                {
                    fieldOfView.gameObject.SetActive(false);
                }


                //喝水动画
                drinkTimer -= Time.deltaTime;

                if (drinkTimer <= 0)
                {
                    fieldOfView.gameObject.SetActive(true);
                    drinkTimer = drinkMaxTime;
                    state = State.Moving;

                }
                break;

            case State.Poisoned:
                //喝水动画
                if (fieldOfView != null)
                {
                    fieldOfView.gameObject.SetActive(false);
                }

                drinkTimer -= Time.deltaTime;

                if (drinkTimer <= 0)
                {
                    drinkTimer = drinkMaxTime;

                    TaskTarget.poisonFinAmount++;
                    Debug.Log(drinker.GetComponent<ItemStatic>().isPoisoned);
                    GetComponent<EnemyBase>().Die();
                }
                break;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }

}

