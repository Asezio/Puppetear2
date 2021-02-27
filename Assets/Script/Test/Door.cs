using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.name);
        if (collision.tag == "Player")
        {
            Debug.Log(collision.gameObject.name);
            if (GetComponentInParent<ItemStatic>().isAvailable == true)
            {
                collision.GetComponent<SignDoor>().NeedSwitch();
                collision.GetComponent<SignDoor>().textUI.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.tag == "Player")
        {
            //Debug.Log(collision.gameObject.name);
            if (GetComponentInParent<ItemStatic>().isAvailable == true)
            {
                collision.GetComponent<SignDoor>().textUI.gameObject.SetActive(false);
            }
        }
    }

}
