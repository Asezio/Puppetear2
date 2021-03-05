using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OpenDoor : MonoBehaviour
{
    public Transform currentPos;
    public Transform targetPos;
    public float lastTime;
    public bool canOpen;

    private Vector3 cPs;
    private Vector3 tPs;
    // Start is called before the first frame update
    void Awake()
    {
        canOpen = false;
        cPs = new Vector3(currentPos.position.x, currentPos.position.y, currentPos.position.z);
        tPs = new Vector3(targetPos.position.x, targetPos.position.y, targetPos.position.z);
    }

    // Update is called once per frame

    public void OpenTheDoor()
    {
        Tweener moveTween = transform.DOMove(tPs, lastTime);
        moveTween.SetEase(Ease.OutQuint);
    }

    public void CloseTheDoor()
    {
        Tweener moveTween = transform.DOMove(cPs, lastTime);
        moveTween.SetEase(Ease.OutQuint);
    }

    public void KeepDoorOpen()
    {
        OpenTheDoor();
        canOpen = true;
        //GetComponentInParent<BoxCollider2D>().enabled = false;
    }

    public void ForceOpen(float time)
    {
        if(canOpen == false)
        {
            OpenTheDoor();
            //GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(DelayClose(time));
        }

    }

    IEnumerator DelayClose(float time)
    {
        yield return new WaitForSeconds(time);
        CloseTheDoor();
        //GetComponent<BoxCollider2D>().enabled = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy" && canOpen ==false)
        {
            OpenTheDoor();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        
        if ( other.tag == "Enemy" && canOpen ==false)
        {
            //Debug.Log("Close");
            CloseTheDoor();
        }
    }
}
