using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTrap : MonoBehaviour
{
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        GetComponent<EdgeCollider2D>().enabled = !GetComponent<ItemMoveable>().isStick;
        //Debug.Log(GetComponent<BoxCollider2D>().enabled);
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        //Debug.Log(gameObject.name);
        if (other.tag == "Enemy")
        {
            if (other.name != "Boss")
            {
                other.GetComponent<EnemyBase>().Die();
            }
            else
            {
                if(Boss.health > 1f)
                {
                    other.GetComponent<EnemyBase>().Hurt();
                }
                else
                {
                    other.GetComponent<EnemyBase>().Die();
                }
            }
            GetComponent<ItemMoveable>().Destroy();
            //GetComponent<MouseTrap>().enabled = false;

        }
        
    }
}
