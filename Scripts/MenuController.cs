using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public int HighScore;
    public TextMeshProUGUI HighScoreHandler;
    // Start is called before the first frame update
    void Start()
    {
        HighScore = PlayerPrefs.GetInt("highscore",0);
    }

    // Update is called once per frame
    void Update()
    {
        HighScoreHandler.text = HighScore.ToString();
    }

    public void Play()
    {
        SceneManager.LoadScene("MainGame");
    }
}
