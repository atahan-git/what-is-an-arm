using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {

    public static GameMaster s;

    public TMP_Text endText;
    public TMP_Text endTime;
    public GameObject restartButton;
    public GameObject startButton;

    public static bool isGameInProgress = false;

    public GameObject collectPrefab;

    public TMP_Text timeText;
    public float curTime;

    private void Awake() {
        s = this;
    }

    // Start is called before the first frame update
    void Start() {
        isGameInProgress = false;
        curTime = 0;
        restartButton.SetActive(false);
        endText.gameObject.SetActive(false);
        endTime.gameObject.SetActive(false);
        startButton.SetActive(true);
        timeText.gameObject.SetActive(false);
    }

    private void Update() {
        if (FindObjectOfType<enemyAI> == null) ;
        }
        if (isGameInProgress) {
            curTime += Time.deltaTime;
        }

        TimeSpan t = TimeSpan.FromSeconds(curTime);
        string nicelyFormatted = $"Time: {t.Minutes:D2}:{t.Seconds:D2}:{t.Milliseconds:D4}";
        timeText.text = nicelyFormatted;
        FindObjectOfType<
    }

    public void StartGame() {
        startButton.SetActive(false);
        timeText.gameObject.SetActive(true);
        isGameInProgress = true;
    }

    public void RestartGame() {
        isGameInProgress = false;
        restartButton.SetActive(false);
        endText.gameObject.SetActive(false);
        endTime.gameObject.SetActive(false);
        startButton.SetActive(false);
        timeText.gameObject.SetActive(false);

        SceneManager.LoadScene(0);
    }

    public void EndGame(bool winstate) {
        endText.gameObject.SetActive(winstate);
        endText.text = winstate ? "You Win!" : "You Lost!";
        endTime.text = timeText.text;
        endTime.gameObject.SetActive(true);
        restartButton.SetActive(true);
    }
}
