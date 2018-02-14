using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Planet : MonoBehaviour
{
	#region Enemy variables

	public Planet enemy;

	#endregion

	#region Player Variables

	public float health;

	public Slider healthSlider;
	public Slider resources;

	public float builtPower;

	#endregion

	public Keybinding keybind;

	public float radius;

	public GameObject[] virus;

	void Start ()
	{
		//slider component on this object or any children
		//resources = GetComponentInChildren<Slider> ();

	}

	void Update ()
	{
		if(Input.GetKeyDown (keybind.button1))
		{
			BuildPower (1);
		}

		if(Input.GetKeyDown (keybind.button2))
		{
			BuildPower (2);

		}

		if(Input.GetKeyDown (keybind.button3))
		{
			BuildPower (3);

		}

		if(Input.GetKeyDown (keybind.button4))
		{
			BuildPower (4);

		}

		if(Input.GetKeyDown (keybind.button5))
		{
			BuildPower (5);

		}

		if(Input.GetKeyDown (keybind.button6))
		{
			BuildPower (6);

		}

	}

	public void BuildPower (int lvl)
	{
		builtPower = ConvertLvlToPower (lvl);

		if (lvl <= resources.value)
		{
			resources.value -= (float)lvl;
			Shoot (lvl);

		}

	}

	private float ConvertLvlToPower (int lvl)
	{
		switch (lvl)
		{
			case 1:
				return 50;
			case 2: 
				return 100;
			case 3:
				return 200;
			case 4:
				return 350;
			case 5:
				return 500;
			case 6:
				return 1000;
			default:
				Debug.Log ("some shit wrong bro");
				return 3;
		}
	}

	void Shoot (int lvl)
	{
		if (virus != null)
		{

			Vector3 loc = (Vector2)transform.position + Random.insideUnitCircle * radius;
			GameObject shot = Instantiate (virus [lvl - 1], loc, Quaternion.identity);

			shot.GetComponent<Virus> ().dmg = builtPower;
			shot.GetComponent<Virus> ().player = this;

			shot.GetComponent<Virus> ().speed = (float)(7 - lvl) / 100;
		
		}

		//reset built power after shooting projectile
		builtPower = 0;
	}

	public void TakeDamage (float dmg)
	{
		healthSlider.value -= dmg;

	}
}
