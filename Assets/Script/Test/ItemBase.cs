﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemBase : MonoBehaviour
{
    protected SpriteRenderer sr;
    
    //public bool startmove;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        
        //startmove = true;
    }

    //public void StartMove()
    //{
    //    Invoke("ChangeStickTrue", duration);
    //    startmove = false;
    //}

    
}
