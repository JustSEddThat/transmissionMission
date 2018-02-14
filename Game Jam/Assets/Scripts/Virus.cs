using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Virus : MonoBehaviour
{
	//Holds Viruses in a static list to do work on them once between all involved viruses

	private static List<Virus> battleArray;

	//position of the camera, used to look at camera
	private Vector3 camPosition;

	#region Planet Scripts

	public Planet player;
	public Planet enemy;
	//either planet or virus

	#endregion

	#region Public Variables

	public Transform target;
	public Rigidbody rb;
	public float rotateSpeed;
	public float speed;

	//damage determined in planet script
	public float dmg;

	public TrailRenderer trail;
	public GameObject canvasPrefab;

	public GameObject earthCollisionParticle;
	public GameObject virusCollisionParticle;

	#endregion


	private GameObject VirusHud;
	private TMP_Text dmgText;


	void Start ()
	{
		battleArray = new List<Virus> ();

		camPosition = GameObject.FindGameObjectWithTag ("MainCamera").transform.position;

		VirusHud = Instantiate (canvasPrefab, transform);

		dmgText = VirusHud.GetComponentInChildren<TMP_Text> ();
		dmgText.text = "" + dmg;

		enemy = player.enemy;
		target = enemy.transform;

		trail.startColor = player.GetComponent<MeshRenderer> ().material.color;
	}

	void FixedUpdate ()
	{
		//If Target can't be found. head for the enemey boi.
		if (target == null)
		{
			target = enemy.transform;
		}

		transform.LookAt (target.position);
		//VirusHud should always be looking at the main camera
		VirusHud.transform.LookAt (camPosition);

		ScanForEnemies ();

	}

	void LateUpdate ()
	{
		VirusHud.transform.position = transform.position;
		dmgText.text = "" + dmg;
		MoveToTarget ();
	}

	#region Virus battle method

	//When viruses collide they will call this method. which compares the viruses as members of battleArray
	void InBattle ()
	{
		battleArray.Add (this);

		Debug.Log (toString () + "BA size: " + battleArray.Count);
		if (battleArray.Count == 2)
		{
			if (battleArray [0].dmg == battleArray [1].dmg)
			{
				Instantiate (virusCollisionParticle, battleArray [1].transform.position, Quaternion.identity);
				Destroy (battleArray [0].gameObject);
				Destroy (battleArray [1].gameObject);

			}  

			if (battleArray [0].dmg < battleArray [1].dmg)
			{
				
				//	other.GetComponent<Virus> ().dmg -= dmg;
				//	other.GetComponent<Virus> ().text.text = "" + other.GetComponent<Virus> ().dmg;
				battleArray [1].dmg -= battleArray [0].dmg;
				Destroy (battleArray [0].gameObject);
				Instantiate (virusCollisionParticle, battleArray [1].transform.position, Quaternion.identity);

			} 

			if (battleArray [0].dmg > battleArray [1].dmg)
			{

				//	other.GetComponent<Virus> ().dmg -= dmg;
				//	other.GetComponent<Virus> ().text.text = "" + other.GetComponent<Virus> ().dmg;
				battleArray [0].dmg -= battleArray [1].dmg;
				Destroy (battleArray [1].gameObject);
				Instantiate (virusCollisionParticle, battleArray [0].transform.position, Quaternion.identity);


			} 
			//clear list after comparison to get ready for the next match
			battleArray.Clear ();
		}
	}

	#endregion

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject == enemy.gameObject)
		{
			//Deal damage to enemy player
			enemy.TakeDamage (dmg);

			Instantiate (virusCollisionParticle, transform.position, Quaternion.identity);
			Destroy (this.gameObject);
		}

		if (other.GetComponent<Virus> ())
		{
			if (other.GetComponent<Virus> ().player != player)
			{

				InBattle ();
			
			}
		}
	}

	void MoveToTarget ()
	{
		if (target != null)
		{
			transform.position = Vector3.MoveTowards (transform.position, target.position, speed);
		}
	}

	void ScanForEnemies ()
	{
		Collider[] colliders = Physics.OverlapSphere (Vector3.zero, 20f);

		foreach (Collider x in colliders)
		{
			if (x.GetComponent<Virus> ())
			{
				if (x.GetComponent<Virus> ().enemy == player)
				{
					target = x.transform;
				}
			}
		}
	}

	string toString ()
	{
		return player.gameObject.name + "'s Virus___ dmg: " + dmg;
	}
}
