using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldManSounds : MonoBehaviour {

    [SerializeField]
    private float interval = 10;

    [SerializeField]
    private AudioClip[] clips = null;

    private IEnumerator Start() {
        while (true) {
            yield return new WaitForSeconds(interval);
            if (clips.Length > 0)
                AudioSource.PlayClipAtPoint(clips[Random.Range(0, clips.Length)], Vector3.zero);
        }
    }

}