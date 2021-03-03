using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Player_Controller player;
    [SerializeField]private Vector3 playerInitialPosition;

    private void Start()
    {
        PlayerPrefs.SetInt("FirstStageShow", 0);
        player = FindObjectOfType<Player_Controller>();
        playerInitialPosition = player.transform.position;

        if (PlayerPrefs.GetInt("FirstStageShow") == 0)
        {
            StartCoroutine(FirstStage());
            PlayerPrefs.SetInt("FirstStageShow", 1);
        }

    }

    private void Update()
    {
        if (Time.timeSinceLevelLoad>2)
        {
            if (playerInitialPosition != player.transform.position)
            {
                GameObject.Find("Stage1").GetComponentInChildren<Animator>().SetBool("isChange", false);
                GameObject.Find("Stage1").GetComponent<UIFadeInOut>().FadeOut();
            }
        }
    }



    IEnumerator FirstStage()
    {
        yield return new WaitForSeconds(1);    
        if (playerInitialPosition == player.transform.position)
        {
            GameObject.Find("Stage1").GetComponent<UIFadeInOut>().FadeIn();
            GameObject.Find("Stage1").GetComponentInChildren<Animator>().SetBool("isChange", true);
        }

    }
    







}
