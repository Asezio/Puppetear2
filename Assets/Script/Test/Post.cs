using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Post : MonoBehaviour
{
    public bool isOpened;
    // Start is called before the first frame update
    void Awake()
    {
        isOpened = false;
    }
}