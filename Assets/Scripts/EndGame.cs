using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour {

    private static EndGame Reference;

    private static float startTime;

    public static bool Ended { get; private set; }

    private float returnToMenuDelay = 5;

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
        Reference.gameOverText.gameObject.SetActive(true);
        Reference.gameOverText.text += SpawnKids.WaveNumber;
        Reference.StartCoroutine(Reference.ChangeScene());
        Reference.endGameKidsContainer.SetActive(true);
    }

    private IEnumerator ChangeScene() {
        yield return new WaitForSeconds(returnToMenuDelay);
        SceneManager.LoadScene("Main Menu");
    }

}