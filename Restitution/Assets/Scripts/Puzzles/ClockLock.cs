using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClockLock : Interactable
{
	public GameObject puzzle;
	public TextMeshProUGUI[] inputs = new TextMeshProUGUI[4];
	public GameObject[] clocks = new GameObject[4];

	private int[] curr_passcode = new int[4];
	private int[] passcode = new int[4];
	private int counter = 0;

	protected override void Initialize()
	{
		base.Initialize();
		CalculatePasscode();
	}
	protected override void PlayerBeginInteract()
	{
		base.PlayerBeginInteract();
		puzzle.SetActive(true);
	}

	protected override void PlayerStopInteract()
	{
		base.PlayerStopInteract();
		puzzle.SetActive(false);
	}

	private void CalculatePasscode()
	{
		
	}

	public void EnterNumber(int num)
	{
		curr_passcode[counter] = num;
		inputs[counter].text = num.ToString();
		counter++;

		if (counter < 4) { return; }
		for (int i = 0; i < 4; i++)
		{
			if (curr_passcode[i] != passcode[i])
			{
				counter = 0;
				ResetSlots();
				return;
			}
		}
		UnlockExit();
	}

	private void UnlockExit()
	{
		PlayerStopInteract();
		interactable = false;
		LevelManager.instance.AdvanceLevel();
	}

	private void ResetSlots()
	{
		foreach (TextMeshProUGUI input in inputs)
		{
			input.text = "-";
		}
	}
}
