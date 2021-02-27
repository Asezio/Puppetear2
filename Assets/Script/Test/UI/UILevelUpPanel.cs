using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevelUpPanel : MonoBehaviour
{
    public Text pointNum;
    public Text speedNum;
    public Text healthNum;
    public Text textComment;
    private Points point;
    public Button speedButton;
    public Button healthButton;

    void Awake()
    {
        point = GameObject.Find("Chara_Player").GetComponent<Points>();
    }

    // Start is called before the first frame update
 
    // Update is called once per frame
    void Update()
    {
        if(pointNum != null)
        {
            pointNum.text = point.sp.skillPoint.ToString();
            speedNum.text = point.sp.speedlevel.ToString();
            healthNum.text = point.sp.healthlevel.ToString();

        }
    }

    public void Comment(int num)
    {
        if(num == 1)
        {
            textComment.text = "You don't have enough skill points.";
        }
        else if (num == 2)
        {
            textComment.text = "Your speed level is max.";
        }
        else if (num == 3)
        {
            textComment.text = "Your health level is max.";
        }
    }
}

