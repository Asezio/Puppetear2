﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float speed;
    protected SpriteRenderer sr;
    public float waitTime;
    private GameObject player;
    private GameObject bossG;
    private Animator anim;
    private UITimeBar timeBar;
    // Start is called before the first frame update
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        bossG = GameObject.Find("Boss");
        anim = GetComponent<Animator>();
        timeBar = GameObject.Find("TimeLeft").GetComponent<UITimeBar>();
    }


    public void Die()
    {
        timeBar.Refresh();

        anim.SetTrigger("isDead");
        //SoundManager.instance.PlaySound("Hit for enemy 2"); 

        GetComponent<AIBase>().enabled = false;
        Destroy(gameObject, 1.1f);
        if (GetComponent<AIBase>().fieldOfView != null)
        {
            Destroy(GetComponent<AIBase>().fieldOfView.gameObject, 0.2f);
        }
       
        int addPoints = Random.Range(150, 201);
        player.GetComponent<Points>().AddPoints(addPoints);

    }

    public void Hurt()
    {
        Boss boss = bossG.GetComponent<Boss>();
        Boss.health--;
        Boss.isChanged = true;
        if (Boss.health == 2)
        {
            bossG.transform.position = boss.waypointList2[0];
        }
        else if (Boss.health == 1)
        {
            bossG.transform.position = boss.waypointList3[0];
        }
        UITimeBar.timeLeft = GameObject.Find("TimeLeft").GetComponent<UITimeBar>().timeMax;
        int addPoints = Random.Range(150, 201);
        player.GetComponent<Points>().AddPoints(addPoints);
    }

    IEnumerator TimeRefresh()
    {
        Debug.Log("1");
        UITimeBar.timeLeft = GameObject.Find("TimeLeft").GetComponent<UITimeBar>().timeMax;
        yield return new WaitForSeconds(waitTime);
        Debug.Log("2");

    }

}
