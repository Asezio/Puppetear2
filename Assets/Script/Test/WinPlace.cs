
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WinPlace : MonoBehaviour
{
    private GameObject player;
    private GameObject canvas;
    private TaskTarget task;
    private bool flag;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        canvas = GameObject.Find("Canvas");
        flag = true;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("1");
        //&& player.GetComponent<TaskTarget>().isLevelPass == true
        if (player.GetComponent<Points>().sp.scene == 1)
        {
            if (other.tag == ("Player"))
            {
                //Debug.Log("2");
                canvas.GetComponent<SceneManagement>().NextLevel();
                //player.GetComponent<CapsuleCollider2D>().enabled = false;
            }
            else
            {
                //UI
            }
        }
        else
        {
            canvas.GetComponent<SceneManagement>().WinPanel();
            if (other.tag == ("Player") && player.GetComponent<TaskTarget>().isLevelPass == true)
            {
                //Debug.Log("2");
                canvas.GetComponent<SceneManagement>().WinPanel();
                player.GetComponent<CapsuleCollider2D>().enabled = false;
            }
            else
            {
                //UI
            }
        }

    }
    //void OnTriggerStay2D(Collider2D other)
    //{
    //    //UI
    //}

    public void OpenFinalDoor()
    {
        OpenDoor[] openDoor = GetComponentsInChildren<OpenDoor>();
        for (int i = 0; i < openDoor.Length; i++)
        {
            openDoor[i].KeepDoorOpen();
        }
    }

    void Update()
    {
        if (player.GetComponent<Points>().sp.scene == 1)
        {
            if (player.GetComponent<TaskTarget2>().isLevelPass == true && flag == true)
            {
                flag = false;
                OpenFinalDoor();
            }
        }
        else if (player.GetComponent<Points>().sp.scene == 2)
        {
            var tT = player.GetComponent<TaskTarget>();
            if (tT != null)
            {
                if (player.GetComponent<TaskTarget>().isLevelPass == true && flag == true)
                {
                    Debug.Log("2333");
                    flag = false;
                    OpenFinalDoor();
                }
            }

        }
    }
}
