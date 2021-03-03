using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UITimeBar : MonoBehaviour
{
    public Text timeText;
    public float timeMax;
    public static float timeLeft;
    private int convertTime;
    private Image TimeBar;
    public Image TimeCao;
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
        //shaLou = GameObject.Find("ShaLou");
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
        StartCoroutine(Rotate());
    }

    IEnumerator Rotate()
    {
        Debug.Log("1");
        TimeBar.transform.DOLocalRotate(new Vector3(0, 0, 180), 0.25f);
        TimeBarDown.transform.DOLocalRotate(new Vector3(0, 0, 180), 0.25f);
        TimeCao.transform.DOLocalRotate(new Vector3(0, 0, 180), 0.25f);
        yield return new WaitForSeconds(0.25f);
        TimeBar.transform.rotation = Quaternion.Euler(0, 0, 0);
        TimeBarDown.transform.rotation = Quaternion.Euler(0, 0, 0);
        TimeCao.transform.rotation = Quaternion.Euler(0, 0, 0);
        timeLeft = timeMax;
    }
}
