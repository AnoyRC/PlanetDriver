using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public int HighScore;
    public TextMeshProUGUI HighScoreHandler;
    public GameObject MainMenu;
    public GameObject Marketplace;
    private bool toggled = false;
    public GameObject CreditPanel;
    // Start is called before the first frame update
    void Start()
    {
        HighScore = PlayerPrefs.GetInt("highscore",0);
    }

    // Update is called once per frame
    void Update()
    {
        HighScoreHandler.text = HighScore.ToString();
        if (toggled)
        {
            MainMenu.SetActive(false);
            Marketplace.SetActive(true);
        }
        else
        {
            MainMenu.SetActive(true);
            Marketplace.SetActive(false);
        }
    }

    public void Play()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void ChangeToggle()
    {
        toggled = !toggled;
    }

    public void CreditsToggle()
    {
        CreditPanel.SetActive(!CreditPanel.activeSelf);
    }
}
