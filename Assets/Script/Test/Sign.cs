using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{

    [Header("Tweaks")]
    [SerializeField] public Transform lookAt;
    [SerializeField] public Vector3 offset;

    [Header("Logic")]
    private Camera cam;

    public Text signText;
    public string sign;
    bool isPlayerInsign;

     void Start()
    {
        cam = Camera.main;
        signText.gameObject.SetActive(false);
       
    }
    void Update()
    {
        if(isPlayerInsign)
        {
            signText.gameObject.SetActive(true);
        }

        Vector3 pos = cam.WorldToScreenPoint(lookAt.position + offset);

        if (signText.transform.position != pos)
           signText.transform.position = pos;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag=="Player")
        {
            Debug.Log("enter sign");
            isPlayerInsign = true;
            signText.text= sign;
        }
    }

   void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("leave sign");
            isPlayerInsign = false;
            signText.gameObject.SetActive(false);
            
        }
    }
}
