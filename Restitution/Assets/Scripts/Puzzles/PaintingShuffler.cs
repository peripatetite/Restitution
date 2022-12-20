using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingShuffler : MonoBehaviour
{
	public Transform[] positions = new Transform[8];

	private QuirpManager quirpManager;
	private List<Painting> paintings = new List<Painting>();
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

	public void AddPainting(Painting painting)
	{
		paintings.Add(painting);
		Transform plaqueTransform = positions[currentPosition++];
		painting.transform.position = plaqueTransform.position;
		painting.transform.localEulerAngles = plaqueTransform.localEulerAngles;
		Transform paintingTransform = positions[currentPosition % positions.Length];
		painting.frame.transform.position = new Vector3(paintingTransform.position.x, painting.frame.transform.localPosition.y, paintingTransform.position.z);
		painting.frame.transform.localEulerAngles = paintingTransform.localEulerAngles + new Vector3(0, 180, 0);
	}

	public void CheckPaintings()
	{
		foreach (Painting painting in paintings)
		{
			if (painting.transform.position.x != painting.frame.transform.position.x
				|| painting.transform.position.z != painting.frame.transform.position.z)
			{
				return;
			}
		}
		StickAllPaintings();
		TurnOffLasers();
	}

	private void StickAllPaintings()
	{
		foreach (Painting painting in paintings)
		{
			painting.SetInteractable(false);
		}

	}

	private void TurnOffLasers()
	{
		//TODO: Turn off the last set of lasers
		Debug.Log("Laser Turned Off");
	}
}
