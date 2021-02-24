using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDetectBar : MonoBehaviour
{
    private Image detectBar;
    public static bool isFound;
    [SerializeField] private float detectSpeed;

     void Awake()
    {
        detectBar = GameObject.Find("BarFilled").GetComponent<Image>();
        detectBar.fillAmount = 0;
        isFound = false;
        //this.gameObject.SetActive(false);
    }

     void Update()
    {
        if (isFound == false)
        {
            Debug.Log("notFound");
            GetComponent<CanvasGroup>().alpha = 0;
            BarDecrease();
        }
        else
        {
            Debug.Log("111");
            GetComponent<CanvasGroup>().alpha = 1;
            BarIncrease();
        }
    }

    private void BarDecrease()
    {
        
        if (detectBar.fillAmount > 0)
        {
            detectBar.fillAmount -= detectSpeed * Time.deltaTime; 
            
        }
        else
        {
            detectBar.fillAmount = 0;
        }
    }

    private void BarIncrease()
    {
        detectBar.fillAmount += detectSpeed * Time.deltaTime;
        if (detectBar.fillAmount >= 1)
        {
            //Player die
        }
    }

}
