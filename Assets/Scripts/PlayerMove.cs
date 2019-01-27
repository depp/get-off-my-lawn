using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    [SerializeField]
    private float speed = 10;

    private Rigidbody2D rb;

    public int PlayerNumber { get; set; }

    [SerializeField]
    private GameObject runningLegs = null, idleLegs = null;

    private float originalLocalScaleX;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        originalLocalScaleX = transform.localScale.x;
    }

    private void Update() {
        Vector2 direction;
        direction.x = Input.GetAxisRaw("P" + PlayerNumber + " Horizontal");
        direction.y = Input.GetAxisRaw("P" + PlayerNumber + " Vertical");
        Vector2 move = Vector2.ClampMagnitude(direction, 1) * speed;
        rb.velocity = move;

        if (runningLegs.activeSelf != (move != Vector2.zero))
            runningLegs.SetActive(move != Vector2.zero);
        if (idleLegs.activeSelf != (move == Vector2.zero))
            idleLegs.SetActive(move == Vector2.zero);

        if (move.x < 0)
            transform.localScale = new Vector3(-originalLocalScaleX, transform.localScale.y, transform.localScale.z);
        else if (move.x > 0)
            transform.localScale = new Vector3(originalLocalScaleX, transform.localScale.y, transform.localScale.z);

    }

}