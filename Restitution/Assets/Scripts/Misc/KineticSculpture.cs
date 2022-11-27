using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KineticSculpture : MonoBehaviour {

    public GameObject outerRing, middleRing, innerRing;

    public float speed = 10;

    private float middleOffset;
    private float innerOffset;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        outerRing.transform.Rotate(new Vector3(0, speed * Time.deltaTime, 0));
        middleOffset += speed * 1.3f * Time.deltaTime;
		innerOffset += speed * 1.1f * Time.deltaTime;
        middleRing.transform.rotation = outerRing.transform.rotation * Quaternion.Euler(0, 0, middleOffset);
        innerRing.transform.rotation = middleRing.transform.rotation * Quaternion.Euler(0, innerOffset, 0);
	}
}
