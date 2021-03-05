using System.Collections;
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
        SoundManager.instance.PlaySound("attack npc");
        anim.SetTrigger("isDead");
        GetComponent<CapsuleCollider2D>().enabled = false;
        //SoundManager.instance.PlaySound("Hit for enemy 2"); 
        if (GameObject.Find("KillStage") != null)
        {
            GameObject.Find("KillStage").GetComponent<KillStage>().FirstKillAI();
        }
        GetComponent<AIBase>().enabled = false;
        Destroy(gameObject, 1.1f);
        if (GetComponent<AIBase>().fieldOfView != null)
        {
            Destroy(GetComponent<AIBase>().fieldOfView.gameObject, 0.2f);
        }
       
        int addPoints = Random.Range(150, 201);
        player.GetComponent<Points>().AddPoints(addPoints);
        if(gameObject.GetComponent<SleepyAI>() == null && gameObject.GetComponent<Boss>() == null && gameObject.GetComponent<PoisonAI>() == null)
        {
            gameObject.GetComponent<PatrolAI>().enabled = false;
        }
        else if(gameObject.GetComponent<Boss>() != null)
        {
            gameObject.GetComponent<Boss>().enabled = false;
        }
        else if (gameObject.GetComponent<PoisonAI>() != null)
        {
            gameObject.GetComponent<PoisonAI>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<SleepyAI>().enabled = false;
        }
    }

    public void Hurt()
    {
        StartCoroutine(BossHurt());
        SoundManager.instance.PlaySound("attack npc");
    }
    IEnumerator BossHurt()
    {
        Boss boss = bossG.GetComponent<Boss>();
        Boss.isChanged = true;
        boss.speed = 0;
        boss.GetComponent<Animator>().SetTrigger("isHit");
        boss.GetComponent<Rigidbody2D>().Sleep();
        boss.GetComponent<CapsuleCollider2D>().enabled = false;
        boss.fieldOfView.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        Boss.health--;
        if (Boss.health == 2)
        {
            bossG.transform.position = boss.waypointList2[0];
            boss.speed = 2;
        }
        else if (Boss.health == 1)
        {
            bossG.transform.position = boss.waypointList3[0];
            boss.speed = 2;
        }
        boss.GetComponent<Rigidbody2D>().WakeUp();
        boss.fieldOfView.gameObject.SetActive(true);
        boss.GetComponent<CapsuleCollider2D>().enabled = true;
    }

    IEnumerator TimeRefresh()
    {
        Debug.Log("1");
        UITimeBar.timeLeft = GameObject.Find("TimeLeft").GetComponent<UITimeBar>().timeMax;
        yield return new WaitForSeconds(waitTime);
        Debug.Log("2");

    }

}
