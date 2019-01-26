using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordPlayer : MonoBehaviour {

    public bool MusicPlaying { get; set; } = false;

    [SerializeField]
    private float destroyAfter = 10;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Kid") && MusicPlaying) {
            other.GetComponent<MoveKid>().SetRunning();
            Destroy(other.gameObject, 10);
        }
    }

    private void Start() {
        Destroy(gameObject, destroyAfter);
    }

}