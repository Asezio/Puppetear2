using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.tag == "Player" && gameObject.GetComponentInChildren<OpenDoor>().canOpen == true) || other.tag == "Enemy")
        {
            gameObject.GetComponentInChildren<OpenDoor>().OpenTheDoor();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {

        if ((other.tag == "Player" && gameObject.GetComponentInChildren<OpenDoor>().canOpen == true) || other.tag == "Enemy")
        {
            //Debug.Log("Close");
            gameObject.GetComponentInChildren<OpenDoor>().CloseTheDoor();
        }
    }
}
