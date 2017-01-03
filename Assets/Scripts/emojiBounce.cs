using UnityEngine;
using System.Collections;

public class emojiBounce : MonoBehaviour {
	public AudioClip parkoursSound;
	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
	}
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.name == "pacman") {
			audioSource.PlayOneShot(parkoursSound);
			gameObject.GetComponent<Animation>().Play ();
		}
	}
}
