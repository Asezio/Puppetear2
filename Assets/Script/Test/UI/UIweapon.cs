using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIweapon : MonoBehaviour
{
    public Text coolDownText;
    private float coolDownTime;
    private float timer;
    public bool isReady;

    //public bool isActive;

    public Image WeaponImage;
    public Image WeaponImageGrey;

    // Start is called before the first frame update
    void Awake()
    {
        coolDownText.enabled = false;
        if(gameObject.name==("UI_Weapon1"))
        {
            WeaponImageGrey.enabled = false;
        }
        coolDownTime = GameObject.Find("Weapon1").GetComponent<WeaponBase>().cooldown;
        isReady = true;
    }

    // Update is called once per frame

    protected void Cooldown()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            WeaponImageGrey.fillAmount = timer / coolDownTime;
            coolDownText.text = timer.ToString();
            isReady = false;
            //Debug.Log("233");
        }

        if (timer <= 0f)
        {
            timer = 0f;
            coolDownText.enabled = false;
            WeaponImageGrey.enabled = false;
            isReady = true;
        }
        
    }

    public void Activate()
    {
        WeaponImageGrey.enabled = true;
        coolDownText.enabled = true;
        timer = coolDownTime;
    }
}
