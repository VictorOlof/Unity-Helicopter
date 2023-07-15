using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levels : MonoBehaviour {

	[SerializeField] private Level[] level;


}

[System.Serializable]
public class Level {
    [SerializeField] private LevelParameters[] levelParameters;

}