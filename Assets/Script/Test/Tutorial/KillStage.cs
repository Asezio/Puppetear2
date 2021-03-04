using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillStage : MonoBehaviour
{
    public static bool hasKill;
    public static bool killDoorKeeper;
    // Start is called before the first frame update
    void Start()
    {
        hasKill = false;
        killDoorKeeper = false;
    }

    public void FirstKillAI()
    {
        if (!hasKill)
        {
            StartCoroutine(FirstKill());
            hasKill = true;
        }

    }

    public void FirstKillDoorKeeper()
    {
        if (!killDoorKeeper)
        {
            StartCoroutine(KillDoorKeeper());
            killDoorKeeper = true;
        }
    }

    IEnumerator KillDoorKeeper()
    {
        GameObject.Find("FinishedText").GetComponent<UIFadeInOut>().FadeIn();
        GameObject.Find("TaskPanel01").GetComponent<Animator>().SetBool("isSpread", true);

        yield return new WaitForSeconds(1);
        GameObject.Find("TaskPanel01").GetComponent<Animator>().SetBool("isSpread", false);
        GameObject.Find("FinishedText").GetComponent<UIFadeInOut>().FadeOut();
    }

    IEnumerator FirstKill()
    {
        GameObject.Find("Head").GetComponentInChildren<Text>().text = "Good Job";
        GameObject.Find("Head").GetComponent<UIFadeInOut>().FadeIn();

        yield return new WaitForSeconds(1);
        GameObject.Find("Head").GetComponent<UIFadeInOut>().FadeOut();
    }

}
