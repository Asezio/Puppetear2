using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exposion : MonoBehaviour
{
    public BoxCollider2D coll2D;
    public float waitTime;
    public float lastTime;
    //public bool exposion;
    // Start is called before the first frame update
    void Awake()
    {
        coll2D = GetComponent<BoxCollider2D>();
        coll2D.enabled = false;
        //exposion = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponentInParent<ItemStatic>().switchflag == true)
        {
            //Debug.Log("here");
            Exposion1();
        }
    }

    public void Exposion1()
    {
        Debug.Log("Exposion");
        StartCoroutine(StartExposion());       
    }

    IEnumerator disableHitBox()
    {
        yield return new WaitForSeconds(lastTime);
        coll2D.enabled = false;
    }

    IEnumerator StartExposion()
    {
        yield return new WaitForSeconds(waitTime);
        coll2D.enabled = true;
        StartCoroutine(disableHitBox());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyBase>().Die();
            Debug.Log("Succeed");
        }
    }
}
