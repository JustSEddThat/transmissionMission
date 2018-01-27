using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{

	public Planet player;

	public Planet enemy;

	public Rigidbody rb;

	public float rotateSpeed;

	public float speed;

	public float dmg;

	public Transform target;

	void Start ()
	{
		enemy = player.enemy;
		target = enemy.transform;
	}

	void FixedUpdate ()
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

		if (target == null)
		{
			target = enemy.transform;
		}
	}

	void LateUpdate ()
	{
		MoveToTarget ();
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject != player.gameObject)
		{
			//Deal damage to player
			enemy.TakeDamage (dmg);
			Destroy (this.gameObject);
		}

		if (other.GetComponent<Virus> ())
		{
			if (other.GetComponent<Virus> ().player != player)
			{
				Debug.Log (this.toString ());
				// Insert player
				Destroy (this.gameObject);
			}
		}
	}

	void OnCollisionEnter (Collision other)
	{
		
	}



	void MoveToTarget ()
	{
		if (target != null)
		{
			transform.position = Vector3.MoveTowards (transform.position, target.position, .3f);

			//transform.position = Vector3.Lerp (transform.position, pos, 0.005f);
		}
	}
		
	string toString()
	{
		return player.gameObject.name + "'s Virus___ dmg: " + dmg;
	}

	//	void MoveToTargetRotation ()
	//	{
	//		Debug.Log ("Move to target rotation");
	//		Vector3 dir = transform.position - enemy.gameObject.transform.position;
	//		dir.Normalize ();
	//
	//		Vector3 rotateAmt = Vector3.Cross (dir, enemy.gameObject.transform.position);
	//
	//		rb.angularVelocity = rotateAmt * rotateSpeed;
	//
	//		rb.velocity = transform.forward * speed;
	//
	//	}

}
