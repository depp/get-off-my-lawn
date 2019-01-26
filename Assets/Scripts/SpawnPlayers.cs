using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour {

    [SerializeField]
    private GameObject playerPrefab = null;

    [SerializeField]
    private Transform spawnpointsContainer = null;
    private Transform[] spawnpoints = null;

    private void Awake() {
        spawnpoints = spawnpointsContainer.GetComponentsInChildren<Transform>().Where(t => t.parent == spawnpointsContainer).ToArray();
    }

    private void Start() {
        int playerCount = Mathf.Max(MenuActions.PlayerCount, 1);
        List<Transform> spawnpointsCopy = new List<Transform>(spawnpoints);
        for (int i = 0; i < playerCount; i++) {
            int randomIndex = Random.Range(0, spawnpointsCopy.Count);
            GameObject player = Instantiate(playerPrefab, spawnpointsCopy[randomIndex].position, Quaternion.identity);
            player.GetComponent<PlayerMove>().PlayerNumber = i + 1;
            player.GetComponent<PlayerMelee>().PlayerNumber = i + 1;
            spawnpointsCopy.RemoveAt(randomIndex);
        }
    }

}