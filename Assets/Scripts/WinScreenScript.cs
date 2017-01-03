using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WinScreenScript : MonoBehaviour {

	public GameObject menu; // Assign in inspector
	public GameObject player;
	public bool isShown;

	public AudioClip[] winSounds;
	private AudioSource audioSource;
	int winSoundIndex;

	void Start() {
		audioSource = GetComponent<AudioSource> ();
		menu.SetActive (false);
		isShown = false;
	}

	void Update() {
		if (PacmanMove.isComplete && !isShown) {
			menu.SetActive (true);
			GameObject.Find ("maze").GetComponent<AudioSource> ().Pause();
			winSoundIndex = Random.Range (0, winSounds.Length);
			audioSource.PlayOneShot(winSounds[winSoundIndex]);
			isShown = true;
			// WIN STATE
			Application.CaptureScreenshot ("SockmanHighScore" + PacmanMove.count + ".png");
			player.SetActive (false);

		}
		if (Input.GetKeyDown("space")&& PacmanMove.isComplete){
			Debug.Log ("Restart");
			PacmanMove.heartCount = 0;
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			PacmanMove.isComplete = false;
			player.SetActive (true);
			PacmanMove.isNearComplete = false;
			PacmanMove.sirenIsOn = false;
		}
	}

}
