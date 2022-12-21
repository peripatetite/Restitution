using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuirpManager : MonoBehaviour {

    public TextMeshProUGUI quirpDisplay;
    public float quirpDuration = 3f;

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
            quirpTimer = (text == previousQuirp) ? 0 : quirpDuration / Mathf.Max(1, quirpQueue.Count-2);
            if (quirpTimer > 0)
			    quirpDisplay.text = text;
			previousQuirp = text;
            Debug.Log("Dequequed, " + quirpQueue.Count);
		} else {
            quirpDisplay.text = "";
            previousQuirp = "";
		}
    }

    public void AddQuirp(string quirp) {
        quirpQueue.Enqueue(quirp);
	}
}
