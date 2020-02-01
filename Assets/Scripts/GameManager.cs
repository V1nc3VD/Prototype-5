using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> targets;

    private float spawnRate = 1.0f;
    private int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;

    public bool isGameActive;
    
    public Button restartButton;

    public GameObject titleScreen;



    void Start()
    {
        StartCoroutine(SpawnTarget());
        scoreText.text = "Score: " + score;



    }

    // Update is called once per frame
    void Update()
    {

    }


    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        restartButton.gameObject.SetActive(true);
        Debug.Log("gameover");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        UpdateScore(0);
        isGameActive = true;
        
        StartCoroutine(SpawnTarget());
        score = 0;
        titleScreen.gameObject.SetActive(false);
        //spawnrate is oude spawnrate gedeeld door diffuculty, hoe lager hoe sneller dingen dus spawnen.
        spawnRate /= difficulty;
    }


}
    

