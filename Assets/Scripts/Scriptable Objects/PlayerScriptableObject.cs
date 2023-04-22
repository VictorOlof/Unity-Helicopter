using UnityEngine;
using System;

[CreateAssetMenu(fileName = "PlayerScriptableObject", menuName="ScriptableObjects/PlayerScriptableObject")]
public class PlayerScriptableObject : ScriptableObject
{
    public State state;
    public enum State
    {
        WaitingToStart,
        Playing,
        Crashed,
        Dead
    }

    public int health = 3;
}