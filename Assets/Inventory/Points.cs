using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    private int totalPoints;
    public int lvUpNeedPoints;
    public SkillPoints sp;
    // Start is called before the first frame update

    void Awake()
    {
        totalPoints = 0;
    }

    void Upgrade()
    {
        if(totalPoints > lvUpNeedPoints)
        {
            sp.skillPoint++;
            totalPoints -= lvUpNeedPoints;
        }
    }
    public void AddPoints(int num)
    {
        sp.currentPoint += num;
        totalPoints += num;
    }

    public void UseSP()
    {
        sp.skillPoint--;
    }
    
}
