using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2Col : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.SetInt("Stage2", 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player_Controller>() != null && PlayerPrefs.GetInt("Stage2") == 0)
        {
            GameObject.Find("Stage2").GetComponent<UIFadeInOut>().FadeIn();
            PlayerPrefs.SetInt("Stage2", 1);
        }
    }
}
