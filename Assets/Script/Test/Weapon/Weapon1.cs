using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon1 : WeaponBase
{
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        DetectThing();       
        Flip();
        if (canAttack == true && GameObject.Find("UI_Weapon1").GetComponent<UIweapon>().isReady == true)
        {
            if (Input.GetButtonDown("Interact"))
            {
                Debug.Log("Attack");
                Attack();
            }
        }
    }


}
