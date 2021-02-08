using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricBox : MonoBehaviour
{
    public GameObject doorUnderCtrl;
    public float lastTime;
    // Start is called before the first frame update
public void ForceOpen()
    {
        doorUnderCtrl.GetComponent<OpenDoor>().ForceOpen(lastTime);  
    }
}
