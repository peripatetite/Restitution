using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting : Interactable
{
	public GameObject frame;
	public int position;

	private PaintingShuffler paintingShuffler;
	private bool hasPainting;

	protected override void Initialize()
	{
		base.Initialize();
		paintingShuffler = LevelManager.instance.gameObject.GetComponent<PaintingShuffler>();
		paintingShuffler.AddPainting(this);
		hasPainting = true;
	}

	protected override void PlayerBeginInteract()
	{
		base.PlayerBeginInteract();
		Debug.Log("Interacting");
		if (hasPainting)
        {
			paintingShuffler.PickUpPainting(position);
		} else
        {
			hasPainting = true;
			paintingShuffler.PutDownPainting(frame, position);
			if (transform.position.x == frame.transform.position.x
			&& transform.position.z == frame.transform.position.z)
			{
				paintingShuffler.CheckPaintings();
			}
		}
		
	}
}
