using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskTarget2 : MonoBehaviour
{
    public int doorKeeperAmount;
    public static int doorKeeperFinAmount;
    public GameObject doorKeeperTask;

    public int sewerPassAmount;
    public static int sewerPassFinAmount;
    public GameObject sewerPassTask;

    public bool isLevelPass = false;


    private void Start()
    {
        doorKeeperFinAmount = 0;
        SetTask(doorKeeperTask, doorKeeperAmount);
        SetTask(sewerPassTask, sewerPassAmount);
    }

    private void Update()
    {
        UpdateTask(doorKeeperTask, doorKeeperFinAmount, doorKeeperAmount);
        UpdateTask(sewerPassTask,sewerPassFinAmount, sewerPassAmount);

        if (doorKeeperAmount <= doorKeeperFinAmount)
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
        if (task.transform.GetChild(2).GetComponent<Text>() != null)
        {
            task.transform.GetChild(2).GetComponent<Text>().text = finish.ToString();
        }


        if (finish == require)
        {
            for (int i = 0; i < 4; i++)
            {
                task.transform.GetChild(i).GetComponent<Text>().color = Color.green;
            }
        }
    }
}
