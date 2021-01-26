﻿using System.Collections;
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

    public FOV fieldOfView;

    private float waitTimer;
    private Vector3 lastMoveDir;

    private enum State
    {
        Waiting,
        Moving,
        Drinking,
    }

    private State state;
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
        switch (state)
        {
            case State.Waiting:

                Collider2D[] hitThing = Physics2D.OverlapCircleAll(transform.position, detectRange, thingLayers);

                foreach (var thing in hitThing)
                {
                    if (thing.gameObject.tag == "drinker")
                    {
                        Debug.Log("hit drinker");
                        // drink animation
                        fieldOfView.gameObject.SetActive(false);
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
