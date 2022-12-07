using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public GameObject player;
    public static LevelManager instance;

	private void Awake() {
        instance = this;
	}
}
