using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    public int lvUpNeedPoints;
    public SkillPoints sp;
    private UITimeBar timeBar;
    public float maxTime = 0f;
    public float speed = 0f;

    private UILevelUpPanel levelUpPanel;

    // Start is called before the first frame update
    void Awake()
    {
        if (GameObject.Find("TimeLeft") != null)
        {
            timeBar = GameObject.Find("TimeLeft").GetComponent<UITimeBar>();
        }
        levelUpPanel = GameObject.Find("Canvas").GetComponent<UILevelUpPanel>();

    }


    void Update()
    {
        if (sp.upgradePoint > lvUpNeedPoints)
        {
            sp.skillPoint++;
            sp.upgradePoint -= lvUpNeedPoints;
        }

        sp.totalTime += Time.deltaTime;

    }
    public void AddPoints(int num)
    {
        sp.currentPoint += num;
        sp.upgradePoint += num;
        //Debug.Log(sp.currentPoint);
    }

    public void UseSP()
    {
        sp.skillPoint--;
    }

    public void SpeedUpgrade()
    {
        if(sp.speedlevel< 5 && sp.skillPoint>0)
        {
            sp.speedlevel++;
            if(sp.speedlevel == 2)
            {
                speed = sp.speedLevel2;
            }
            else if (sp.speedlevel == 3)
            {
                speed = sp.speedLevel3;
            }
            else if (sp.speedlevel == 4)
            {
                speed = sp.speedLevel4;
            }
            else if (sp.speedlevel == 5)
            {
                speed = sp.speedLevel5;
            }
            UseSP();
            GetComponent<Player_Controller>().speed = speed;
        }
        else if (sp.speedlevel >=5)
        {
            levelUpPanel.Comment(2);
        }
        else if(sp.skillPoint <= 0)
        {
            levelUpPanel.Comment(1);
        }
        
    }

    public void HealthUpgrade()
    {
        if (sp.healthlevel < 5 && sp.skillPoint > 0)
        {
            sp.healthlevel++;
            if (sp.healthlevel == 2)
            {
                maxTime = sp.healthLevel2;
            }
            else if (sp.healthlevel == 3)
            {
                maxTime = sp.healthLevel3;
            }
            else if (sp.healthlevel == 4)
            {
                maxTime = sp.healthLevel4;
            }
            else if (sp.healthlevel == 5)
            {
                maxTime = sp.healthLevel5;
            }
            UseSP();
            timeBar.timeMax = maxTime;
            timeBar.Refresh();
        }
        else if(sp.healthlevel >=5)
        {
            levelUpPanel.Comment(3);
        }
        else if(sp.skillPoint <= 0)
        {
            levelUpPanel.Comment(1);
        }
    }


    public void SaveLevelData()
    {
        sp.levelPoint = sp.currentPoint;
        sp.levelTime = sp.totalTime;
    }

    public void Restart()
    {
        sp.currentPoint = sp.levelPoint;
        sp.totalTime = sp.levelTime;
    }

    public void Reset()
    {
        sp.skillPoint = 0;
        sp.currentPoint = 0;
        sp.levelPoint = 0;
        sp.levelTime = 0f;
        sp.totalTime = 0f;
        sp.upgradePoint = 0;
        sp.scene = 1;
        sp.speedlevel = 1;
        sp.healthlevel = 1;
        sp.intro = false;
    }

    public void PassGame()
    {
        if (sp.currentPoint > sp.bestScore)
        {
            sp.bestScore = sp.currentPoint;
        }
    }
}
