using UnityEngine;
using System.Collections;

public class batJarScript : MonoBehaviour {

	public AudioClip batjarSound;
	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
	}
	void OnTriggerEnter2D(Collider2D co) {
		if (co.name == "pacman") {
			audioSource.PlayOneShot(batjarSound);
		}
	}
}
