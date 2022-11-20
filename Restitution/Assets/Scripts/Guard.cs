using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    private Animator animController;
    private NavMeshAgent navAgent;
    private GameObject david;
    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        animController = GetComponent<Animator>();
        david = GameObject.Find("David");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 guardDirection = transform.forward;
        Vector3 davidCentroid = david.GetComponent<CharacterController>().center;
        Vector3 guardView = transform.position + new Vector3(0, 1.5f, 0);
        Vector3 direction_from_guard_to_david = davidCentroid - guardView;
        direction_from_guard_to_david.Normalize();
        bool inView = Vector3.Dot(guardDirection, direction_from_guard_to_david) > 0;
        if (inView)
        {
            RaycastHit hit;
            if (Physics.Raycast(guardView, direction_from_guard_to_david, out hit, Mathf.Infinity))
            {
                animController.SetInteger("movement", 2);
                //Set the guard's destination to the player's position
                //navAgent.destination = davidCentroid;
            }
        } else
        {
            //Set the agent back on the path
            //navAgent.path = NavMeshPath();
            animController.SetInteger("movement", 1);
        }
    }
}
