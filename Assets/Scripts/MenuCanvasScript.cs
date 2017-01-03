using UnityEngine;
using System.Collections;

public class MenuCanvasScript : MonoBehaviour {
	public GameObject menu; // Assign in inspector
	private bool isShowing;

	void Start() {
		menu.SetActive (false);
	}

	void Update() {
		if (Input.GetKeyDown("escape")) {
			isShowing = !isShowing;
			menu.SetActive(isShowing);
			if (isShowing) {
				GameObject.Find ("maze").GetComponent<AudioSource> ().Pause();
				Time.timeScale = 0.0f;
			} else {
				GameObject.Find ("maze").GetComponent<AudioSource> ().Play();
				Time.timeScale = 1.0f;
			}
		}
	}
}
