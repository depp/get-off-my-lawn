using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Game Parameters")]
public class GameParameters : ScriptableObject
{
    // Speed at which kids move.
    public float kidMoveSpeedStart = 1.0f;
    public float kidMoveSpeedStep = 1.0f;
    public float kidMoveSpeedVariation = 0.25f;

    // Number of kids in a wave.
    public float kidCountStart = 10.0f;
    public float kidCountStep = 10.0f;

    // Rate at which kids spawn, kids per second.
    public float kidSpawnRateStart = 1.0f;
    public float kidSpawnRateStep = 1.0f;
}
