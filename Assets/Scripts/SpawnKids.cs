using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SpawnKids : MonoBehaviour {

    [SerializeField]
    private GameObject kidPrefab = null;

    [SerializeField]
    private float startingInterval = 1, startingSpeed = 1, percentSpeedVariation = .25f, timeBetweenWaves = 3, endGameSpawnInterval = .1f;

    public static int WaveNumber { get; private set; }

    [SerializeField]
    private Text waveText = null;

    private BoxCollider2D spawnBox = null;

    private void Awake() {
        spawnBox = GetComponent<BoxCollider2D>();
        WaveNumber = 0;
    }

    private void Start() {
        StartCoroutine(Wave(WaveNumber));
    }

    private bool endGameCoroutineStarted = false;
    private void Update() {
        if (!waveInProgress && MoveKid.AllMoveKidsCopy().All(mk => mk.RunningAway) && !EndGame.Ended) {
            WaveNumber++;
            StartCoroutine(StartWave(WaveNumber));
        }
        if (EndGame.Ended && !endGameCoroutineStarted) {
            StartCoroutine(KidSwarm());
            endGameCoroutineStarted = true;
        }

    }

    private bool waveInProgress = false;
    private IEnumerator StartWave(int wave) {
        waveInProgress = true;

        waveText.gameObject.SetActive(true);
        waveText.text = "Wave " + wave;
        yield return new WaitForSeconds(timeBetweenWaves);
        waveText.gameObject.SetActive(false);

        yield return StartCoroutine(Wave(wave));

        waveInProgress = false;
    }

    private IEnumerator Wave(int wave) {
        for (int i = 0; i < wave * 10; i++) {
            yield return new WaitForSeconds(startingInterval / wave);
            SpawnKid(startingSpeed * wave * Random.Range(1 - percentSpeedVariation, 1 + percentSpeedVariation));
        }
    }

    private IEnumerator KidSwarm() {
        while (true) {
            yield return new WaitForSeconds(endGameSpawnInterval);
            SpawnKid(10);
        }
    }

    private void SpawnKid(float speed) {
        GameObject kid = Instantiate(kidPrefab);

        //https://forum.unity.com/threads/randomly-generate-objects-inside-of-a-box.95088/#post-1263920
        Vector2 spawnPosition = new Vector2(
            Random.Range(-spawnBox.size.x, spawnBox.size.x),
            Random.Range(-spawnBox.size.y, spawnBox.size.y)
        );
        spawnPosition = spawnBox.transform.TransformPoint(spawnPosition / 2);
        kid.transform.position = spawnPosition;

        kid.GetComponent<MoveKid>().Speed = speed;
    }

}