using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float timeLeft = 10f;
    private float currentCD;
    private GameObject cookie;

    private ClickManager clickManager;
    public TextMeshProUGUI timer;
    public GameObject start;
    public GameObject endGamePanel;
    public TextMeshProUGUI score;
    private Highscore prevHighscore;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        LoadFromJson(); // load prev highscore
        cookie = GameObject.Find("Cookie");
        clickManager = cookie.GetComponent<ClickManager>();
        clickManager.OnClick += Countdown;
    }

    public void Countdown()
    {
        start.SetActive(false);
        StartCoroutine(StartCountdown());
    }

    public IEnumerator StartCountdown()
    {
        clickManager.OnClick -= Countdown;
        currentCD = timeLeft;
        while (currentCD >= 0f)
        {
            yield return new WaitForSeconds(1f);
            timer.text = $"Time: {currentCD}";
            currentCD--;
        }

        DisplayEndGame();
        Debug.Log($"previous HS: {prevHighscore.score}");
        Highscore scoreAchieved = new Highscore();
        scoreAchieved.score = Convert.ToInt32(score.text);
        if (scoreAchieved.score > prevHighscore.score)
        {
            SaveToJson(scoreAchieved);
        }
    }

    void DisplayEndGame()
    {
        endGamePanel.SetActive(true);
        cookie.SetActive(false);
    }

    public void RestartGame()
    {
        endGamePanel.SetActive(false);
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        clickManager.OnClick -= Countdown;
        SceneManager.LoadScene("MainMenu");
    }

    public void SaveToJson(Highscore score)
    {
        string scoreData = JsonUtility.ToJson(score);
        string filePath = Application.persistentDataPath + "/Highscores.json";
        Debug.Log(filePath);
        System.IO.File.WriteAllText(filePath, scoreData);
        Debug.Log("save file");
        Debug.Log(scoreData);
    }

    public void LoadFromJson()
    {
        string filePath = Application.persistentDataPath + "/Highscores.json";
        string loadedData = System.IO.File.ReadAllText(filePath);

        prevHighscore = JsonUtility.FromJson<Highscore>(loadedData);
        Debug.Log($"Load complete {prevHighscore.score}");
    }
}