using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SpawnKids : MonoBehaviour {

    [SerializeField]
    private GameObject kidPrefab = null;

    [SerializeField]
    private float startintInterval = 1, timeBetweenWaves = 3;

    public static int WaveNumber { get; private set; } = 0;

    [SerializeField]
    private Text waveText = null;

    private BoxCollider2D spawnBox = null;

    private void Awake() {
        spawnBox = GetComponent<BoxCollider2D>();
    }

    private void Start() {
        StartCoroutine(Wave(WaveNumber));
    }

    private void Update() {
        if (!waveInProgress && MoveKid.AllMoveKidsCopy().All(mk => mk.RunningAway) && !EndGame.Ended) {
            WaveNumber++;
            StartCoroutine(StartWave(WaveNumber));
        }
    }

    private IEnumerator StartWave(int wave) {
        waveInProgress = true;

        waveText.gameObject.SetActive(true);
        waveText.text = "Wave " + wave;
        yield return new WaitForSeconds(timeBetweenWaves);
        waveText.gameObject.SetActive(false);

        yield return StartCoroutine(Wave(wave));

        waveInProgress = false;
    }

    private bool waveInProgress = false;
    private IEnumerator Wave(int wave) {
        for (int i = 0; i < wave * 10; i++) {
            yield return new WaitForSeconds(startintInterval / wave);
            Transform t = Instantiate(kidPrefab).transform;
            //https://forum.unity.com/threads/randomly-generate-objects-inside-of-a-box.95088/#post-1263920
            Vector2 spawnPosition = new Vector2(
                Random.Range(-spawnBox.size.x, spawnBox.size.x),
                Random.Range(-spawnBox.size.y, spawnBox.size.y)
            );
            spawnPosition = spawnBox.transform.TransformPoint(spawnPosition / 2);
            t.position = spawnPosition;
        }
    }

}