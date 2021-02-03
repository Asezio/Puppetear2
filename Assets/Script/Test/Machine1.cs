using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine1 : MonoBehaviour
{
    public bool machState;
    public float lastTime;
    // Start is called before the first frame update
    void Start()
    {
        machState = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<ItemStatic>().switchflag==true)
        {
            machState = true;
        }
    }


}
