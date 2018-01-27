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

	private Slider resources;

	[Range(1,6)]
	public int diseaseLevel;

	public float builtPower;
	#endregion

	public float radius;

	public GameObject virus;

	void Start()
	{
		//slider component on this object or any children
		resources = GetComponentInChildren<Slider> ();

	}
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Z))
		{
			Shoot ();
		}
	}

	public void BuildPower(int lvl)
	{
		builtPower += ConvertLvlToPower (lvl);

		resources.value -= (float)lvl;

		Shoot ();
	}

	private float ConvertLvlToPower(int lvl)
	{
		switch(lvl)
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
			Debug.Log("some shit wrong bro");
			return 3;
		}
	}
	void Shoot ()
	{
		if (virus != null)
		{
			//Vector3 loc = Random.insideUnitCircle * radius;

			GameObject shot = Instantiate (virus, transform.position, Quaternion.identity);

			if (shot.GetComponent<Virus> ())
			{
				shot.GetComponent<Virus> ().dmg = builtPower;
				shot.GetComponent<Virus> ().player = this;
				//Debug.Log ("Spawned by " + this.gameObject);
			}
		}

		//reset built power after shooting projectile
		builtPower = 0;
	}

	public void TakeDamage (float dmg)
	{
		health -= dmg;
	}
}
