using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class David : MonoBehaviour
{
    private Animator davidAnimator;
    private CharacterController characterController;
    public float walkingVelocity;
    public float velocity;
    public Vector3 movementDirection;

    // Start is called before the first frame update
    void Start()
    {
        davidAnimator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        velocity = 0.0f;
        walkingVelocity = 1.5f;
        movementDirection = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
            {
                //Crouch walk forwards
                Debug.Log(characterController.bounds.center);
                davidAnimator.SetInteger("movement", 3);
                velocity = Mathf.Min(velocity + 0.25f, walkingVelocity / 2);
            }
            else if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                //Run
                davidAnimator.SetInteger("movement", 2);
                velocity = Mathf.Min(velocity + 1, walkingVelocity * 2);
            }
            else
            {

                Debug.Log(characterController.bounds.center);
                //Walk
                davidAnimator.SetInteger("movement", 1);
                velocity = Mathf.Min(velocity + 0.5f, walkingVelocity);
            }
        }
        else
        {
            velocity = 0;
            if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
            {
                //Crouch Idle
                davidAnimator.SetInteger("movement", 4);
            }
            else
            {
                //Idle
                davidAnimator.SetInteger("movement", 0);
            }
        }

        //Rotate David right
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0, 5f, 0), Space.Self);
        }
        //Rotate David left
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0, -5f, 0), Space.Self);
        }

        float xdirection = Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
        float zdirection = Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
        movementDirection = new Vector3(xdirection, 0.0f, zdirection);

        characterController.Move(movementDirection * velocity * Time.deltaTime);
    }
}
