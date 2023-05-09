using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int score = 0;
    private float CurrentScore = 0;
    public int highscore = 0;
    private int multiplier = 1;
    private PlayerController playerController;
    public bool hasStarted = false;
    public GameObject MeteorSpawner;
    private bool startedCoroutine =  false;
    public GameObject[] cars;

    public TextMeshProUGUI MultiplierHandler;
    public TextMeshProUGUI ScoreCard;
    public TextMeshProUGUI AlternateScoreCard;
    public TextMeshProUGUI HighScoreCard;
    public GameObject StartPanel;
    public GameObject MainPanel;
    public GameObject DeathPanel;
    // Start is called before the first frame update
    void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore", 0);
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        HighScoreCard.text = highscore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted) return;
        if (playerController.isDead)
        {
            DeathPanel.SetActive(true);
            MainPanel.SetActive(false);
            return;
        }
        MeteorSpawner.SetActive(true);
        CurrentScore += 0.3f * multiplier;
        score = (int) CurrentScore;
 
        if(score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt("highscore", score);
        }
        if(!startedCoroutine)
        {
            StartCoroutine(IncrementSpeed());
            startedCoroutine = true;
        }

        ScoreCard.text = score.ToString();
        AlternateScoreCard.text = score.ToString();
        HighScoreCard.text = highscore.ToString();
    }

    IEnumerator IncrementSpeed()
    {
        yield return new WaitForSeconds(15f);
        playerController.moveSpeed += 1;
        StartCoroutine(IncrementSpeed());
    }

    public void restartLevel()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void CarSelect(int index)
    {
        multiplier = index;
        hasStarted = true;
        StartPanel.SetActive(false);
        cars[index - 1].SetActive(true);
        MultiplierHandler.text = index.ToString()+"X";
        MainPanel.SetActive(true);
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
