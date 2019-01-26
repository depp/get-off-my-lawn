using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordPlayer : MonoBehaviour {

    public bool MusicPlaying { get; set; } = false;

    [SerializeField]
    private float destroyAfter = 10;
    private float timePlayed = 0;

    [SerializeField]
    private ParticleSpawner particleSpawner = null;

    public ParticleSpawner GetParticleSpawner() {
        return particleSpawner;
    }

    private void Update() {
        if (MusicPlaying)
            timePlayed += Time.deltaTime;
        if (timePlayed >= destroyAfter)
            Destroy(gameObject);
    }

}