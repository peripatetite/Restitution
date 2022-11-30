using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    public List<Transform> waypoints;

    private GameObject replay;
    private NavMeshAgent guardAgent;
    private Animator guardAnimator;
    private GameObject david;
    private CharacterController characterController;
    private Vector3 davidCentroid;
    private int index = 1;
    private bool moving = true;
    private bool chasing = false;

    // Start is called before the first frame update
    void Start()
    {
        guardAgent = GetComponent<NavMeshAgent>();
        guardAnimator = GetComponent<Animator>();
        david = GameObject.Find("David");
        characterController = david.GetComponent<CharacterController>();

        replay = GameObject.Find("Play");
        replay.SetActive(false);

        guardAgent.destination = waypoints[index].position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 guardDirection = transform.forward;
        Vector3 guardView = transform.position + new Vector3(0, 1.5f, 0);
        davidCentroid = characterController.bounds.center;
        Vector3 direction_from_guard_to_david = davidCentroid - guardView;
        direction_from_guard_to_david.Normalize();
        bool inView = Vector3.Angle(guardDirection, direction_from_guard_to_david) < 60;
        RaycastHit hit;
        if (inView
            && Physics.Raycast(guardView, direction_from_guard_to_david, out hit, Vector3.Distance(transform.position, davidCentroid) + 1)
            && hit.collider.gameObject == david)
        {
            // We should use bounds.center
            if (Vector3.Distance(transform.position, characterController.bounds.center) < 1)
            {
                replay.SetActive(true);
                guardAnimator.SetTrigger("capture");
                guardAgent.speed = 0;
            } else
            {
                chasing = true;
                guardAnimator.SetInteger("movement", 2);
                //Set the guard's destination to the player's position
                guardAgent.destination = davidCentroid;
                guardAgent.speed = 3.5f;
            }
        } else
        {
            if (moving && Vector3.Distance(transform.position, waypoints[index].position) < 0.1f || chasing)
            {
                guardAgent.speed = 2;
                moving = false;
                chasing = false;
                guardAnimator.SetInteger("movement", 0);
                StartCoroutine("PatrolNextLocation");
            }
            else if (moving)
            {
                guardAnimator.SetInteger("movement", 1);
            }
        }
    }

    IEnumerator PatrolNextLocation()
    {
        index++;
        if (index == waypoints.Count)
        {
            index = 0;
        }
        yield return new WaitForSeconds(Random.Range(1f, 5f));
        guardAgent.destination = waypoints[index].position;
        moving = true;
    }
}
