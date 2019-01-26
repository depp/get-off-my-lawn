using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveKid : MonoBehaviour {

    [SerializeField]
    private float speed = 1;

    public bool RunningAway { get; private set; } = false;

    private static List<MoveKid> allMoveKids = new List<MoveKid>();

    public static List<MoveKid> AllMoveKidsCopy() {
        return new List<MoveKid>(allMoveKids);
    }

    public void SetRunning() {
        RunningAway = true;
    }

    private void Update() {
        transform.position += Vector3.left * (RunningAway ? -speed * 5 : speed) * Time.deltaTime;
    }

    private void Awake() {
        allMoveKids.Add(this);
    }

    private void OnDestroy() {
        allMoveKids.Remove(this);
    }

}