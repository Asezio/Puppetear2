using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon1 : WeaponBase
{
    //public GameObject UIweapon1;
    public override void Start()
    {
        base.Start();
        //UIweapon1 = GameObject.Find("UI_Weapon1");
    }



    // Update is called once per frame
    void Update()
    {
        DetectThing();
        Flip();

        //if (UIweapon1.GetComponent<UIweapon1>() != null)
        //{
        //    if (canAttack == true && UIweapon1.GetComponent<UIweapon1>().isReady == true)
        //    {
        //        if (Input.GetButtonDown("Interact"))
        //        {
        //            //Debug.Log("Attack");

        //            Attack();
        //        }
        //    }
        //}


        if (canAttack == true)
        {
            if (Input.GetButtonDown("Interact"))
            {
                //Debug.Log("Attack");

                Attack();
            }
        }


    }


}
