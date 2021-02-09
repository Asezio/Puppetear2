using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private GameObject door;

    void Awake()
    {
        door = GetComponentInChildren<OpenDoor>().gameObject;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" && door.GetComponent<OpenDoor>().canOpen == false)
        {
            door.GetComponent<OpenDoor>().OpenTheDoor();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {

        if (other.tag == "Enemy" && door.GetComponent<OpenDoor>().canOpen == false)
        {
            //Debug.Log("Close");
            door.GetComponent<OpenDoor>().CloseTheDoor();
        }
    }
}
