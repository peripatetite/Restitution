using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class AnagramLock : Interactable
{
	public GameObject puzzle;
	public TMP_InputField input;

	private string answer = "guard";

	protected override void Initialize()
	{
		base.Initialize();
	}

	protected override void PlayerBeginInteract()
	{
		base.PlayerBeginInteract();
		puzzle.SetActive(true);
		input.Select();
	}

	protected override void PlayerInteract()
    {
		if (Input.GetKey(KeyCode.Return))
        {
			Guess();
        }
    }

	protected override void PlayerStopInteract()
	{
		base.PlayerStopInteract();
		puzzle.SetActive(false);
	}

	public void Guess()
	{
		if (input.text.ToLower().Trim().Equals(answer))
        {
			PlayerStopInteract();
			interactable = false;
			LevelManager.instance.AdvanceLevel();
		} else
        {
			input.text = "";
			input.Select();
        }
	}
}
