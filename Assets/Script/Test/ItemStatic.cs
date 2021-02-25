using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStatic : ItemBase
{
    public string itemName;
    public bool isPoisoned;
    //public bool exposion;
    public bool isAvailable;
    public bool switchflag;

    private Animator anim;
    private float lastTime;

    //public BoxCollider2D coll2D;
    //public float waitTime;
    //public float lastTime;
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        isPoisoned = false;
        //exposion = false;
        isAvailable = true;
        switchflag = false;
        lastTime = GameObject.Find("NonTargetAI (1)").GetComponent<PatrolAI>().machineWaitTimer;

        if (GetComponent<Animator>() != null)
        {
            anim = GetComponent<Animator>();
        }
        //coll2D = GetComponent<BoxCollider2D>();
        //coll2D.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (switchflag == true)
        {
            StartCoroutine(Switch());
        }
    }

    
    IEnumerator Switch()
    {
        yield return new WaitForEndOfFrame();
        switchflag = false;
    }

    public void FBIOpenTheDoor()
    {
        gameObject.GetComponentInChildren<OpenDoor>().KeepDoorOpen();
        isAvailable = false;
        //Debug.Log("Kere");
    }

    public void DrinkerBreaker()
    {
        anim.SetTrigger("Break");
        StartCoroutine(Broken());
        //gameObject.GetComponentInChildren<>
        isAvailable = false;
    }


    public void DrinkerPoisoned()
    {
        isPoisoned = true;
        anim.SetTrigger("Poison");
        anim.SetBool("IsPoisoned", true);
    }

    public void ActiveMachine()
    {
        anim.SetBool("Active", true);
        //Debug.Log("Kere");
        StartCoroutine(DisableMachine());
    }

    IEnumerator DisableMachine()
    {
        yield return new WaitForSeconds(lastTime);
        anim.SetBool("Active", false);
    }

 
    IEnumerator Broken()
    {
        yield return new WaitForSeconds(0.59f);
        anim.SetBool("IsBroken", true);
    }
}
