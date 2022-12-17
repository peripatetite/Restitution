using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RebusLock : Interactable
{
	public GameObject puzzle;
	public TMP_InputField[] inputs = new TMP_InputField[2];
	public GameObject[] lights;

	private string answer1 = "stolen";
	private string answer2 = "artifacts";
	private int selected = 0;

	// Start is called before the first frame update
	protected override void Initialize()
	{
		//How can we select the first box when the puzzle opens? Just do it through another script?
		base.Initialize();
		inputs[0].Select();
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

    // Update is called once per frame
    protected override void PlayerInteract()
    {
		if (Input.GetKeyDown(KeyCode.Tab))
        {
			selected = (selected + 1) % 2;
			inputs[selected].Select();
        }
		if (inputs[0].text.ToLower().Trim().Equals(answer1)
			&& inputs[1].text.ToLower().Trim().Equals(answer2))
		{
			TurnOnLights();
        }
    }

	private void TurnOnLights()
	{
		foreach (GameObject light in lights)
        {
			light.SetActive(true);
        }
		PlayerStopInteract();
		interactable = false;
	}

	//Should we have a button that they press when they want to guess?
}