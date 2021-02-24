﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    SpriteRenderer sr;
    public Material outlineMaterial;
    public Material defaultMaterial;
    private Transform textPosition;

    private GameObject TextUI;
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        defaultMaterial = sr.material;
        //TextUI.SetActive(false);
        textPosition = GameObject.Find("TextPosition").GetComponent<Transform>();
        TextUI = GameObject.Find("TextUI");
    }

    //碰到玩家时，给outline
    public void MiaoBian()
    {
      
            sr.material = outlineMaterial;
            TextUI.transform.position = textPosition.position;
            TextUI.SetActive(true);
        //TextChange();
    }

    public void ExitMiaobian()
    {
       
            sr.material = defaultMaterial;
            TextUI.SetActive(false);
        
    }

    public void TextChange()
    {
        
      //  if ((this.tag == "Enemy") && (GameObject.Find("Weapon1").GetComponent<Weapon1>().enabled == true))
        if(this.tag == "Enemy")
        {
            Text text = TextUI.GetComponent<Text>();
            text.text = "Press E to Kill";
            
        }

        
        //if ((this.tag == "Item") && (GameObject.Find("Weapon2").GetComponent<Weapon2>().enabled == true))
        //if(this.tag == "Item")
        //{
        //    Text text = TextUI.GetComponent<Text>();
        //    text.text = "Left Click to use";
        //    TextUI.SetActive(true);
        //}
    }


}