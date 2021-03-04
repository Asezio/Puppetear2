using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Post : MonoBehaviour
{
    public bool isOpened;
    private GameObject postUI;
    public Image postImage;
    // Start is called before the first frame update
    void Awake()
    {
        postUI = GameObject.Find("PostPanel");
        isOpened = false;
    }

    void Start()
    {
        postUI.SetActive(false);
    }

    public void ShowPost(bool flag)
    {
        postUI.SetActive(flag);
        postImage.enabled = flag;
        if (flag == true)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}