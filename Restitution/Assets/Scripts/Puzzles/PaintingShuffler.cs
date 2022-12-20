using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingShuffler : MonoBehaviour
{
	public Transform[] positions = new Transform[8];

	private QuirpManager quirpManager;
	private GameObject[] frames = new GameObject[8];
	private List<Painting> paintings = new List<Painting>();
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

	public void AddPainting(Painting painting)
	{
		frames[currentPosition] = painting.frame;
		paintings.Add(painting);
		painting.position = currentPosition;
		Transform plaqueTransform = positions[currentPosition++];
		painting.transform.position = plaqueTransform.position;
		painting.transform.localEulerAngles = plaqueTransform.localEulerAngles;
		Transform paintingTransform = positions[currentPosition % positions.Length];
		painting.frame.transform.position = new Vector3(paintingTransform.position.x, painting.frame.transform.localPosition.y, paintingTransform.position.z);
		painting.frame.transform.localEulerAngles = paintingTransform.localEulerAngles + new Vector3(0, 180, 0);
	}

	public void PickUpPainting(int position)
    {
		if (davidScript.frame != null)
        {
			PutDownPainting(davidScript.frame, position);
        }
		GameObject frame = frames[position];
		davidScript.frame = frame;
		Debug.Log(davidScript.frame.transform.position);
		frames[position].transform.position = ;
    }

	public void PutDownPainting(GameObject frame, int position)
    {
		frame.SetActive(true);
		Transform paintingTransform = frames[position].transform;
		frame.transform.position = new Vector3(paintingTransform.position.x, frame.transform.localPosition.y, paintingTransform.position.z);
		frame.transform.localEulerAngles = paintingTransform.localEulerAngles + new Vector3(0, 180, 0);
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
