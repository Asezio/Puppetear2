using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDetectBar : MonoBehaviour
{
    public Image detectBar;
    public bool isFound;
    private GameObject player;
    [SerializeField] private float barIncreasingSpeed;
    [SerializeField] private float barDecreasingSpeed;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        detectBar.fillAmount = 0;
        isFound = false;
        //this.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isFound == false)
        {
            BarDecrease();

        }
        else
        {
            BarIncrease();
        }
    }

    private void BarDecrease()
    {
        if (detectBar.fillAmount > 0)
        {
            detectBar.fillAmount -= barDecreasingSpeed * Time.deltaTime;
        }
        else
        {
            detectBar.fillAmount = 0;
            GetComponent<CanvasGroup>().alpha = 0;
        }
    }

    private void BarIncrease()
    {
        GetComponent<CanvasGroup>().alpha = 1;
        detectBar.fillAmount += barIncreasingSpeed * Time.deltaTime;
        if (detectBar.fillAmount >= 1)
        {
            player.GetComponent<Player_Controller>().PlayerDead();
        }
    }

}
