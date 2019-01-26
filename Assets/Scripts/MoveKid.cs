using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveKid : MonoBehaviour {

    public float Speed { get; set; } = 1;

    public bool RunningAway { get; private set; } = false;

    private static List<MoveKid> allMoveKids = new List<MoveKid>();

    public static List<MoveKid> AllMoveKidsCopy() {
        return new List<MoveKid>(allMoveKids);
    }

    public void SetRunning() {
        RunningAway = true;
    }

    private void Update() {
        transform.position += Vector3.left * (RunningAway ? -Speed * 5 : Speed) * Time.deltaTime;
    }

    private void Awake() {
        allMoveKids.Add(this);
    }

    private void OnDestroy() {
        allMoveKids.Remove(this);
    }

}