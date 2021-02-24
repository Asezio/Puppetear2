using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drinker : MonoBehaviour
{
    public bool isPoisoned;
    // Start is called before the first frame update
    void Start()
    {
        isPoisoned = false;
    }

    // Update is called once per frame
    void Update()
    {
        isPoisoned = GetComponent<ItemStatic>().isPoisoned;
    }

    
}
