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

    [SerializeField]
    private Transform spawnpointsContainer = null;
    private Transform[] spawnpoints = null;

    public static int WaveNumber { get; private set; } = 0;

    [SerializeField]
    private Text waveText = null;

    private void Awake() {
        spawnpoints = spawnpointsContainer.GetComponentsInChildren<Transform>().Where(s => s.parent).ToArray();
    }

    private void Start() {
        StartCoroutine(Wave(WaveNumber));
    }

    private void Update() {
        if (!waveInProgress && MoveKid.AllMoveKidsCopy().All(mk => mk.RunningAway)) {
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
            t.position = spawnpoints[Random.Range(0, spawnpoints.Length)].position;
        }
    }

}