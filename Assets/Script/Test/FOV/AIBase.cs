using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBase : MonoBehaviour
{  
    protected SpriteRenderer sr;
    protected Animator anim;
    public bool isWalking;

    [Header("Moving Speed")]
    public float speed = 25f;

    [Header("FOV Setting")]
    [SerializeField] protected Transform pfFieldofView;
    [SerializeField] protected float fov = 90f;
    [SerializeField] protected float viewDistance = 50f;
    [SerializeField] protected Vector3 aimDirection;
    public float offsetX;
    protected Vector3 lastMoveDir;
    public FOV fieldOfView;

    [Header("Player Setting")]
    [SerializeField] protected Transform player;
    [SerializeField] protected LayerMask playerLayer;
    private GameObject canvas;

    [Header("Is Target")]
    public bool isTarget;

    [Header("Detect Delay Time")]
    public float delayTime;
    private float delayMaxTime;


  

    protected virtual void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        canvas = GameObject.Find("Canvas");
        delayTime = 0.2f;
        delayMaxTime = delayTime;
        anim = GetComponent<Animator>();
        isWalking = false;
    }

    protected virtual void Start()
    {
        //Set the aimDirection
        lastMoveDir = aimDirection;

        //Instantiate FOV
        fieldOfView = Instantiate(pfFieldofView, null).GetComponent<FOV>();
        fieldOfView.SetFoV(fov);
        fieldOfView.SetViewDistance(viewDistance);

        Physics2D.queriesStartInColliders = false;
    }

    protected virtual void Update()
    {
        AIFlip();

        //Link FOV on enemy
        if (fieldOfView != null)
        {
            if (lastMoveDir.x < 0)
            {
                fieldOfView.SetOrigin(transform.position + new Vector3(offsetX, 0));
            }

            if (lastMoveDir.x > 0)
            {
                fieldOfView.SetOrigin(transform.position - new Vector3(offsetX, 0));
            }

            fieldOfView.SetAimDirection(GetAimDir());
        }

        if (fieldOfView != null &&
            fieldOfView.gameObject.activeSelf == true)
        {
            FindTargetPlayer();
        }
      
        //Show the move direction
        Debug.DrawLine(transform.position, transform.position + GetAimDir() * 0.5f);

    }

    protected virtual void FindTargetPlayer()
    {
        if (Vector3.Distance(GetPosition(), player.position) < viewDistance)
        {

            // Player inside viewDistance
            Vector3 dirToPlayer = (player.position - GetPosition()).normalized;
            if (Vector3.Angle(GetAimDir(), dirToPlayer) < fov / 2f)
            {
                RaycastHit2D raycastHit2D = Physics2D.Raycast(GetPosition(), dirToPlayer, viewDistance);
                if (raycastHit2D.collider != null)
                {
                    //Debug.Log(raycastHit2D.collider.name);
                    if (raycastHit2D.collider.CompareTag("Player"))
                    {
                        delayTime -= Time.deltaTime;
                        if (delayTime <= 0)
                        {
                            UIDetectBar.isFound = true;
                            Debug.Log("IsFound: " + UIDetectBar.isFound);
                            //player.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
                            //canvas.GetComponent<SceneManagement>().LosePanel();
                            //Debug.Log("hit by" + this);
                            delayTime = delayMaxTime;
                        }
                        //canvas.GetComponent<SceneManagement>().Restart();

                    }
                }
            }
        }
    }


    protected virtual Vector3 GetPosition()
    {
        return transform.position;
    }

    protected virtual Vector3 GetAimDir()
    {
        return lastMoveDir;
    }

    protected virtual void AIFlip()
    {
        if (lastMoveDir.x > 0)
        {
            sr.flipX = true;
        }

        if (lastMoveDir.x < 0)
        {
            sr.flipX = false;
        }
    }
}
