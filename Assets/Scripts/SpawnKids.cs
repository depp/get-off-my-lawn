using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnKids : MonoBehaviour {

    [SerializeField]
    private GameObject kidPrefab = null;

    [SerializeField]
    private float interval = 1;

    [SerializeField]
    private Transform spawnpointsContainer = null;
    private Transform[] spawnpoints = null;

    private void Awake() {
        spawnpoints = spawnpointsContainer.GetComponentsInChildren<Transform>().Where(s => s.parent).ToArray();
    }

    private IEnumerator Start() {
        while (true) {
            yield return new WaitForSeconds(interval);
            Transform t = Instantiate(kidPrefab).transform;
            t.position = spawnpoints[Random.Range(0, spawnpoints.Length)].position;
        }
    }

}