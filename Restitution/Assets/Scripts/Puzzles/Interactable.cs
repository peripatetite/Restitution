using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour {
    public RectTransform keyIcon;
    public KeyCode keyCode = KeyCode.E;

	protected bool interacting;

	private Vector2 keyIconScale;
	private Vector2 keyIconScale_target;

	protected virtual void Initialize() { }
	protected virtual void PlayerInteract() { }

	private void Start() {
		keyIconScale = keyIcon.sizeDelta;
		keyIconScale_target = Vector2.zero;
		keyIcon.sizeDelta = keyIconScale_target;
		Initialize();
	}

	private void Update() {
		TweenKeyIcon();
		if (interacting)
			PlayerInteract();
	}

	private void OnTriggerStay(Collider other) {
		if (other.tag == "Player") {
			keyIconScale_target = keyIconScale;
			if (Input.GetKeyDown(keyCode)) {
				interacting = !interacting;
			}
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
			keyIconScale_target = Vector2.zero;
			interacting = false;
		}
	}

	private void TweenKeyIcon() {
		if (keyIconScale_target == Vector2.zero && keyIcon.sizeDelta.sqrMagnitude < 0.01f) {
			keyIcon.gameObject.SetActive(false);
		} else {
			keyIcon.gameObject.SetActive(true);
			keyIcon.sizeDelta = Vector2.Lerp(keyIcon.sizeDelta, keyIconScale_target, Mathf.Min(1, 10f * Time.deltaTime));
		}
	}
}
