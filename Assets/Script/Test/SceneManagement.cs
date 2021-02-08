using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public int count = 1;
    private GameObject player;
    private GameObject timeBar;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        timeBar = GameObject.Find("TimeLeft");
    }
    // Update is called once per frame
    void Update()
    {
        if (UITimeBar.timeLeft <= 0)
        {
            Restart();
        }
    }

    public void NextLevel()
    {
        count++;
        SceneManager.LoadScene(count);
        player.GetComponent<Points>().SaveLevelData();
    }

    public void Restart()
    {
        SceneManager.LoadScene(count);
        player.GetComponent<Points>().Restart();
    }

    public void Win()
    {
        player.GetComponent<Points>().PassGame();
        timeBar.GetComponent<UITimeBar>().isActive = false;
        //GetComponentInChildren<>
    }
}
