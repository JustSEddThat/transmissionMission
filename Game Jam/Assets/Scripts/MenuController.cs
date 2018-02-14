using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour {

	public TMP_Text titleText;

	public string title;
	public float typeSpeed;

	public float speed;

	void Start()
	{
		StartCoroutine (TypeLetter());
	}

	void Update()
	{
		RenderSettings.skybox.SetFloat ("_Rotation" , Time.time * speed);
	}

	public void Play()
	{
		SceneManager.LoadScene (1);
	}

	public void Quit()
	{
		Application.Quit ();
	}

	IEnumerator TypeLetter()
	{
		foreach (char letter in title.ToCharArray ())
		{
			titleText.text += letter;
			yield return new WaitForSeconds (typeSpeed);
		}
	}
}
