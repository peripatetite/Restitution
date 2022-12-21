using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour {

    private static BackgroundMusic instance;
    // Start is called before the first frame update
    void Start() {
        if (instance == null) {
            instance = this;
		} else if (instance != this) {
            Destroy(gameObject);
		}
        DontDestroyOnLoad(this);
    }
}
