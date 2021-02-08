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
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        timeBar = GameObject.Find("TimeLeft");
        winPanel = GameObject.Find("WinPanel");
        losePanel = GameObject.Find("LosePanel");
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        count = player.GetComponent<Points>().sp.scene;
        if (GameObject.Find("CanvasTask2") != null)
        {
            player.GetComponent<Points>().Reset();
            count = player.GetComponent<Points>().sp.scene;
        }

    }
    //void Start()
    //{
    //    if (GameObject.Find("CanvasTask2") != null)
    //    {
    //        player.GetComponent<Points>().Reset();
    //    }
    //}
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("1");
        //Debug.Log(UITimeBar.timeLeft);
        if (UITimeBar.timeLeft <= 0)
        {
            //Debug.Log("12");
            LosePanel();
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
        SceneManager.LoadScene(count);
    }

    public void WinPanel()
    {
        player.GetComponent<Points>().PassGame();
        timeBar.GetComponent<UITimeBar>().isActive = false;
        winPanel.SetActive(true);
    }
    public void LosePanel()
    {
        //losePanel.SetActive(true);
    }
    public void NewGame()
    {
        player.GetComponent<Points>().sp.scene = 1;
        count = 1;
        player.GetComponent<Points>().Reset();
        SceneManager.LoadScene(1);
    }

    
}
