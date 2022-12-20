using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class David : MonoBehaviour
{
    public float walkingVelocity;
    public float rotateSpeed;
    public Vector3 movementDirection;
    public Button ZoomOut;
    public bool allowZoom;
    public Camera playerCamera;
    public GameObject frame;
    bool zoomedout;

    public bool caught;
    public bool frozen;

    private Animator davidAnimator;
    private CharacterController davidController;
    private float regularFOV;
    private Quaternion Camera_rot;
    public AudioClip walkingAudio;
    private AudioSource davidAudio;

    private float velocity;
    private bool triggered;

    // Start is called before the first frame update
    void Start()
    {
        davidAnimator = GetComponent<Animator>();
        davidController = GetComponent<CharacterController>();
        davidAudio = GetComponent<AudioSource>();

        playerCamera = Camera.main;
        velocity = 0;
        movementDirection = new Vector3(0, 0, 0);
        allowZoom = true;
        zoomedout = true;
        ZoomOut.gameObject.SetActive(false);
        regularFOV = playerCamera.fieldOfView;
        Camera_rot = playerCamera.transform.rotation;
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
        playerCamera.transform.rotation = Camera_rot;
        playerCamera.fieldOfView = regularFOV;
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

        float targetZ = -1;

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
            } else if (Input.GetKey(KeyCode.S))
            {
                if (!zoomedout)
                    LookAway();

                //Walk Backwards
                davidAnimator.SetInteger("movement", 5);
                velocity = Mathf.Max(velocity - 0.4f, -walkingVelocity + 0.3f);
            }
            else
            {
                //Idle
                davidAnimator.SetInteger("movement", 0);
                velocity = 0;
            }   
        }

        //Rotate David right
        if (Input.GetKey(KeyCode.D))
        {
            if (!zoomedout)
                LookAway();
            transform.Rotate(new Vector3(0, 1, 0) * rotateSpeed * Time.deltaTime, Space.Self);
        }
        //Rotate David left
        if (Input.GetKey(KeyCode.A))
        {
            if (!zoomedout)
                LookAway();
            transform.Rotate(new Vector3(0, -1, 0) * rotateSpeed * Time.deltaTime, Space.Self);
        }
        movementDirection = transform.forward * velocity;
        movementDirection = transform.position.y > 0 ? movementDirection - new Vector3(0, 5, 0) : movementDirection;
        davidController.Move(movementDirection * Time.deltaTime);

        //After rotating and moving, see if the camera is in the wall and if so move it out of the wall
        Vector3 source = transform.position + new Vector3(0, playerCamera.transform.localPosition.y, 0);
        Vector3 sourceToCamera = source - playerCamera.transform.position;
        sourceToCamera.Normalize();
        RaycastHit cameraHit;
        if (Physics.Raycast(source, -transform.forward, out cameraHit, 1))
        {
            targetZ = -cameraHit.distance;
        }

        playerCamera.transform.localPosition = Vector3.Lerp(playerCamera.transform.localPosition, new Vector3(0, playerCamera.transform.localPosition.y, targetZ), 0.6f);

        if (Input.GetMouseButtonDown(0)) {
            if (zoomedout && allowZoom) {
                davidController.enabled = false;
                Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                
                if(Physics.Raycast(ray, out hit, 100))
                {
                    Camera_rot = playerCamera.transform.rotation;
                    playerCamera.transform.LookAt(hit.point);
                    playerCamera.fieldOfView = 10;
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
