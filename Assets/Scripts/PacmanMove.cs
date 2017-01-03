using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PacmanMove : MonoBehaviour {
	public float speed = 0.25f;
	float speedModifier;
	public Text countText;
	public Text finalCountText;


	//sounds
	public AudioClip nomSound;
	public AudioClip sirenSound;
	public AudioClip sodaSound;
	public AudioClip[] voiceSounds;
	private AudioSource audioSource;


	public static int count;
	public static int heartCount;
	public GameObject siren;
	public static bool sirenIsOn;
	public static bool isNearComplete;
	static int heartsTotal = 336;//336

	bool isCombo;
	int comboCount;
	Rigidbody2D rbody;
	Animator anim;
	ParticleSystem emitter;
	ParticleSystem sirenEmitter;
	public static bool isComplete = false;


	void Start () {
		rbody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		audioSource = GetComponent<AudioSource>();
		emitter = GetComponent<ParticleSystem> ();
		sirenEmitter = siren.GetComponent<ParticleSystem> ();
		sirenIsOn = false;
		count = 0;
		UpdateScore ();
	}
		
	void FixedUpdate () {

		Vector2 targetposition = gameObject.GetComponent<Transform>().position;
		GameObject.Find ("speedParticle").GetComponent<Transform>().position = targetposition;



		if (Powerup.isPoweredUp == true) {
			speedModifier = 1.75f;
		} else {
			speedModifier = 1.0f;
		}


		Vector2 movement_vector = new Vector2(Input.GetAxisRaw("Horizontal")*speedModifier, Input.GetAxisRaw("Vertical")*speedModifier);


		if (movement_vector != Vector2.zero) {
			// TODO: fix flashing animation when diagonol, or remove diagonal
			anim.SetFloat ("DirX", movement_vector.x);
			anim.SetFloat ("DirY", movement_vector.y);
		} else {
			// TODO: Make mouth close somehow
		}

		rbody.MovePosition (rbody.position + movement_vector * speed);

		if (isNearComplete && !sirenIsOn) {
			sirenIsOn = true;
			sirenEmitter.enableEmission = true;
			siren.GetComponent<Animator> ().enabled = true;
		}
	}

	void OnTriggerEnter2D(Collider2D co) {

		if (co.tag == "heart") {
			if (!audioSource.isPlaying) {
				audioSource.PlayOneShot(nomSound);
			}
		
			StartCanvasScript.hasPlayed = true;
			CancelInvoke ("clearCombo"); //cancels combo
			int basePoints;
			if (Powerup.isPoweredUp == true) {
				basePoints = 50;
			} else {
				basePoints = 10;
			}
			// combo behavior
			if (isCombo == true) {
				count = count + (basePoints * (1+Mathf.Clamp(comboCount, 0, 20)));
				comboCount = comboCount + 1;
			} else {
				count = count + basePoints;
			}
			// start siren
			if (heartCount >= (heartsTotal*0.8)) {
				isNearComplete = true;
				if (!sirenIsOn) {
					audioSource.loop = true;
					audioSource.Play();
				}
			}
			UpdateScore ();
			heartCount = heartCount + 1;
			isCombo = true; // starts combo
			Invoke ("clearCombo", 1.0f);

			if (heartCount >= heartsTotal) {
				isComplete = true;
			}
		}

		if (co.tag == "powerup"){
			GameObject.Find ("speedParticle").GetComponent<ParticleSystem> ().enableEmission = true;
			int voiceSoundsIndex = Random.Range (0, voiceSounds.Length);
			audioSource.PlayOneShot(sodaSound, 0.3f); // plays can sound
			audioSource.PlayOneShot(voiceSounds[voiceSoundsIndex]); // plays drpepper sound
			CancelInvoke ("clearPoweredUp"); //cancels powerup timeout
			Powerup.isPoweredUp = true; // starts powerup
			emitter.enableEmission = true;


			Invoke("clearBubbles", 3.0f);
			Invoke("clearSpeed", 0.12f);
			Invoke("clearPoweredUp", 3.0f);
		}
				
	}

	void UpdateScore () {
		countText.text = ""+count.ToString("000000");
		finalCountText.text = ""+count.ToString("000000");
	}

	private void clearCombo()
	{
		isCombo = false;
		comboCount = 0;
	}
	private void clearPoweredUp()
	{
		Powerup.isPoweredUp = false;
	}
	private void clearBubbles()
	{
		emitter.enableEmission = false;
	}
	private void clearSpeed()
	{
		GameObject.Find ("speedParticle").GetComponent<ParticleSystem> ().enableEmission = false;
	}
}
