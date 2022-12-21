using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerialController : MonoBehaviour {

	public int serialLimit = 4;
	public GameObject[] clocks = new GameObject[4];

	private QuirpManager quirpManager;
	private List<SerialButton> serialButtons = new List<SerialButton>();
	private int currentSerial;
	private int[] serials;
	private int buttonCount;

	void Awake() {
		serials = new int[serialLimit];
		for (int i = 0; i < serialLimit; i++) {
			serials[i] = i + 1;
		}
		for (int i = 0; i < serialLimit * 2; i++) {
			int temp = serials[i % serialLimit];
			int toSwap = Random.Range(0, serialLimit);
			serials[i % serialLimit] = serials[toSwap];
			serials[toSwap] = temp;
		}
	}

	void Start() {
		quirpManager = GetComponent<QuirpManager>();
		currentSerial = 1;
	}

	public void AddButton(SerialButton button) {
		if (buttonCount >= serialLimit) {
			return;
		}
		serialButtons.Add(button);
		button.serialNumber = serials[buttonCount++];
	}

	public bool ReceiveButtonPress(int serialNum) {
		if (currentSerial == serialNum) {
			if (currentSerial++ == serialLimit) {
				quirpManager.AddQuirp("David: Great, that's all the buttons. Took me long enough....");
				quirpManager.AddQuirp("Anouncement: Time is now flowing...");
				quirpManager.AddQuirp("Guard: What was that?");
				quirpManager.AddQuirp("Other Guard: Eh, probably the new security system.");
				SetClocks();
			} else {
				quirpManager.AddQuirp("David: Nice! This is the correct button.");
			}
			return true;
		} else {
			if (currentSerial == 1) {
				quirpManager.AddQuirp("David: I don't think I should press this button first.");
			} else {
				quirpManager.AddQuirp("David: Everything has reset! I think I pressed the wrong button.");
			}
			ResetAllButtons();
			currentSerial = 1;
			return false;
		}
	}

	private void ResetAllButtons() {
		foreach (SerialButton button in serialButtons) {
			button.SetInteractable(true);
		}
	}

	private void SetClocks() {
		foreach (GameObject clock in clocks)
        {
			clock.GetComponent<ClockRandomizer>().setTime();
        }
		GameObject.Find("Clock Lock").GetComponent<ClockLock>().CalculatePasscode();
	}
}
