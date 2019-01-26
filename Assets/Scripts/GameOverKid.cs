using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverKid : MonoBehaviour {

    [SerializeField]
    private Camera mainCamera = null;

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private float speed = 10;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (mainCamera.WorldToViewportPoint(transform.position).x < -.001f) {
            Vector3 newPosition = transform.position;
            newPosition.x = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
            transform.position = newPosition;
        }
    }

}