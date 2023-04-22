using UnityEngine;
using System;

[CreateAssetMenu(fileName = "PlayerScriptableObject", menuName="ScriptableObjects/PlayerScriptableObject")]
public class PlayerScriptableObject : ScriptableObject
{
    public int health = 3;
}