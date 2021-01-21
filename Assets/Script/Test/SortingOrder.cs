using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingOrder : MonoBehaviour
{
    private SpriteRenderer sr;
    private Transform trans;
    // Start is called before the first frame update
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        trans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        OrderInLayer();
    }
    protected void OrderInLayer()//改变角色层级
    {
        float y = trans.position.y * (-10);
        sr.sortingOrder = Mathf.RoundToInt(y);
    }

}
