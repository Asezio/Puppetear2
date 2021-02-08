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

    public void KillText()
    {
        textUI.text = "E to Kill";
    }
}
