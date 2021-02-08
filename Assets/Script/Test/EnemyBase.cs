using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float speed;
    protected SpriteRenderer sr;
    public float waitTime;
    private GameObject player;

    // Start is called before the first frame update
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
    }


    public void Die()
    {
        UITimeBar.timeLeft = GameObject.Find("TimeLeft").GetComponent<UITimeBar>().timeMax;
        Destroy(gameObject,0.2f);
        Destroy(GetComponent<AIBase>().fieldOfView.gameObject,0.2f);
        int addPoints = Random.Range(150, 201);
        player.GetComponent<Points>().AddPoints(addPoints);
    }

    public void Hurt()
    {
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
