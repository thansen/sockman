using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CatMove : MonoBehaviour {

	//sounds
	public AudioClip[] deathSounds;
	private AudioSource audioSource;

	Rigidbody2D rbody;
	Animator anim;
	Vector2 movement_vector;

	public int[] arrX = new int[] { 0, 1, 0, -1};
	public int[] arrY = new int[] { 1, 0, -1, 0};

	public float speed;
	public Sprite catButt;

	float heartLeftSpeedMultiplier = 1;
	bool speedModified = false;
	int dirIndex;
	int rndX;
	int rndY;
	void Start () {
		rbody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		audioSource = GetComponent<AudioSource>();
		dirIndex = Random.Range (0, 4);
//		rndX = Random.Range (-1, 2);
//		rndY = Random.Range (-1, 2);

		InvokeRepeating("SetNewVector", speed*3, speed);
	}

	void FixedUpdate () {
		if (PacmanMove.sirenIsOn && !speedModified) {
			gameObject.GetComponent<ParticleSystem> ().enableEmission = true;
			heartLeftSpeedMultiplier = 1.25f;//if you want to increase speed when siren on
			speed = speed * heartLeftSpeedMultiplier;
			speedModified = true;
			Invoke("clearSpeed", 0.15f);
		}

		movement_vector = new Vector2(rndX * speed, rndY * speed);

		if (movement_vector != Vector2.zero) {
			// TODO: fix flashing animation when diagonol, or remove diagonal
			anim.SetFloat ("DirX", movement_vector.x);
			anim.SetFloat ("DirY", movement_vector.y);
		} else {
			SetNewVector ();
		}

		rbody.MovePosition (rbody.position + (movement_vector * speed));

	}
	void OnTriggerEnter2D(Collider2D co) {
		if (co.name == "pacman") {
			Destroy (co.gameObject);
			int deathSoundIndex;
			if (gameObject.name == "cat_butt") {
				GameObject.Find ("scaleCat").GetComponent<SpriteRenderer> ().sprite = catButt;
				GameObject.Find ("scaleCat").GetComponent<Animation> ().Play ("scaleButtAnimation");
				Invoke ("resetGame", 1.5f);
				deathSoundIndex = 0;
			} else {
				GameObject.Find ("scaleCat").GetComponent<Animation> ().Play ("scaleCatAnimation");
				Invoke ("resetGame", 0.5f);
				deathSoundIndex = Random.Range (1, deathSounds.Length);
			}
			GameObject.Find ("maze").GetComponent<AudioSource> ().Pause();
			audioSource.PlayOneShot(deathSounds[deathSoundIndex]); // plays can sound
		} 
	}
	void SetNewVector() {
		dirIndex = Random.Range (0, 4);
		rndX = arrX [dirIndex];
		rndY = arrY [dirIndex];
	}
	void resetGame (){
		PacmanMove.heartCount = 0;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		PacmanMove.isNearComplete = false;
		PacmanMove.sirenIsOn = false;
	}
	private void clearSpeed()
	{
		gameObject.GetComponent<ParticleSystem> ().enableEmission = false;
	}
}
