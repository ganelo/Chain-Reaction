using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	string text;
	bool end = true;

	// Use this for initialization
	void Start () {
		if (Application.loadedLevelName.Equals("Winner")) {
			text = "You Win!\nFinal Score: "+Movement.score;
		} else if (Application.loadedLevelName.Equals ("GameOver")) {
			text = "Game Over :(\nFinal Score: "+Movement.score;
		} else {
			text = "Chain Reaction";
			end = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		// Make a group on the center of the screen
		GUI.BeginGroup (new Rect (Screen.width / 2 - 50, Screen.height / 2 - 50, 120, 140));
		// All rectangles are now adjusted to the group. (0,0) is the topleft corner of the group.

		GUI.Box (new Rect (0,0,120,140), text);
		string play_text;
		if (end) {
			play_text = "Play again?";
		} else {
			play_text = "Play";
		}
		if (GUI.Button (new Rect (10,40,100,30), play_text)) {
			Movement.score = 0;
			Application.LoadLevel ("Goal_1");
		}
		if (GUI.Button (new Rect(10,80,100,30), "Quit")) {
			Application.Quit();
		}

		GUI.EndGroup ();
	}
}
