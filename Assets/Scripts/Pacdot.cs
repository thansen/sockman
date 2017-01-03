using UnityEngine;
using System.Collections;

public class Pacdot : MonoBehaviour {
	
	public SpriteRenderer heartSpriteRenderer;
	public Sprite[] heartSprites;
	int randomHeartIndex;

	// Use this for initialization
	void Start () {
		randomHeartIndex = Random.Range (0, heartSprites.Length);
		heartSpriteRenderer.sprite = heartSprites [randomHeartIndex];
	}
	void FixedUpdate () {
		if (Powerup.isPoweredUp == true) {
			heartSpriteRenderer.sprite = heartSprites [9];
		} else {
			heartSpriteRenderer.sprite = heartSprites [randomHeartIndex];
		}
	}
	void OnTriggerEnter2D(Collider2D co) {
		if (co.name == "pacman")
			Destroy (gameObject);
		}
}
