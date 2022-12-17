using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class David : MonoBehaviour
{
    private Animator davidAnimator;
    private CharacterController davidController;
    private float regularFOV;
    private Quaternion Camera_rot;
    public AudioClip walkingAudio;
    private AudioSource davidAudio;

    public float walkingVelocity;
    public float velocity;
    public Vector3 movementDirection;
    public Button ZoomOut;
    bool zoomedout;

    public bool caught;
    public bool frozen;

    private bool triggered;

    // Start is called before the first frame update
    void Start()
    {
        davidAnimator = GetComponent<Animator>();
        davidController = GetComponent<CharacterController>();
        davidAudio = GetComponent<AudioSource>(); 

        velocity = 0;
        walkingVelocity = 1.5f;
        movementDirection = new Vector3(0, 0, 0);
        zoomedout = true;
        ZoomOut.gameObject.SetActive(false);
        regularFOV = Camera.main.fieldOfView;
        Camera_rot = Camera.main.transform.rotation;
        ZoomOut.onClick.AddListener(LookAway);

        frozen = false;
    }

    void Step() {
        davidAudio.PlayOneShot(walkingAudio, 0.1f);
    }

    void crouchStep() {
        davidAudio.PlayOneShot(walkingAudio, 0.05f);
    }

    void runStep() {
        davidAudio.PlayOneShot(walkingAudio, 0.2f);
    }

    void LookAway() 
    {
        Camera.main.transform.rotation = Camera_rot;
        Camera.main.fieldOfView = regularFOV;
        zoomedout = true;
        ZoomOut.gameObject.SetActive(false);
        foreach(Renderer rend in GetComponentsInChildren<Renderer>(true))
        {
            rend.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (caught)
        {
            if (!triggered)
            {
                davidAnimator.SetTrigger("hasLost");
                triggered = true;
            }
            if (!zoomedout)
            {
                LookAway();
            }
            return;
        }

        if (frozen)
        {
            return;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (!zoomedout)
                LookAway();
            //Lower the collider
            davidController.center = new Vector3(davidController.center.x, 0.58f, davidController.center.z);
            davidController.height = 1f;
            if (Input.GetKey(KeyCode.W))
            {
                //Crouch walk forwards
                davidAnimator.SetInteger("movement", 3);
                velocity = Mathf.Min(velocity + 0.35f, walkingVelocity / 2);
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
                if (!zoomedout)
                    LookAway();
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
            if (!zoomedout)
                LookAway();
            transform.Rotate(new Vector3(0, 3f, 0), Space.Self);
        }
        //Rotate David left
        if (Input.GetKey(KeyCode.A))
        {
            if (!zoomedout)
                LookAway();
            transform.Rotate(new Vector3(0, -3f, 0), Space.Self);
        }
        movementDirection = transform.position.y > 0 ? transform.forward - new Vector3(0, 5, 0) : transform.forward;
        davidController.Move(movementDirection * velocity * Time.deltaTime);
        

        if (Input.GetMouseButtonDown(0)) {
            if (zoomedout) {
                davidController.enabled = false;
                Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
                RaycastHit hit;
                
                if( Physics.Raycast( ray, out hit, 100 ) )
                {
                    Camera_rot = Camera.main.transform.rotation;
                    Camera.main.transform.LookAt(hit.point);
                    Camera.main.fieldOfView = 10;
                    foreach(Renderer rend in GetComponentsInChildren<Renderer>(true))
                    {
                        rend.enabled = false;
                    }
                    ZoomOut.gameObject.SetActive(true);
                    zoomedout = false; 
                }
                
                davidController.enabled = true; 
            } 
        }
    }
}
