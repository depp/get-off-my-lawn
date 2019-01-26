using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlaceObject : MonoBehaviour {

    [SerializeField]
    private GameObject objectToPlace = null;

    [SerializeField]
    private float objectTime = 10;

    private void Update() {
        if (Input.GetButtonDown("Fire2")) {
            GameObject g = Instantiate(objectToPlace, transform.position, Quaternion.identity);
            Destroy(g, objectTime);
        }
    }

}