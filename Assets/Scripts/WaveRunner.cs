using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveRunner : MonoBehaviour
{
    public ScriptableInteger waveNumber;
    public Text waveText;
    public SpawnKids spawnKids;

    [Range(0.0f, 10.0f)]
    public float waveTextTime = 3.0f;

    void Start()
    {
        StartWave();
    }

    private void StartWave()
    {
        StartCoroutine(RunWave());
    }

    private IEnumerator RunWave()
    {
        int wave = waveNumber.value;

        waveText.gameObject.SetActive(true);
        waveText.text = string.Format("Wave {0}", wave);
        yield return new WaitForSeconds(waveTextTime);
        waveText.gameObject.SetActive(false);

        spawnKids.StartWave(wave, WaveCleared);
    }

    private void WaveCleared()
    {
        waveNumber.value++;
        StartWave();
    }
}
