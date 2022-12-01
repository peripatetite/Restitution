using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class David : MonoBehaviour
{
    private Animator davidAnimator;
    private CharacterController davidController;
    public float walkingVelocity;
    public float velocity;
    public Vector3 movementDirection;

    public bool caught;

    // Start is called before the first frame update
    void Start()
    {
        davidAnimator = GetComponent<Animator>();
        davidController = GetComponent<CharacterController>();
        velocity = 0;
        walkingVelocity = 1.5f;
        movementDirection = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (caught)
        {
            davidAnimator.SetTrigger("hasLost");
            return;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            //Lower the collider
            davidController.center = new Vector3(davidController.center.x, 0.58f, davidController.center.z);
            davidController.height = 1f;
            if (Input.GetKey(KeyCode.W))
            {
                //Crouch walk forwards
                davidAnimator.SetInteger("movement", 3);
                velocity = Mathf.Min(velocity + 0.25f, walkingVelocity / 2);
            }
            else
            {
                //Crouch Idle
                velocity = 0;
                davidAnimator.SetInteger("movement", 4);
            }
        }
        else  
        {
            //David is standing
            davidController.center = new Vector3(davidController.center.x, 1, davidController.center.z);
            davidController.height = 1.9f;
            if (Input.GetKey(KeyCode.W))
            {
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    //Run
                    davidAnimator.SetInteger("movement", 2);
                    velocity = Mathf.Min(velocity + 1, walkingVelocity * 2);
                }
                else
                {
                    //Walk
                    davidAnimator.SetInteger("movement", 1);
                    velocity = Mathf.Min(velocity + 0.5f, walkingVelocity);
                }
            }
            else
            {
                //Idle
                velocity = 0;
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
        movementDirection = transform.position.y > 0 ? transform.forward - new Vector3(0, 5, 0) : transform.forward;
        davidController.Move(movementDirection * velocity * Time.deltaTime);
    }
}
