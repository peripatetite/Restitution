using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuirpManager : MonoBehaviour {

    public TextMeshProUGUI quirpDisplay;
    public float quirpDuration = 5f;

    private Queue<string> quirpQueue;
    private float quirpTimer;
	private string previousQuirp;

    // Start is called before the first frame update
    void Start() {
        quirpQueue = new Queue<string>();
        quirpTimer = -1;
		previousQuirp = "";
    }

    // Update is called once per frame
    void Update() {
        if (quirpTimer > 0) {
            quirpTimer -= Time.deltaTime;
        } else if (quirpQueue.Count > 0) {
			string text = quirpQueue.Dequeue();
            quirpDisplay.text = text;
            quirpTimer = (text == previousQuirp) ? 0 : quirpDuration / Mathf.Max(1, quirpQueue.Count - 1);
			previousQuirp = text;
		} else {
            quirpDisplay.text = "";
		}
    }

    public void AddQuirp(string quirp) {
        quirpQueue.Enqueue(quirp);
	}
}
