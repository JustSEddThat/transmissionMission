using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiseaseLevel : MonoBehaviour
{
	public Planet myPlayer;
	public int myLevel;

	private Button me;

	void Start ()
	{
		me = GetComponent<Button> ();
	}

	void Update ()
	{
		if (myPlayer.resources.value >= myLevel)
		{
			me.interactable = true;
		} else
		{
			me.interactable = false;
		}
	}

	public void Play()
	{
		GetComponent<AudioSource> ().Play ();
	}




}
