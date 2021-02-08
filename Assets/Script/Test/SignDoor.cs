using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignDoor : MonoBehaviour
{
    public Transform textPosition;
    public Text textUI;

    private void Start()
    {
        textUI.gameObject.SetActive(false);
    }

    private void Update()
    {
        textUI.transform.position = textPosition.position;
    }


    //Need switch
    public void NeedSwitch()
    {
        textUI.text = "Need Switch first";
    }



}
