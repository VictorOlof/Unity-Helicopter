using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ControllerScriptObj", menuName="ScriptableObjects/Controller2")]
public class Controller2ScriptableObject : ScriptableObject
{
    [SerializeField]
    public bool startMode = true;
    //{get; private set; } = true;

}