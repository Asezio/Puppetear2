using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskTarget : MonoBehaviour
{
    public int targetAmount;
    public int poisonAmount;
    public static int targetFinAmount;
    public static int poisonFinAmount;
    public GameObject targetTask;
    public GameObject poisonTask;

    private void Start()
    {
        targetFinAmount = 0;
    }

    private void Update()
    {
        UpdateTask(targetTask,targetFinAmount,targetAmount);
        UpdateTask(poisonTask, poisonFinAmount, poisonAmount);
    }

    public void UpdateTask(GameObject task, int finish, int require)
    {
        task.transform.GetChild(2).GetComponent<Text>().text = finish.ToString();

        if (finish == require)
        {
            for (int i = 0; i < 3; i++)
            {
                task.transform.GetChild(i).GetComponent<Text>().color = Color.green;
            }
        }
    }

}
