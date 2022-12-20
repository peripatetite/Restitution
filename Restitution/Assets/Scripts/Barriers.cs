using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barriers : MonoBehaviour
{
    private GameObject Barrier1;
    public bool disabled1;
    // Start is called before the first frame update
    void Start()
    {
        Barrier1 = GameObject.Find("Barrier1");
        disabled1 = false;
    }

    // Update is called once per frame
    void Update()
    {   
        if (disabled1)
        {
            Transform[] children = Barrier1.GetComponentsInChildren<Transform>();
            foreach (Transform child in children)
            {
                if (child.name.Contains("Beam"))
                {
                    Renderer beamRenderer = child.GetComponent<Renderer>();
                    beamRenderer.enabled = false;
                }
            }
            Barrier1.GetComponent<Collider>().enabled = false;
        }

    }
}
