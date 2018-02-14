using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer: MonoBehaviour
{
	private float years;
	private bool done;

	public TMP_Text timeText;

	void Start ()
	{
		done = false;
		StartCoroutine (TimePass ());
	}

	void Update ()
	{
		timeText.text = "Years Since beginning of War \n" + years;

		RenderSettings.skybox.SetFloat ("_Rotation", Time.time * 30);

	}

	IEnumerator TimePass ()
	{
		while (!done)
		{
			yield return new WaitForSecondsRealtime (.5f);
			years += 10;
			if (years % 25 == 0)
			{
				foreach (GameObject go in GameObject.FindGameObjectsWithTag("Planet"))
				{
					go.GetComponent<Planet> ().resources.value++;
				}
				yield return null;
			}
		}
	}
}
