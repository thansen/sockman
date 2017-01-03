using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {

	public void onClickQuit(){
		// Save game data

		// Close game
		Debug.Log ("Application Closing");
		Application.Quit ();
	}
	public void onClickDonate(){
		Application.OpenURL("http://sockcoprocks.com");
	}
}
