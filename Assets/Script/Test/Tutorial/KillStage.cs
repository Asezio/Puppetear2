using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillStage : MonoBehaviour
{
    public static bool hasKill;
    // Start is called before the first frame update
    void Start()
    {
        hasKill = false;
    }

    public void FirstKillAI()
    {
        if (!hasKill)
        {
            StartCoroutine(FirstKill());
            hasKill = true;
        }

    }

    IEnumerator FirstKill()
    {
        GameObject.Find("Head").GetComponentInChildren<Text>().text = "Good Job";
        GameObject.Find("Head").GetComponent<UIFadeInOut>().FadeIn();

        yield return new WaitForSeconds(3);
        GameObject.Find("Head").GetComponent<UIFadeInOut>().FadeOut();
    }

}
