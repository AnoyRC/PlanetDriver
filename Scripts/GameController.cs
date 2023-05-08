using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore", 0);
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted) return;
        if (playerController.isDead) return;
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
    }

    IEnumerator IncrementSpeed()
    {
        yield return new WaitForSeconds(15f);
        playerController.moveSpeed += 1;
        StartCoroutine(IncrementSpeed());
    }
}
