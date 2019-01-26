using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveKid : MonoBehaviour {

    [SerializeField]
    private float speed = 1;
    public float Speed { get { return speed; } set { speed = value; } }

    private void Update() {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

}