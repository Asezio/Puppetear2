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
    public float healthLevel1;
    public float healthLevel2;
    public float healthLevel3;
    public float healthLevel4;
    public float healthLevel5;

    [Header("Speed Level")]
    public float speedLevel1;
    public float speedLevel2;
    public float speedLevel3;
    public float speedLevel4;
    public float speedLevel5;

    //public int hintPoint;
}
