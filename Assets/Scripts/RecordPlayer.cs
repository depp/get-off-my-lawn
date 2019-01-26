using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordPlayer : MonoBehaviour {

    [SerializeField]
    private float hitKidSpeed = -10;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Kid")) {
            other.GetComponent<MoveKid2>().Speed = hitKidSpeed;
            Destroy(other.gameObject, 10);
        }
    }

}