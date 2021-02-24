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
        //gameObject.GetComponentInChildren<>
        isAvailable = false;
    }
}
