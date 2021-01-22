using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    public float DetectRange;       //检测范围
    public float cooldown;
    public float timer;
    public float time;
    public float starttime;
    public bool canAttack;
    public bool isCarrying;
    public bool isHiden;
    public bool isHaveThing;
    public bool isReady;
    //protected bool canInteract= false;
    public LayerMask thinglayers;


    protected SpriteRenderer playerSr;
    protected Animator anim;
    protected Transform playerTrans;
    public Transform attackPoint;
    private PolygonCollider2D coll2D;

    protected GameObject test;
    public GameObject used;

    //protected GameObject[] enemyArr;
    protected List<float> thingList;
    protected Dictionary<float, GameObject> thingDic;

    //protected List<float> itemList;
    //protected Dictionary<float, GameObject> itemDic;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        playerTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerSr = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
        coll2D = GetComponent<PolygonCollider2D>();

        test = GameObject.Find("FirstAttached");

        thingDic = new Dictionary<float, GameObject>();//初始化       
        thingList = new List<float>();

        //itemDic = new Dictionary<float, GameObject>();//初始化       
        //itemList = new List<float>();
        isHaveThing = false;
        isHiden = false;
        canAttack = true;
        isCarrying = false;
    
    }




    protected void DetectThing()
    {
        if (isCarrying == true)
        {

            canAttack = false;
            used.GetComponent<SortingOrder>().enabled = false;
            used.GetComponent<SpriteRenderer>().sortingOrder = GetComponentInParent<SpriteRenderer>().sortingOrder;
            used.GetComponent<ItemBase>().Stick();
            if (Input.GetButtonDown("Interact") || Input.GetMouseButtonDown(0))
            {
                if (used.GetComponent<ItemBase>().CanDrop() == true)
                {
                    used.GetComponent<ItemBase>().Drop();
                    isCarrying = false;
                    StartCoroutine(ActivateAttack());
                    used.GetComponent<SortingOrder>().enabled = true;
                }
                else
                {
                    //显示UI 无法掉落
                    Debug.Log("Can't drop.");
                }
            }
        }
        if (isCarrying == false && isHiden == false)
        {
            Collider2D[] hitThings = Physics2D.OverlapCircleAll(attackPoint.position, DetectRange, thinglayers);
            //Debug.Log(hitEnemies.Length);
            if (hitThings.Length > 0)
            {
                for (int i = 0; i < hitThings.Length; i++)
                {
                    float dis = Vector3.Distance(hitThings[i].gameObject.transform.position, playerTrans.position);
                    thingDic.Add(dis, hitThings[i].gameObject);
                    //Debug.Log(dis);
                    if (!thingList.Contains(dis))
                    {
                        thingList.Add(dis);
                    }
                }
                thingList.Sort();//对距离进行排序
                                 //Debug.Log("***" + enemyList[0]);
                GameObject obj;
                thingDic.TryGetValue(thingList[0], out obj);//获取距离最近的对象
                                                            //Debug.Log(obj.name);
                                                            //若检测物体发生改变，上一检测目标取消描边
                if (test != obj)
                {
                    test.GetComponent<Interactable>().ExitMiaobian();
                }

                if (obj.tag != "Enemy")
                {
                    canAttack = false;
                }
                else
                {
                    canAttack = true;
                }

                if ((playerTrans.position.x < obj.transform.position.x && playerSr.flipX == true && playerSr.flipX == true)
                    || (playerTrans.position.x > obj.transform.position.x && playerSr.flipX == false && playerSr.flipX == false))
                {
                    //当前物体描边
                    obj.GetComponent<Interactable>().MiaoBian();

                    //将检测物体设置为当前选中物体
                    test = obj;


                    if (Input.GetButtonDown("Interact") || Input.GetMouseButtonDown(0))
                    {
                        if (obj.tag == ("Item"))
                        {
                            isCarrying = true;
                            used = obj;
                            //obj.GetComponent<ItemBase>().MoveTo();
                        }
                        //anim.SetTrigger("Interact");
                        //obj.GetComponent<Interactable>().ExitMiaobian();
                        //obj.GetComponent<EnemyBase>().Die();
                        //test.GetComponent<Interactable>().ExitMiaobian();
                        //GameObject.Find("UI_Weapon1").GetComponent<UIweapon>().Cooldown(gameObject);
                        //测试物体失去目标，将其指定为初始物体
                        //test = GameObject.Find("FirstAttached");

                    }

                }


                //重置字典与列表
                thingList.Clear();
                thingDic.Clear();
            }

            else
            {
                if (test != null)
                {
                    test.GetComponent<Interactable>().ExitMiaobian();
                }
                else
                {
                    test = GameObject.Find("FirstAttached");
                }
            }
        }
        
    }

    protected void Attack()
    {
        if(Input.GetMouseButtonDown(0)||Input.GetButtonDown("Interact"))
        {
            Debug.Log("Attack");
            if (canAttack == true && GameObject.Find("UI_Weapon1").GetComponent<UIweapon>().isReady == true)
            {
                //Debug.Log("Attack");
                anim.SetBool("Attack", true);
                anim.SetTrigger("Interact");
                StartCoroutine(StartAttack());
                GameObject.Find("UI_Weapon1").GetComponent<UIweapon1>().Activate();
            }
        }
    }
    IEnumerator disableHitBox()
    {
        yield return new WaitForSeconds(time);
        coll2D.enabled = false;
    }

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(starttime);
        coll2D.enabled = true;
        StartCoroutine(disableHitBox());
    }

    IEnumerator ActivateAttack()
    {
        yield return new WaitForEndOfFrame();
        canAttack = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyBase>().Die();
        }
    }

    protected void Flip()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().flipX == true)
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
    }
    //protected void DetectItem()
    //{

    //    Collider2D[] hitItems = Physics2D.OverlapCircleAll(attackPoint.position, WeaponRange, itemlayers);
    //    //Debug.Log(hitItems.Length);
    //    if (hitItems.Length > 0)
    //    {
    //        for (int i = 0; i < hitItems.Length; i++)
    //        {
    //            float dis = Vector3.Distance(hitItems[i].gameObject.transform.position, playerTrans.position);
    //            itemDic.Add(dis, hitItems[i].gameObject);
    //            //Debug.Log(dis);
    //            if (!itemList.Contains(dis))
    //            {
    //                itemList.Add(dis);
    //            }
    //        }
    //        itemList.Sort();//对距离进行排序
    //        //Debug.Log("***" + itemList[0]);
    //        GameObject obj;
    //        itemDic.TryGetValue(itemList[0], out obj);//获取距离最近的对象
    //        //Debug.Log(obj.name);
    //        if (test != obj)
    //        {
    //            test.GetComponent<Interactable>().ExitMiaobian();
    //        }
    //        if ((playerTrans.position.x < obj.transform.position.x && playerSr.flipX == true)
    //            || (playerTrans.position.x > obj.transform.position.x && playerSr.flipX == false))
    //        {
    //            //当前物体描边
    //            obj.GetComponent<Interactable>().MiaoBian();

    //            //将检测物体设置为当前选中物体
    //            test = obj;

    //            if (Input.GetButtonDown("Interact") || Input.GetMouseButtonDown(0))
    //            {
    //                anim.SetTrigger("Interact");
    //            }
    //        }
    //        itemList.Clear();
    //        itemDic.Clear();

    //    }
    //    else
    //    {
    //        test.GetComponent<Interactable>().ExitMiaobian();
    //    }
    //}


    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, DetectRange);
    }

}