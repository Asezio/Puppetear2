using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemMoveable : ItemBase
{
    public string targetName;//Static物体不用加
    public int eventNumber;//1.Poison 2.Exposion  加给触发物体
    public LayerMask itemSlayer;
    protected Transform targetTrans;
    protected Transform playerTrans;

    public bool detected;
    public bool isStick;
    private float duration;
    private float raycastDistance;
    private float offsetX;
    private float detectRange;
    public bool startDetect;

    protected GameObject test;
    protected GameObject used;
    private GameObject player;

    protected List<float> itemSList;
    protected Dictionary<float, GameObject> itemSDic;
    // Start is called before the first frame update
    void Awake()
    {
        base.Awake();
        targetTrans = GameObject.Find("ItemPoint").GetComponent<Transform>();
        playerTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player");
        isStick = false;
        duration = 0.5f;
        raycastDistance = targetTrans.position.y - playerTrans.position.y;
        startDetect = false;
        detected = false;
        offsetX = 0.1f;
        detectRange = 0.2f;

        test = GameObject.Find("FirstAttached");
    }

    // Update is called once per frame
    void Update()
    {
        //MoveTo();
        //Stick();
        if (startDetect == true)
        {
            Detect();
        }
    }

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
        StartCoroutine(ChangeStickFalse());
    }

    IEnumerator ChangeStickFalse()
    {
        yield return new WaitForSeconds(0.3f);
        isStick = false;
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
        isStick = true;
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

    public void Detect()
    {
        Collider2D[] detectThings = Physics2D.OverlapCircleAll(targetTrans.position, detectRange, itemSlayer);
        //Debug.Log(hitEnemies.Length);
        if (detectThings.Length > 0)
        {
            //for (int i = 0; i < detectThings.Length; i++)
            //{
                //float dis = Vector3.Distance(detectThings[i].gameObject.transform.position, targetTrans.position);
                //itemSDic.Add(dis, detectThings[i].gameObject);
                //Debug.Log(dis);
                //if (!itemSList.Contains(dis))
            //    {
            //        itemSList.Add(dis);
            //    }
            //}
            //itemSList.Sort();//对距离进行排序
                             //Debug.Log("***" + enemyList[0]);
            GameObject obj;           
            obj = detectThings[0].gameObject;
            
            //itemSDic.TryGetValue(itemSList[0], out obj);//获取距离最近的对象
            //Debug.Log(obj.name);
            //若检测物体发生改变，上一检测目标取消描边
            if (test != obj)
            {
                test.GetComponent<Interactable>().ExitMiaobian();
            }

            //当前物体描边
            if (obj.GetComponent<ItemStatic>().itemName == gameObject.GetComponent<ItemMoveable>().targetName)
            {
                Debug.Log(obj.name);
                detected = true;
                obj.GetComponent<Interactable>().MiaoBian();
                player.GetComponent<SignText>().Interact();
                used = obj;
            }


            //将检测物体设置为当前选中物体
            test = obj;

            //加UI位置

            //重置字典与列表
            //itemSList.Clear();
            //itemSDic.Clear();
        }
        else
        {
            detected = false;
            test.GetComponent<Interactable>().ExitMiaobian();
            test = GameObject.Find("FirstAttached");
        }
    }

    public void ItemInteract(int number)// ji de xian qu xiao miaobian 
    {
        if (number == 1)
        {
            used.GetComponent<ItemStatic>().isPoisoned = true;
            //used.GetComponent<ItemStatic>().Destroy();
            used.GetComponent<SpriteRenderer>().color = new Color32(0,255,25,255);
        }
        else if (number == 2)
        {
            used.GetComponent<ItemStatic>().FBIOpenTheDoor();
            //Debug.Log("Here");
        }
        else if (number == 3)
        {
            used.GetComponentInChildren<DrinkerBreak>().Active();           
        }
    }

    public void Destroy()
    {
        test.GetComponent<Interactable>().ExitMiaobian();
        Destroy(gameObject);
    }
}
