using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignText : MonoBehaviour
{

    public Transform textPosition;
    public Text textUI;

    private void Update()
    {
        textUI.transform.position = textPosition.position;
    }

    //Kill Enemy
    public void KillText()
    {
        textUI.text = "E to Kill";
    }

    //Hide in box
    public void Hide()
    {
        textUI.text = "E to Hide";
    }

    //Pick up interactive items
    public void Pickup()
    {
        textUI.text = "E to Pickup";
    }

    //Interact items
    public void Interact()
    {
        textUI.text = "E to Interact";
    }

    //Need switch
    public void NeedSwitch()
    {
        textUI.text = "Need Switch";
    }

    public void NeedKey()
    {
        textUI.text = "Need Key";
    }

    public void NeedItem()
    {
        textUI.text = "Need Item";
    }
}
