using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2ColOut : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.SetInt("Stage2Out", 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player_Controller>() != null && PlayerPrefs.GetInt("Stage2Out") == 0)
        {
            GameObject.Find("Stage2").GetComponent<UIFadeInOut>().FadeOut();
            PlayerPrefs.SetInt("Stage2Out", 1);
        }
    }
}
