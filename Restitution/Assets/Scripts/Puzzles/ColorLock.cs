using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorLock : Interactable {
	public Image[] slots = new Image[4];
    public TextMeshProUGUI[] inputs = new TextMeshProUGUI[4];
    public Color[] jarColors = new Color[10];

	private int[] curr_passcode = new int[4];
	private int[] passcode = new int[4];
	private int counter = 0;

	protected override void Initialize() {
		GeneratePasscode();
	}

	private void GeneratePasscode() {
		List<int> nums = new List<int>();
		for (int i = 0; i < 10; i++) { nums.Add(i); }
		for (int i = 0; i < 4; i++) {
			int index = Random.Range(0, nums.Count);
			passcode[i] = nums[index];
			Debug.Log(passcode[i]);
			slots[i].color = jarColors[nums[index]];
			nums.RemoveAt(index);
		}

	}

	public void EnterNumber(int num) {
		curr_passcode[counter] = num;
		inputs[counter].text = num.ToString();
		counter++;

		if (counter < 4) { return; }
		for (int i = 0; i < 4; i++) {
			if (curr_passcode[i] != passcode[i]) {
				counter = 0;
				ResetSlots();
				return;
			}
		}

		UnlockExit();
	}

	private void UnlockExit() {

	}

	private void ResetSlots() {
		foreach (TextMeshProUGUI input in inputs) {
			input.text = "-";
		}
	}
}
