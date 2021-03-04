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

    private bool scale1Flag;
    private bool scale2Flag;
    private bool scale3Flaf;
    // Use this for initialization
    void Awake()
    {
        TimeBar = GetComponent<Image>();
        isActive = true;
        points = GameObject.Find("Chara_Player").GetComponent<Points>();
        //shaLou = GameObject.Find("ShaLou");
        scale1Flag = false;
        scale2Flag = false;
        scale3Flaf = false;
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
        if(scale1Flag == false && TimeBar.fillAmount < 0.7f )
        {
            if(TimeBar.fillAmount >=0.5f )
            {
                StartCoroutine(Scale1());
            }
            else if(TimeBar.fillAmount >= 0.2f)
            {
                StartCoroutine(Scale2());
            }
            else if (TimeBar.fillAmount >= 0f)
            {
                StartCoroutine(Scale3());
            }
        }
    }

    public void Refresh()
    {
        StartCoroutine(Rotate());
    }

    IEnumerator Rotate()
    {
        //Debug.Log("1");
        TimeBar.transform.DOLocalRotate(new Vector3(0, 0, 180), 0.25f);
        TimeBarDown.transform.DOLocalRotate(new Vector3(0, 0, 180), 0.25f);
        TimeCao.transform.DOLocalRotate(new Vector3(0, 0, 180), 0.25f);
        yield return new WaitForSeconds(0.25f);
        TimeBar.transform.rotation = Quaternion.Euler(0, 0, 0);
        TimeBarDown.transform.rotation = Quaternion.Euler(0, 0, 0);
        TimeCao.transform.rotation = Quaternion.Euler(0, 0, 0);
        timeLeft = timeMax;
    }

    IEnumerator Scale1()
    {
        scale1Flag = true;
        TimeBar.transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 1f, 1);
        TimeBarDown.transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 1f, 1);
        TimeCao.transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 1f, 1);
        yield return new WaitForSeconds(1f);
        scale1Flag = false;
    }
    IEnumerator Scale2()
    {
        scale1Flag = true;
        TimeBar.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.85f, 1);
        TimeBarDown.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.85f, 1);
        TimeCao.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.85f, 1);
        yield return new WaitForSeconds(0.85f);
        scale1Flag = false;
    }
    IEnumerator Scale3()
    {
        scale1Flag = true;
        TimeBar.transform.DOPunchScale(new Vector3(0.3f, 0.3f, 0.3f), 0.7f, 1);
        TimeBarDown.transform.DOPunchScale(new Vector3(0.3f, 0.3f, 0.3f), 0.7f, 1);
        TimeCao.transform.DOPunchScale(new Vector3(0.3f, 0.3f, 0.3f), 0.7f, 1);
        yield return new WaitForSeconds(0.7f);
        scale1Flag = false;
    }
}
