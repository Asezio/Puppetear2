using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimeBar : MonoBehaviour
{
    public Text timeText;
    public float timeMax;
    public static float timeLeft;
    private int convertTime;
    private Image TimeBar;
    public Image TimeBarDown;
    public bool isActive;
    public float speed;
    private Points points;

    // Use this for initialization
    void Awake()
    {
        TimeBar = GetComponent<Image>();
        isActive = true;
        points = GameObject.Find("Chara_Player").GetComponent<Points>();   
    }

    void Start()
    {
        timeMax = points.maxTime;
        timeLeft = timeMax;
    }
    // Update is called once per frame
    void Update()
    {
        if (isActive == true)
        {
            timeLeft = timeLeft - Time.deltaTime;
        }
        TimeBar.fillAmount = timeLeft / timeMax;
        TimeBarDown.fillAmount = 1-timeLeft / timeMax;
        convertTime = (int)timeLeft;
        timeText.text = convertTime.ToString() + "s";
    }

    public void Refresh()
    {
        timeLeft = timeMax;
    }
}
