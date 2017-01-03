using UnityEngine;
using System.Collections;

public class StartCanvasScript : MonoBehaviour {
	public GameObject menu; // Assign in inspector
	public GameObject player;
	public AudioClip[] introSounds;
	private AudioSource audioSource;
	int introSoundIndex;
	public static bool hasPlayed = false;

	void Start() {
		audioSource = GetComponent<AudioSource> ();
		if (hasPlayed == true) {
			menu.SetActive (false);
			player.SetActive (true);
		} else {
			introSoundIndex = Random.Range (0, introSounds.Length);
			audioSource.PlayOneShot(introSounds[introSoundIndex]);
			player.SetActive (false);
		}
		hasPlayed = false;
	}

	void Update() {
		if (Input.GetKeyDown("space")) {
			menu.SetActive(false);
			player.SetActive (true);
		}
	}
}
