using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerialController : MonoBehaviour {

	public int serialLimit = 4;

	private QuirpManager quirpManager;
	private List<SerialButton> serialButtons = new List<SerialButton>();
	private int currentSerial;

	void Start() {
		quirpManager = GetComponent<QuirpManager>();
		currentSerial = 1;
	}

	public void AddButton(SerialButton button) {
		serialButtons.Add(button);
	}

	public bool ReceiveButtonPress(int serialNum) {
		Debug.Log(serialNum + " should be " + currentSerial);
		if (currentSerial == serialNum) {
			if (currentSerial++ == serialLimit) {
				quirpManager.AddQuirp("David: Great, that's all the buttons. Something is disabled.");
				Unlock();
			} else {
				quirpManager.AddQuirp("David: Nice! This is the correct button.");
			}
			return true;
		} else {
			currentSerial = 1;
			quirpManager.AddQuirp("David: Everything has reset! I don't think I should press this button now.");
			ResetAllButtons();
			return false;
		}
	}

	private void ResetAllButtons() {
		foreach (SerialButton button in serialButtons) {
			button.SetInteractable(true);
		}
	}

	private void Unlock() {
		// TODO: Unlock laser
	}
}
