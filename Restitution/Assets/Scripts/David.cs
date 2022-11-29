using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class David : MonoBehaviour
{
    private Animator davidAnimator;
    // Start is called before the first frame update
    void Start()
    {
        davidAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKey(KeyCode.W))
       {
            davidAnimator.SetInteger("movement", 1);
       } else
        {
            davidAnimator.SetInteger("movement", 0);
        }
    }
}
