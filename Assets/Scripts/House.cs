using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class House : MonoBehaviour {

    public static GameObject Reference { get; private set; }

    private int health = 100;

    [SerializeField]
    private Text houseText = null;

    private void Awake() {
        Reference = gameObject;
        SetHealthText();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Kid")) {
            Destroy(other.gameObject);
            health -= other.GetComponent<KidDamage>().Damage;
            SetHealthText();
            if (health <= 0 && !EndGame.Ended)
                EndGame.End();
        }
    }

    private void SetHealthText() {
        houseText.text = Mathf.Max(health, 0).ToString();
    }

}