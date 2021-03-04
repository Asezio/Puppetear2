using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SKGShine : MonoBehaviour
{
    private SpriteRenderer sr;
    public ItemMoveable card;
    private bool flag;
    // Start is called before the first frame update
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        flag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(card!=null)
        {
            if (card.startDetect == true && flag == false)
            {
                StartCoroutine(Shine());
            }
        }
    }

    IEnumerator Shine()
    {
        flag = true;
        sr.enabled = true;
        yield return new WaitForSeconds(1f);
        StartCoroutine(StopShine());
    }

    IEnumerator StopShine()
    {
        sr.enabled = false;
        yield return new WaitForSeconds(1f);
        flag = false;
    }
}
