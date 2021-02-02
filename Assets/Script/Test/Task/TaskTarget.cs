using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskTarget : MonoBehaviour
{
    public GameObject[] targetList;
    public int targetAmount;

    private void Start()
    {
        targetAmount = targetList.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TargetManage()
    {
        for (int i = 0; i < targetList.Length; i++)
        {
            if (targetList[i] == null)
            {

            }
        }
    }
}
