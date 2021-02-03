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
    // Start is called before the first frame update
    void Start()
    {
        currentPos = GetComponent<Transform>();
        canOpen = false;
    }

    // Update is called once per frame

    public void OpenTheDoor()
    {
        Tweener moveTween = transform.DOMove(targetPos.position, lastTime);
        moveTween.SetEase(Ease.OutQuint);
    }

    public void CloseTheDoor()
    {
        Vector3 up = new Vector3(currentPos.position.x * 2 - targetPos.position.x, currentPos.position.y * 2 - targetPos.position.y, currentPos.position.z);
        Tweener moveTween = transform.DOMove(up, lastTime);
        moveTween.SetEase(Ease.OutQuint);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if((other.tag == "Player" && canOpen == true)||other.tag == "Enemy")
        {
            OpenTheDoor();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        
        if ((other.tag == "Player" && canOpen == true) || other.tag == "Enemy")
        {
            //Debug.Log("Close");
            CloseTheDoor();
        }
    }
}
