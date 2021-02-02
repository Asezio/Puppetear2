using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public static TaskManager instance;

    public GameObject[] taskArray;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    public void UpdateTaskList(int taskNumber)
    {
        taskArray[taskNumber - 1].transform.GetChild(1).GetComponent<Text>().text = Task.TaskState.Finished.ToString();
        taskArray[taskNumber - 1].transform.GetChild(1).GetComponent<Text>().color = Color.green;
    }
}
