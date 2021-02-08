using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkerBreak : MonoBehaviour
{
    private SpriteRenderer sr;
    private PolygonCollider2D coll;

    // Start is called before the first frame update
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        coll = GetComponent<PolygonCollider2D>();
        sr.enabled = false;
        coll.enabled = false;
    }

    public void Active()
    {
        sr.enabled = true;
        coll.enabled = true;
    }

    // Update is called once per frame

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            if (other.name != "Boss")
            {
                other.GetComponent<EnemyBase>().Die();
            }
            else
            {
                if (Boss.health > 1f)
                {
                    other.GetComponent<EnemyBase>().Hurt();
                }
                else
                {
                    other.GetComponent<EnemyBase>().Die();
                }
            }
            //GetComponent<MouseTrap>().enabled = false;

        }
    }
}
