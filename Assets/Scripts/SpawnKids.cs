using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SpawnKids : MonoBehaviour {
    private class Wave
    {
        private readonly System.Action cleared;
        private readonly float moveSpeedMin, moveSpeedMax;
        private readonly int count;
        private readonly float spawnRate;

        private float phase;
        private int spawnCount;
        private int defeatCount;

        public Wave(GameParameters parameters, int waveNumber, System.Action cleared)
        {
            this.cleared = cleared;
            float n = waveNumber - 1;
            float moveSpeed = parameters.kidMoveSpeedStart + n * parameters.kidMoveSpeedStep;
            moveSpeedMin = moveSpeed * (1.0f - parameters.kidMoveSpeedVariation);
            moveSpeedMax = moveSpeed * (1.0f + parameters.kidMoveSpeedVariation);
            count = (int)Mathf.Round(parameters.kidCountStart + n * parameters.kidCountStep);
            spawnRate = parameters.kidSpawnRateStart + n * parameters.kidSpawnRateStep;
        }

        public void Update(SpawnKids spawn)
        {
            phase += spawnRate * Time.deltaTime;
            int n = Mathf.Min(this.count - this.spawnCount, (int)phase);
            phase -= n;
            for (int i = 0; i < n; i++)
            {
                MoveKid kid = spawn.SpawnKid();
                this.spawnCount++;
                kid.Speed = Random.Range(moveSpeedMin, moveSpeedMax);
                kid.Defeated = KidDefeated;
            }
        }

        private void KidDefeated()
        {
            defeatCount++;
            if (defeatCount == count)
                cleared();
        }
    }

    [SerializeField]
    private GameParameters parameters = null;

    [SerializeField]
    private GameObject kidPrefab = null;

    private BoxCollider2D spawnBox = null;

    private Wave wave;

    private void Awake()
    {
        spawnBox = GetComponent<BoxCollider2D>();
        if (spawnBox == null)
            Debug.LogError("Missing spawn box");
    }

    public void StartWave(int waveNumber, System.Action cleared)
    {
        Debug.Log("START WAVE");
        Wave newWave = null;
        newWave = new Wave(parameters, waveNumber, delegate
        {
            if (wave == newWave)
            {
                wave = null;
                cleared();
            }
        });
        wave = newWave;
    }

    private void Update()
    {
        if (wave != null)
            wave.Update(this);
    }
        
    private MoveKid SpawnKid()
    {
        GameObject kid = Instantiate(kidPrefab);

        //https://forum.unity.com/threads/randomly-generate-objects-inside-of-a-box.95088/#post-1263920
        Vector2 spawnPosition = new Vector2(
            Random.Range(-spawnBox.size.x, spawnBox.size.x),
            Random.Range(-spawnBox.size.y, spawnBox.size.y)
        );
        spawnPosition = spawnBox.transform.TransformPoint(spawnPosition / 2);
        kid.transform.position = spawnPosition;

        return kid.GetComponent<MoveKid>();
    }
}