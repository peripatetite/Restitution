using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour {
    public RectTransform keyIcon;
    public KeyCode keyCode = KeyCode.E;

	protected bool interactable;
	protected bool interacting;
	protected bool inRange;

	private Vector2 keyIconScale;
	private Vector2 keyIconScale_target;

	/// <summary>
	/// Called in start. Use to initialize variables.
	/// </summary>
	protected virtual void Initialize() { }
	/// <summary>
	/// Called (once) when player starts interacting with this Interactable.
	/// </summary>
	protected virtual void PlayerBeginInteract() { interacting = true; }
	/// <summary>
	/// Called when player is interacting with this Interactable.
	/// </summary>
	protected virtual void PlayerInteract() { }
	/// <summary>
	/// Called (once) when player stops or can no longer interact with this Interactable.
	/// </summary>
	protected virtual void PlayerStopInteract() { interacting = false; }

	private void Start() {
		interactable = true;
		keyIconScale = keyIcon.sizeDelta;
		keyIconScale_target = Vector2.zero;
		keyIcon.sizeDelta = keyIconScale_target;
		Initialize();
	}

	private void Update() {
		if (inRange) {
			keyIconScale_target = keyIconScale;
			if (interactable && Input.GetKeyDown(keyCode)) {
				if (interacting) {
					PlayerStopInteract();
				} else {
					PlayerBeginInteract();
				}
			}
		}

		if (interactable && interacting)
			PlayerInteract();

		TweenKeyIcon();
	}

	private void OnTriggerStay(Collider other) {
		if (other.tag == "Player") {
			inRange = true;
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
			inRange = false;
			keyIconScale_target = Vector2.zero;
			PlayerStopInteract();
		}
	}

	private void TweenKeyIcon() {
		if (!interactable) { keyIconScale_target = Vector2.zero; }
		if (keyIconScale_target == Vector2.zero && keyIcon.sizeDelta.sqrMagnitude < 0.01f) {
			keyIcon.gameObject.SetActive(false);
		} else {
			keyIcon.gameObject.SetActive(true);
			keyIcon.sizeDelta = Vector2.Lerp(keyIcon.sizeDelta, keyIconScale_target, Mathf.Min(1, 10f * Time.deltaTime));
		}
	}
}
