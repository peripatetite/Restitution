using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    public List<Transform> waypoints;

    private NavMeshAgent guard;
    private Animator guardAnimator;

    private int index = 1;
    private bool moving = true;
    // Start is called before the first frame update
    void Start()
    {
        guard = GetComponent<NavMeshAgent>();
        //guardAnimator = GetComponent<Animator>();

        guard.destination = waypoints[index].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving && Vector3.Distance(transform.position, waypoints[index].position) < 0.1f)
        {
            moving = false;
            //guardAnimator.SetInteger("movement", 0);
            StartCoroutine("PatrolNextLocation");
        } else if (moving)
        {
            //guardAnimator.SetInteger("movement", 1);
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
        guard.destination = waypoints[index].position;
        moving = true;
    }
}
