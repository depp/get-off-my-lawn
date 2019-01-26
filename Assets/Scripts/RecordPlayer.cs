using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordPlayer : MonoBehaviour {

    public bool MusicPlaying { get; set; } = false;

    public int Carrying { get; set; } = 0;//the player number that's carrying it. 0 if none.

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Kid") && MusicPlaying) {
            other.GetComponent<MoveKid>().SetRunning();
            Destroy(other.gameObject, 10);
        }
    }

    private void Update() {
        if (Carrying == 0)
            return;
        transform.position = Players.GetPlayersCopy()[Carrying - 1].transform.position;
    }

}