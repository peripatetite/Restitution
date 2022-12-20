using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingShuffler : MonoBehaviour
{
	public Transform[] positions = new Transform[8];
	public Barriers barrier;
	public Artifact artifact;

	private QuirpManager quirpManager;
	private GameObject[] frames = new GameObject[8];
	private List<Plaque> plaques = new List<Plaque>();
	private int currentPosition;
	private David davidScript;

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

	void Start()
    {
		davidScript = GameObject.Find("David").GetComponent<David>();
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
		if (davidScript.frame != null)
        {
			PutDownPainting(davidScript.frame, position);
        } else
        {
			plaques[position].hasPainting = false;
        }
		davidScript.frame = frame;
		frame.SetActive(false);
    }

	public void PutDownPainting(GameObject frame, int position)
    {
		frame.SetActive(true);
		Transform paintingTransform = positions[position].transform;
		frame.transform.position = new Vector3(paintingTransform.position.x, frame.transform.position.y, paintingTransform.position.z);
		frame.transform.localEulerAngles = paintingTransform.localEulerAngles + new Vector3(0, 180, 0);
		frames[position] = frame;
		davidScript.frame = null;
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
		Debug.Log("Laser Turned Off");
	}
}
