using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public int count;
    private GameObject player;
    private GameObject timeBar;
    private GameObject winPanel;
    private GameObject losePanel;
    private UIRecord record;
    private GameObject introduction;
    private GameObject levelUp;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        timeBar = GameObject.Find("TimeLeft");
        winPanel = GameObject.Find("WinPanel");
        losePanel = GameObject.Find("LosePanel");
        levelUp = GameObject.Find("LevelUp");
        record = GameObject.Find("Canvas").GetComponent<UIRecord>();
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        count = player.GetComponent<Points>().sp.scene;
        if (GameObject.Find("Introduction") != null)
        {
            introduction = GameObject.Find("Introduction");
        }
        //Debug.Log(player.GetComponent<Points>().sp.intro);
        if (GameObject.Find("CanvasTask2") != null)
        {
            //player.GetComponent<Points>().Reset();
            count = player.GetComponent<Points>().sp.scene;
        }

        if (player.GetComponent<Points>().sp.intro == false)
        {
            //Debug.Log("kere");
            Time.timeScale = 0;
        }
        else if(player.GetComponent<Points>().sp.intro == true && GameObject.Find("CanvasTask2") != null)
        {
            Time.timeScale = 1;
            //Debug.Log(Time.timeScale);
            introduction.SetActive(false);
        }
        else
        {
            Time.timeScale = 1;
            //Debug.Log(Time.timeScale + "2");
        }
    }
    void Start()
    {
        levelUp.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("1");
        //Debug.Log(UITimeBar.timeLeft);
        if (UITimeBar.timeLeft <= 0)
        {
            //Debug.Log("12");
            player.GetComponent<Player_Controller>().PlayerDead();
        }
    }

    public void NextLevel()
    {
        player.GetComponent<Points>().sp.scene++;
        count = player.GetComponent<Points>().sp.scene;
        SceneManager.LoadScene(count);
        player.GetComponent<Points>().SaveLevelData();
    }

    public void Restart()
    {        
        player.GetComponent<Points>().Restart();
        //Time.timeScale = 1;
        SceneManager.LoadScene(count);
        //Debug.Log(Time.timeScale);
    }

    public void WinPanel()
    {
        //Debug.Log("1");
        player.GetComponent<Points>().PassGame();
        winPanel.SetActive(true);
        record.BestScore();
        Time.timeScale = 0;
    }
    public void LosePanel()
    {
        losePanel.SetActive(true);
        record.CountScore();
        Time.timeScale = 0;
    }
    public void NewGame()
    {
        player.GetComponent<Points>().sp.scene = 1;
        count = 1;
        player.GetComponent<Points>().Reset();
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void ResetTimeScale()
    {
        Time.timeScale = 1;
        player.GetComponent<Points>().sp.intro = true;
        Debug.Log(player.GetComponent<Points>().sp.intro);
    }


}
