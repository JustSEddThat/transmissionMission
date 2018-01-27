using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
	public Bounds bound;

	public List<Transform> items;

	public Vector3 offset;

	public Camera cam;

	void Awake ()
	{
		bound = new Bounds (items [0].position, Vector3.zero);
		cam = GetComponent<Camera> ();
	}

	void Start ()
	{
		
	}

	void Update ()
	{
		
	}

	void LateUpdate ()
	{
		if (items.Count == 0)
		{
			return;
		}

		Vector3 center = FindCenterPoint ();

		Vector3 newPos = center + offset;

		transform.position = newPos;

		foreach (Transform i in items)
		{
			bound.Encapsulate (i.position);
		}
	}

	void Zoom()
	{
		float newZoom = Mathf.Lerp (20, 40, bound.size.x / 50);

		cam.fieldOfView = newZoom;

	}

	Vector3 FindCenterPoint ()
	{
		if (items.Count == 1)
		{
			return items [0].position;
		}

		return bound.center;

	}


}
