using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneStart : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame


    public void NextLevel()
    {
        SceneManager.LoadScene(1);
        //BackgroundMusic.instance.audioSource.Stop();
        //BackgroundMusic.instance.ChangeBGM();
        SoundManager.instance.PlaySound("click");
    }

    public void QuitGame()
    {
        /*将状态设置false才能退出游戏*/
        //UnityEditor.EditorApplication.isPlaying = false;
        SoundManager.instance.PlaySound("click");

        Application.Quit();
    }
}

