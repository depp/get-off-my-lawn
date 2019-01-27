using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveKid : MonoBehaviour {

    public float Speed { get; set; } = 1;

    public bool RunningAway { get; private set; } = false;

    private static List<MoveKid> allMoveKids = new List<MoveKid>();

    public Action Defeated;

    public static List<MoveKid> AllMoveKidsCopy() {
        return new List<MoveKid>(allMoveKids);
    }

    public void SetRunning() {
        if (RunningAway)
            return;
        RunningAway = true;
        Action f = Defeated;
        if (f != null)
            Defeated();
    }

    private void Update() {
        transform.position += Vector3.left * (RunningAway ? -Speed * 5 : Speed) * Time.deltaTime;
        transform.localScale = new Vector3(
            RunningAway ? Mathf.Abs(transform.localScale.x) : -Mathf.Abs(transform.localScale.x),
            transform.localScale.y,
            transform.localScale.z
        );
    }

    private void Awake() {
        allMoveKids.Add(this);
    }

    private void OnDestroy() {
        allMoveKids.Remove(this);
    }

}