using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerialButton : Interactable {
    private SerialController serialController;
	public int serialNumber;

	protected override void Initialize() {
		base.Initialize();
		serialController = LevelManager.instance.gameObject.GetComponent<SerialController>();
		serialController.AddButton(this);
	}

	protected override void PlayerBeginInteract() {
		base.PlayerBeginInteract();
		interactable = false;
		PlayerStopInteract();
		serialController.ReceiveButtonPress(serialNumber);
	}
}
