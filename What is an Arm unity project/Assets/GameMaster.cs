using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {


    public static GameMaster s;

    public int curScore;
    public int totalAmount = 10;
    public TMP_Text scoreText;
    public GameObject winText;
    public TMP_Text winTime;
    public GameObject restartButton;
    public GameObject startButton;

    public static bool isGameInProgress = false;

    public GameObject collectPrefab;

    public TMP_Text timeText;
    public float curTime;

    private void Awake() {
        s = this;
        totalAmount = 0;
        curScore = 0;
    }

    public void RegisterPickup() {
        totalAmount += 1;
        UpdateScore();
    }

    // Start is called before the first frame update
    void Start() {
        isGameInProgress = false;
        curTime = 0;
        UpdateScore();
        restartButton.SetActive(false);
        winText.SetActive(false);
        winTime.gameObject.SetActive(false);
        startButton.SetActive(true);
        timeText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
    }

    private void Update() {
        if (isGameInProgress) {
            curTime += Time.deltaTime;
        }

        TimeSpan t = TimeSpan.FromSeconds(curTime);
        string nicelyFormatted = $"Time: {t.Minutes:D2}:{t.Seconds:D2}:{t.Milliseconds:D4}";
        timeText.text = nicelyFormatted;
    }

    public void PickupCollected(GameObject source) {
        Destroy(Instantiate(collectPrefab, source.transform.position, Quaternion.identity), 10f);
        Destroy(source);

        curScore += 1;
        UpdateScore();
    }

    void UpdateScore() {
        scoreText.text = $"Score: {curScore}/{totalAmount}";
        if (totalAmount > 0) {
            if (curScore >= totalAmount) {
                winText.SetActive(true);
                winTime.gameObject.SetActive(true);
                timeText.gameObject.SetActive(false);

                TimeSpan t = TimeSpan.FromSeconds(curTime);
                string nicelyFormatted = $"Time: {t.Minutes:D2}:{t.Seconds:D2}:{t.Milliseconds:D4}";
                winTime.text = nicelyFormatted;
                Invoke(nameof(EnableRestart), 0.5f);
            }
        }
    }

    void EnableRestart() {
        restartButton.SetActive(true);
    }

    public void StartGame() {
        startButton.SetActive(false);
        timeText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
        isGameInProgress = true;
    }

    public void RestartGame() {
        isGameInProgress = false;
        restartButton.SetActive(false);
        winText.SetActive(false);
        winTime.gameObject.SetActive(false);
        startButton.SetActive(false);
        timeText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);

        SceneManager.LoadScene(0);
    }
}
