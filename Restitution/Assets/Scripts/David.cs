using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class David : MonoBehaviour
{
    private Animator davidAnimator;
    private CharacterController character_controller;
    public float walking_velocity;
    public float velocity;
    public Vector3 movement_direction;

    // Start is called before the first frame update
    void Start()
    {
        davidAnimator = GetComponent<Animator>();
        character_controller = GetComponent<CharacterController>();
        velocity = 0.0f;
        walking_velocity = 1.5f;
        movement_direction = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) 
            {
                davidAnimator.SetInteger("movement", 3);
            } else if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                davidAnimator.SetInteger("movement", 2);
            }
            else 
            {
                davidAnimator.SetInteger("movement", 1);
            }
        } else if (Input.GetKey(KeyCode.C)) 
        {
            davidAnimator.SetInteger("movement", 4);    
        }
        else
        {
            davidAnimator.SetInteger("movement", 0);
        } 

        if (davidAnimator.GetCurrentAnimatorStateInfo(0).IsName("CrouchingForward")) 
        {
            velocity += 0.1f;
            velocity = (velocity > 0.5f * walking_velocity) ? 0.75f : velocity;
        }
        else if (davidAnimator.GetCurrentAnimatorStateInfo(0).IsName("Running"))
        {
            velocity += 0.3f;
            velocity = (velocity > 2.0f * walking_velocity) ? 3.0f : velocity;
        }
        else if (davidAnimator.GetCurrentAnimatorStateInfo(0).IsName("Walking")) 
        {
            velocity += 0.1f;
            velocity = (velocity > walking_velocity) ? 1.5f : velocity;
        } else {
            velocity = 0.0f;
        }

        float xdirection = Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
        float zdirection = Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
        movement_direction = new Vector3(xdirection, 0.0f, zdirection);

        character_controller.Move(movement_direction * velocity * Time.deltaTime);
    }
}
