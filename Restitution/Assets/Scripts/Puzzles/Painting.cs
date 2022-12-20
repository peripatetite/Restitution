using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting : Interactable
{
	public GameObject frame;

	private PaintingShuffler paintingShuffler;
	private bool hasPainting;
	private David davidScript;

	protected override void Initialize()
	{
		base.Initialize();
		davidScript = GameObject.Find("David").GetComponent<David>();
		paintingShuffler = LevelManager.instance.gameObject.GetComponent<PaintingShuffler>();
		paintingShuffler.AddPainting(this);
		hasPainting = true;
	}

	protected override void PlayerInteract()
	{
		base.PlayerInteract();
		if (hasPainting)
        {
			
		} else
        {
			hasPainting = true;
			if (transform.position.x == frame.transform.position.x
			&& transform.position.z == frame.transform.position.z)
			{
				paintingShuffler.CheckPaintings();
			}
		}
		
	}
}
