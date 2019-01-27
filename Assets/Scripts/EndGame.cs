using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour {

    private static EndGame Reference;

    private static float startTime;

    public static bool Ended { get; private set; }

    [SerializeField]
    private Text gameOverText = null;

    [SerializeField]
    private GameObject endGameKidsContainer = null;

    private void Awake() {
        startTime = Time.time;
        Ended = false;
        Reference = this;
    }

    public static float GetTime() {
        return Time.time - startTime;
    }

    public static void End() {
        Ended = true;
        Reference.gameOverText.transform.parent.gameObject.SetActive(true);
        Reference.gameOverText.text += 5;
        Reference.endGameKidsContainer.SetActive(true);
    }

    private void Update() {
        if (Ended && Input.GetButtonDown("All Players Button 1"))
            SceneManager.LoadScene("Main Menu");
    }

}