using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class RebusLock : Interactable
{
	public GameObject puzzle;
	public TMP_InputField[] inputs = new TMP_InputField[2];
	public GameObject[] lights;

	private QuirpManager quirpManager;
	private string answer1 = "stolen";
	private string answer2 = "artifact";
	private string answer3 = "artifacts";
	private int selected = 0;

	protected override void Initialize()
	{
		base.Initialize();
		quirpManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<QuirpManager>();
	}

	protected override void PlayerBeginInteract()
	{
		base.PlayerBeginInteract();
		puzzle.SetActive(true);

		//Select the first box when the play opens the lock
		inputs[0].Select();
	}

	protected override void PlayerStopInteract()
	{
		base.PlayerStopInteract();
		puzzle.SetActive(false);
	}	

    protected override void PlayerInteract()
    {
		//Allow the player to toggle between guess boxes with Tab
		if (Input.GetKeyDown(KeyCode.Tab))
        {
			selected = (selected + 1) % 2;
			inputs[selected].Select();
        }
    }

	//Turn on the lights for the next puzzle
	private void TurnOnLights()
	{
		foreach (GameObject light in lights)
        {
			light.SetActive(true);
        }
		PlayerStopInteract();
		interactable = false;
	}
	
	//Check both words, indicating if one is correct by changing the background to green
	public void Guess()
    {
		if (inputs[0].text.ToLower().Trim().Equals(answer1)
			&& inputs[1].text.ToLower().Trim().Equals(answer2)
			|| inputs[1].text.ToLower().Trim().Equals(answer3))
		{
			quirpManager.AddQuirp("Correct!");
			TurnOnLights();
		} else if (inputs[0].text.ToLower().Trim().Equals(answer1))
        {
			inputs[0].gameObject.GetComponent<Image>().color = new Color32(0, 180, 0, 255);
			inputs[1].text = "";
			inputs[1].Select();
		} else if (inputs[1].text.ToLower().Trim().Equals(answer2)
			|| inputs[1].text.ToLower().Trim().Equals(answer3))
        {
			inputs[1].gameObject.GetComponent<Image>().color = new Color32(0, 180, 0, 255);
			inputs[0].text = "";
			inputs[0].Select();
		} else
        {
			inputs[0].text = "";
			inputs[1].text = "";
			inputs[0].Select();
		}
	}
}
