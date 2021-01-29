using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploringBox : MonoBehaviour
{
    public float radius = 0.5f;
    public float waitTime = 3f;
    private void Update()
    {
        Collider2D[] hitInfo = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (var hit in hitInfo)
        {
            if (hit.GetComponent<PatrolTest>() != null)
            {
                //Debug.Log("In here");
                PatrolTest patrol = hit.GetComponent<PatrolTest>();
                patrol.MoveToBox(this.transform);
                waitTime -= Time.deltaTime;

                if (waitTime <= 0)
                {
                    patrol.state = PatrolTest.State.Moving;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
