﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPlace : MonoBehaviour
{
    private GameObject player;
    private GameObject canvas;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        canvas = GameObject.Find("Canvas");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("1");
        //&& player.GetComponent<TaskTarget>().isLevelPass == true
        if (other.tag == ("Player"))
        {
            Debug.Log("2");
            canvas.GetComponent<SceneManagement>().WinPanel();
            player.GetComponent<CapsuleCollider2D>().enabled = false;
        }
        else
        {
            //UI
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        //UI
    }
}
