using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RebusLock : Interactable
{
	public GameObject puzzle;
	public TMP_InputField[] inputs = new TMP_InputField[2];
	public GameObject[] lights;

	private QuirpManager quirpManager;
	private string answer1 = "stolen";
	private string answer2 = "artifacts";
	private int selected = 0;

	// Start is called before the first frame update
	protected override void Initialize()
	{
		//Why isn't the first box being selected?
		base.Initialize();
		inputs[0].Select();
		quirpManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<QuirpManager>();
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

	public void Guess()
    {
		if (inputs[0].text.ToLower().Trim().Equals(answer1)
			&& inputs[1].text.ToLower().Trim().Equals(answer2))
		{
			quirpManager.AddQuirp("Correct!");
			TurnOnLights();
		} else if (inputs[0].text.ToLower().Trim().Equals(answer1))
        {
			inputs[0].gameObject.GetComponent<Image>().color = new Color32(0, 180, 0, 255);
		} else if (inputs[1].text.ToLower().Trim().Equals(answer2))
        {
			inputs[1].gameObject.GetComponent<Image>().color = new Color32(0, 180, 0, 255);
		}
	}
}
