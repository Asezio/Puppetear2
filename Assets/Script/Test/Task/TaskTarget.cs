using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskTarget : MonoBehaviour
{
    public int nonTargetAmount;
    public static int nonTargetFinAmount;
    public GameObject nonTargetTask;

    public int sleepyAmount;
    public static int sleepyFinAmount;
    public GameObject sleepyTask;

    public int poisonAmount;
    public static int poisonFinAmount;
    public GameObject poisonTask;

    public int bossAmount;
    public static int bossFinAmount;
    public GameObject bossTask;
  
    public bool isLevelPass = false;


    private void Start()
    {
        nonTargetFinAmount = 0;
        sleepyFinAmount = 0;
        poisonFinAmount = 0;
        bossFinAmount = 0;
        SetTask(nonTargetTask, nonTargetAmount);
        SetTask(sleepyTask, sleepyAmount);
        SetTask(poisonTask, poisonAmount);
        SetTask(bossTask, bossAmount);
    }

    private void Update()
    {
        UpdateTask(nonTargetTask, nonTargetFinAmount, nonTargetAmount);
        UpdateTask(sleepyTask, sleepyFinAmount, sleepyAmount);
        UpdateTask(poisonTask, poisonFinAmount, poisonAmount);
        UpdateTask(bossTask, bossFinAmount, bossAmount);

        if (nonTargetAmount<=nonTargetFinAmount &&
            sleepyAmount<=sleepyFinAmount &&
            poisonAmount<=poisonFinAmount &&
            bossAmount<=bossFinAmount)
        {
            isLevelPass = true;
        }
    }

    public void SetTask(GameObject task, int require)
    {
        task.transform.GetChild(3).GetComponent<Text>().text = require.ToString();
    }

    public void UpdateTask(GameObject task, int finish, int require)
    {

        task.transform.GetChild(2).GetComponent<Text>().text = finish.ToString();

        if (finish == require)
        {
            for (int i = 0; i < 4; i++)
            {
                task.transform.GetChild(i).GetComponent<Text>().color = Color.green;
            }
        }
    }

}
