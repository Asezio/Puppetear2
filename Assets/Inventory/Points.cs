using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    public int lvUpNeedPoints;
    public SkillPoints sp;
    // Start is called before the first frame update



    void Update()
    {
        if(sp.upgradePoint > lvUpNeedPoints)
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
    }

    public void PassGame()
    {
        if (sp.currentPoint > sp.bestScore)
        {
            sp.bestScore = sp.currentPoint;
        }
    }
}
