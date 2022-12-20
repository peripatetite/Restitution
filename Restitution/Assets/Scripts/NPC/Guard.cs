using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    public List<Transform> waypoints;
    public AudioClip shout;

    private GameObject replay;
    private NavMeshAgent guardAgent;
    private Animator guardAnimator;
    private GameObject david;
    private CharacterController characterController;
    private Vector3 davidCentroid;
    private David davidScript;
    private Action<bool> state;
    
    private AudioSource guardAudio;

    private int index = 1;
    private bool moving = true;
    private bool chasing;
    private bool caught;

    // Start is called before the first frame update
    void Start()
    {
        replay = LevelManager.instance.replayButton;
        guardAgent = GetComponent<NavMeshAgent>();
        guardAnimator = GetComponent<Animator>();
        david = GameObject.Find("David");
        characterController = david.GetComponent<CharacterController>();
        davidScript = david.GetComponent<David>();
        guardAudio = GetComponent<AudioSource>();

        state = Patrolling;
        guardAgent.destination = waypoints[index].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!caught)
        {
            Vector3 guardDirection = transform.forward; //which direction the guard is looking
            Vector3 guardView = transform.position + new Vector3(0, 1.5f, 0); //raised because the guard's eyes are not on the floor
            davidCentroid = characterController.bounds.center;
            Vector3 direction_from_guard_to_david = davidCentroid - guardView;
            direction_from_guard_to_david.Normalize();
            bool inView = Vector3.Angle(guardDirection, direction_from_guard_to_david) < 60;
            RaycastHit hit;
            if (inView
                && Physics.Raycast(guardView, direction_from_guard_to_david, out hit, Vector3.Distance(transform.position, davidCentroid) + 1)
                && hit.collider.gameObject == david)
            {
                state(true);
            }
            else
            {
                state(false);
            }
        } else
        {
            state(true);
        }
    }

    IEnumerator PatrolNextLocation()
    {
        index = (index + 1) % waypoints.Count;
        yield return new WaitForSeconds(UnityEngine.Random.Range(2f, 6f));
        guardAgent.destination = waypoints[index].position;
        moving = true;
    }

    void Patrolling(bool spotted)
    {
        if (spotted)
        {
            guardAudio.PlayOneShot(shout, 0.7F);
            if (Vector3.Distance(transform.position, david.transform.position) < 1)
            {
                StopCoroutine("PatrolNextLocation");
                caught = true;
                state = Caught;
            }
            else
            {
                state = Chasing;
            }
        }
        else
        {
            guardAgent.speed = 2;
            if (chasing)
            {
                chasing = false;
                guardAnimator.SetInteger("movement", 1);
                index = (index + 1) % waypoints.Count;
                guardAgent.destination = waypoints[index].position;
            }
            else if (moving && Vector3.Distance(transform.position, waypoints[index].position) < 0.1f)
            {
                moving = false;
                guardAnimator.SetInteger("movement", 0);
                StartCoroutine("PatrolNextLocation");
            }
            else if (moving)
            {
                guardAnimator.SetInteger("movement", 1);
            }
        }
    }

    void Chasing(bool spotted)
    {
        if (spotted)
        {
            if (Vector3.Distance(transform.position, david.transform.position) < 1)
            {
                StopCoroutine("PatrolNextLocation");
                caught = true;
                state = Caught;
            }
            else
            {
                chasing = true;
                guardAnimator.SetInteger("movement", 2);
                //Set the guard's destination to the player's position
                guardAgent.destination = davidCentroid;
                guardAgent.speed = 3.5f;
            }
        }
        else
        {
            state = Patrolling;
        }
    }

    void Caught(bool spotted)
    {
        davidScript.caught = true;
        guardAgent.destination = transform.position;
        replay.SetActive(true);
        if (!guardAnimator.GetCurrentAnimatorStateInfo(0).IsName("Excited"))
        { 
            guardAnimator.SetTrigger("capture");
        }
        guardAgent.speed = 0;
    }
}
