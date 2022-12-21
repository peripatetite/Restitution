using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintingShuffler : MonoBehaviour
{
	public Transform[] positions = new Transform[8];
	public Barriers barrier;
	public Artifact artifact;
	public Image paintingDisplay;
	public GameObject heldPainting;

	private QuirpManager quirpManager;
	private GameObject[] frames = new GameObject[8];
	private List<Plaque> plaques = new List<Plaque>();
	private int currentPosition;

	void Awake()
	{
		for (int i = 0; i < positions.Length * 2; i++)
		{
			Transform temp = positions[i % positions.Length];
			int toSwap = Random.Range(0, positions.Length);
			positions[i % positions.Length] = positions[toSwap];
			positions[toSwap] = temp;
		}
	}

	public void AddPainting(Plaque plaque)
	{
		plaques.Add(plaque);
		plaque.position = currentPosition;
		Transform plaqueTransform = positions[currentPosition++];
		frames[currentPosition % positions.Length] = plaque.frame;
		plaque.transform.position = plaqueTransform.position;
		plaque.transform.localEulerAngles = plaqueTransform.localEulerAngles;
		Transform paintingTransform = positions[currentPosition % positions.Length];
		plaque.frame.transform.position = new Vector3(paintingTransform.position.x, plaque.frame.transform.localPosition.y, paintingTransform.position.z);
		plaque.frame.transform.localEulerAngles = paintingTransform.localEulerAngles + new Vector3(0, 180, 0);
	}

	public void PickUpPainting(int position)
    {
		GameObject frame = frames[position];
		if (heldPainting != null)
        {
			PutDownPainting(heldPainting, position);
        } else
        {
			plaques[position].hasPainting = false;
        }
		heldPainting = frame;
		frame.SetActive(false);
		paintingDisplay.gameObject.SetActive(true);
		paintingDisplay.sprite = frame.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Image>().sprite;
    }

	public void PutDownPainting(GameObject frame, int position)
    {
		frame.SetActive(true);
		Transform paintingTransform = positions[position].transform;
		frame.transform.position = new Vector3(paintingTransform.position.x, frame.transform.position.y, paintingTransform.position.z);
		frame.transform.localEulerAngles = paintingTransform.localEulerAngles + new Vector3(0, 180, 0);
		frames[position] = frame;
		heldPainting = null;
		paintingDisplay.gameObject.SetActive(false);
	}

	public void CheckPaintings()
	{
		foreach (Plaque plaque in plaques)
		{
			if (plaque.transform.position.x != plaque.frame.transform.position.x
				|| plaque.transform.position.z != plaque.frame.transform.position.z)
			{
				return;
			}
		}
		StickAllPaintings();
		TurnOffLasers();
	}

	public void UnstickAllPaintings()
    {
		foreach (Plaque plaque in plaques)
        {
			plaque.SetInteractable(true);
        }
    }

	private void StickAllPaintings()
	{
		foreach (Plaque plaque in plaques)
		{
			plaque.SetInteractable(false);
		}

	}

	private void TurnOffLasers()
	{
		barrier.disabled1 = true;
		artifact.SetInteractable(true);
	}
}
