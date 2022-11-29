using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialGuard : MonoBehaviour {

    [SerializeField]
    private GameObject miniGate;
    [SerializeField]
    private float raisedHeight;

    private GameObject player;
    private Collider playerCollider;
    private Vector3 guardView;
    private Vector3 raisedPos, loweredPos;
    private Vector3 targetPos;

    private const float EPS = 0.05f;

	// Start is called before the first frame update
	void Start() {
        if (miniGate == null) { Debug.LogError("Mini gate not assigned to tutorial guard!"); }

        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) { Debug.LogError("Player not found in scene! Is tag not correctly assigned?"); }
        playerCollider = player.GetComponent<CharacterController>();

        guardView = transform.position + new Vector3(0, 1.5f, 0);
        raisedPos = new Vector3(miniGate.transform.position.x, raisedHeight, miniGate.transform.position.z);
        loweredPos = miniGate.transform.position;
        targetPos = loweredPos;
	}

	// Update is called once per frame
	void Update() {
        Vector3 guardDirection = transform.forward;
        Vector3 playerCentroid = playerCollider.bounds.center;
        Vector3 direction_from_guard_to_david = (playerCentroid - guardView).normalized;
        bool inView = Vector3.Angle(guardDirection, direction_from_guard_to_david) < 60;
        RaycastHit hit;
        if (inView
            && Physics.Raycast(guardView, direction_from_guard_to_david, out hit, Vector3.Distance(transform.position, playerCentroid) + 1)
            && hit.collider.tag == "Player") {
            // TODO: Display guard warning to player
            targetPos = raisedPos;
        } else {
            targetPos = loweredPos;
		}

		if (Mathf.Abs(miniGate.transform.position.y - targetPos.y) > EPS) {
			miniGate.transform.position = Vector3.Lerp(miniGate.transform.position, targetPos, 0.4f);
		}
	}
}
