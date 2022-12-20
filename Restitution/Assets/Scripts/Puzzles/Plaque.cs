using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plaque : Interactable
{
	public GameObject frame;
	public int position;
	public bool hasPainting;

	private PaintingShuffler paintingShuffler;
	private David davidScript;

	protected override void Initialize()
	{
		base.Initialize();
		paintingShuffler = LevelManager.instance.gameObject.GetComponent<PaintingShuffler>();
		paintingShuffler.AddPainting(this);
		davidScript = GameObject.Find("David").GetComponent<David>();
		hasPainting = true;
		SetInteractable(false);
	}

	protected override void PlayerBeginInteract()
	{
		base.PlayerBeginInteract();
		if (hasPainting)
		{
			paintingShuffler.PickUpPainting(position);
		}
		else
		{
			hasPainting = true;
			paintingShuffler.PutDownPainting(davidScript.frame, position);
			if (transform.position.x == frame.transform.position.x
			&& transform.position.z == frame.transform.position.z)
			{
				paintingShuffler.CheckPaintings();
			}
		}
		base.PlayerStopInteract();
	}
}
