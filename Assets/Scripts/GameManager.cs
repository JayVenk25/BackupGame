using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static int health;
    public static int numOfEnemies;
    public Text waveTxt;
    public Text numOfEnemiesTxt;
    public Text healthTxt;
    public GameObject losePanel;
    public GameObject winPanel;

    void Awake() 
    {
        instance = this;
        health = 3;
        losePanel.SetActive(false);
        winPanel.SetActive(false);
        Time.timeScale = 1f;
        numOfEnemies = 30;
        if (PlayerPrefs.GetInt("WaveNum") <= 1)
        {
            PlayerPrefs.SetInt("WaveNum", 1);
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("WaveNum", 1);
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Shop>().Start();
        GetComponent<HealthSystem>().Start();
    }

    private void Update()
    {
        healthTxt.text = "Health: " + health.ToString();
        numOfEnemiesTxt.text = "Enemies Remaining: " + numOfEnemies.ToString();
        waveTxt.text = "Wave " + PlayerPrefs.GetInt("WaveNum");

        if (health == 0)
        {
            losePanel.SetActive(true);
            Time.timeScale = 0f;
        }

        if (numOfEnemies == 0)
        {
            PlayerPrefs.SetInt("WaveNum", PlayerPrefs.GetInt("WaveNum")+1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (PlayerPrefs.GetInt("WaveNum") == 10)
        {
            winPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
