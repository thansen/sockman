using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour {
	
	public static bool isPoweredUp;

	// Use this for initialization
	void Start () {
		isPoweredUp = false;
	}

	void OnTriggerEnter2D(Collider2D co) {
		if (co.name == "pacman")
			Destroy (gameObject);
		}


}
