using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artifact : Interactable
{
	protected override void PlayerBeginInteract() {
		base.PlayerBeginInteract();
		LevelManager.instance.AdvanceLevel();
		interactable = false;
		PlayerStopInteract();
	}
}
