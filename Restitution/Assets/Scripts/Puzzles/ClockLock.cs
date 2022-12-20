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
	public TextMeshProUGUI problem;

	private int[] curr_passcode = new int[4];
	private int[] passcode = new int[4];
	private int counter = 0;

	protected override void Initialize()
	{
		base.Initialize();
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

	public void CalculatePasscode()
	{
		int[] clockInts = new int[4];
		for (int i = 0; i < 4; i++)
        {
			clockInts[i] = clocks[i].GetComponent<ClockRandomizer>().clockInt;
		}
		int solution = clockInts[0];
		int[] ops = new int[3];
		string[] strOps = new string[3];
		for (int i = 0; i < 3; i++) {
			ops[i] = Random.Range(0, 2);
			if (ops[i] == 0)
            {
				solution += clockInts[i + 1];
				strOps[i] = "+";
			} else
            {
				solution -= clockInts[i + 1];
				strOps[i] = "-";
            }
		}
		solution = Mathf.Abs(solution);
		Debug.Log(solution);
		for (int i = 0; i < 4; i++)
        {
			passcode[i] = (int)(solution / Mathf.Pow(10, 3 - i));
			solution %= (int)Mathf.Pow(10, 3 - i);
		}
		problem.text = "IV " + strOps[0] + " IX " + strOps[1] + " VI " + strOps[2] + " XV";
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
		UnlockPaintings();
	}

	private void UnlockPaintings()
	{
		PlayerStopInteract();
		interactable = false;
		LevelManager.instance.gameObject.GetComponent<PaintingShuffler>().UnstickAllPaintings();
	}

	private void ResetSlots()
	{
		foreach (TextMeshProUGUI input in inputs)
		{
			input.text = "-";
		}
	}
}
