using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemBase : MonoBehaviour
{
    public string itemName;
    public SpriteRenderer sr;
    protected Transform targetTrans;
    protected Transform playerTrans;
    public bool isStick;
    public float duration;
    private float raycastDistance;
    public float offsetX;
    //public bool startmove;

    // Start is called before the first frame update
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        targetTrans = GameObject.Find("ItemPoint").GetComponent<Transform>();
        playerTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        isStick = false;
        duration = 0.5f;
        raycastDistance = targetTrans.position.y - playerTrans.position.y;
        //startmove = true;
    }

    //public void StartMove()
    //{
    //    Invoke("ChangeStickTrue", duration);
    //    startmove = false;
    //}

    public void MoveTo()
    {

        Tweener moveTween = transform.DOMove(targetTrans.position, duration);
        moveTween.SetEase(Ease.Linear);
  
    }   
    
    public void Drop()
    {
        //isStick = false;
        Vector3 dropPosition = new Vector3(targetTrans.position.x, playerTrans.position.y, targetTrans.position.z);
        Tweener moveTween = transform.DOMove(dropPosition, duration);
        //gameObject.GetComponent<Collider2D>().enabled = true;
        //Invoke("ChangeStartmoveTrue", duration);       
    }
        
    void ChangeStickTrue()
    {
        isStick = true;
    }

    public bool CanDrop()
    {
        Vector2 raycastStart = transform.position;
        RaycastHit2D hitInfo = Physics2D.Raycast(raycastStart, Vector2.down, raycastDistance);
        Debug.DrawRay(raycastStart, Vector2.down * raycastDistance, Color.red);
        if (hitInfo.collider != null)
        {
            Debug.Log(hitInfo.collider.gameObject.name);
        }
        //GameObject test = gameObject;
        //test.transform.position = new Vector3(targetTrans.position.x, playerTrans.position.y, targetTrans.position.z);
        if (hitInfo.collider == null || hitInfo.collider.gameObject.tag != "Obstacles" && hitInfo.collider.gameObject.tag != "Wall")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //void ChangeStartmoveTrue()
    //{
        //startmove = true;
        
    //}

    //private IEnumerator MoveTawords(Transform tr, Vector3 pos, float time)
    //{
    //    float t = 0;
        
    //    while (true)
    //    {
    //        t += Time.deltaTime;
    //        float a = t / time;
    //        tr.position = Vector3.Lerp(tr.position, pos, a);
    //        if (a >= 1.0f)
    //            break;
    //        yield return null;
    //    }
    //    isStick = true;


    //}


    //private IEnumerator Drop(Transform tr, Vector3 pos, float time)
    //{
    //    float t = 0;

    //    while (true)
    //    {
    //        t += Time.deltaTime;
    //        float a = t / time;
    //        tr.position = Vector3.Lerp(tr.position, pos, a);
    //        if (a >= 1.0f)
    //            break;
    //        yield return null;
    //    }
    //    // Update is called once per frame

    //}
    public void Stick()
    {
        //gameObject.GetComponent<Collider2D>().enabled = false;
        transform.position = targetTrans.position;
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().flipX == false)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }

}
