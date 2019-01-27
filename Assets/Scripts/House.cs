using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class House : MonoBehaviour {

    public static GameObject Reference { get; private set; }

    private int health = 100;

    [SerializeField]
    private Slider healthSlider = null;

    [SerializeField]
    private SpriteRenderer goodHouse = null, mediumHouse = null, badHouse = null;

    [SerializeField]
    private float mediumThreshold = 60, badThreshold = 20;

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
            if (health <= badThreshold)
                ChangeHouses(badHouse);
            else if (health <= mediumThreshold)
                ChangeHouses(mediumHouse);
        }
    }

    private void ChangeHouses(SpriteRenderer sr) {
        if (badHouse.gameObject.activeSelf != (sr == badHouse))
            badHouse.gameObject.SetActive(sr == badHouse);
        if (badHouse.gameObject.activeSelf != (sr == mediumHouse))
            mediumHouse.gameObject.SetActive(sr == mediumHouse);
        if (badHouse.gameObject.activeSelf != (sr == goodHouse))
            goodHouse.gameObject.SetActive(sr == goodHouse);
    }

    private void SetHealthText() {
        healthSlider.value = Mathf.Clamp(health, 0, 100);
        if (healthSlider.value == 0)
            healthSlider.fillRect.gameObject.SetActive(false);
    }

}