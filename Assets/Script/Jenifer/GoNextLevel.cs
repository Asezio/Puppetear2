﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Go_Next_Level : MonoBehaviour
{

    int m_test = 1;
    private void OnTriggerEnter2D(Collider2D oogaboogaboo)
    {
        //Debug.LogError("player colliding here OnTrigger Enter2D");
        if (oogaboogaboo.GetComponent<PlayerController>() != null)
        {
            //Debug.LogError("player colliding here, value pf int mt_test is:"+m_test);
            //SceneManager.LoadScence();
            SceneManager.LoadScene("SampleScene_02");
        }
        else
        {
            //do nothing
        }
    }

    

}
