using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTrap : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    // Update is called once per frame

    void Awake()
    {
        player = GameObject.Find("Chara_Player");
    }
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
            Debug.Log("1");
            if (other.GetComponent<Boss>()==null)
            {
                other.GetComponent<EnemyBase>().Die();
            }
            else
            {
                if(Boss.health > 1f)
                {
                    Debug.Log("2");
                    other.GetComponent<EnemyBase>().Hurt();
                    GameObject.Find("TimeLeft").GetComponent<UITimeBar>().Refresh();
                    int addPoints = Random.Range(150, 201);
                    player.GetComponent<Points>().AddPoints(addPoints);
                }
                else
                {
                    Boss.health--;
                    TaskTarget.bossFinAmount++;
                    other.GetComponent<EnemyBase>().Die();
                }
            }
            GetComponent<ItemMoveable>().Destroy();
            //GetComponent<MouseTrap>().enabled = false;

        }
        
    }
}
