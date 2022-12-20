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

	void Start()
    {
		currentPosition = 0;
    }

	public void AddPainting(Painting painting)
    {
		paintings.Add(painting);
		painting.transform.position = positions[currentPosition].position;
		painting.transform.rotation = positions[currentPosition++].rotation;
		Transform paintingTransform = positions[currentPosition % positions.Length];
		painting.frame.transform.position = paintingTransform.position + new Vector3(0, 1, 0);
		painting.frame.transform.rotation = new Quaternion(paintingTransform.rotation.x, paintingTransform.rotation.y + 180, paintingTransform.rotation.z, 1);
	}

	public void CheckPaintings()
    {
		foreach (Painting painting in paintings)
        {
			if (painting.transform.position.x != painting.frame.transform.position.x 
				|| painting.transform.position.z != painting.frame.transform.position.z) {
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
