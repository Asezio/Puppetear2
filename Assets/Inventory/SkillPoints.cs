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

    public int speedlevel=1;
    public int healthlevel=1;

    public bool intro = false;

    [Header("Health Level")]
    public int healthLevel1;
    public int healthLevel2;
    public int healthLevel3;
    public int healthLevel4;
    public int healthLevel5;

    [Header("Speed Level")]
    public int speedLevel1;
    public int speedLevel2;
    public int speedLevel3;
    public int speedLevel4;
    public int speedLevel5;

    //public int hintPoint;
}
