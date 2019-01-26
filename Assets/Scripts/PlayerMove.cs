﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    [SerializeField]
    private float speed = 10;

    private Rigidbody2D rb;

    public int PlayerNumber { get; set; }

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        Vector2 direction;
        direction.x = Input.GetAxisRaw("P" + PlayerNumber + " Horizontal");
        direction.y = Input.GetAxisRaw("P" + PlayerNumber + " Vertical");
        Vector3 move = Vector3.ClampMagnitude(direction, 1) * speed;
        rb.velocity = move;
    }

}