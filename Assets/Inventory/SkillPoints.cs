using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Skill",menuName ="Inventory/New Skill")]
public class SkillPoints : ScriptableObject
{
    public int scene = 1;
    public int upgradePoint = 0;
    public int currentPoint = 0;
    public int levelPoint = 0;
    public int bestScore = 0;

    public int skillPoint = 0;

    public float totalTime = 0f;
    public float levelTime = 0f;

    //public int hintPoint;
}
