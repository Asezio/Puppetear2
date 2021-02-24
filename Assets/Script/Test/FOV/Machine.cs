using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour
{
    public bool machState;
    // Start is called before the first frame update
    void Start()
    {
        machState = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            machState = true;
        }
    }
}
