using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour {

    public static GameObject Reference { get; private set; }

    private int health = 100;

    private void Awake() {
        Reference = gameObject;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Kid")) {
            Destroy(other.gameObject);
            health -= other.GetComponent<KidDamage>().Damage;
        }
    }

}