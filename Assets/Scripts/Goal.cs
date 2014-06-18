using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	float timer;
	public static int level;
	public static int[] goals;

	// Use this for initialization
	void Start () {
		timer = 3;
		level = int.Parse (Application.loadedLevelName.Split ('_') [1])-1;

		goals = new int[] {2, 5, 10, 20, 50};
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI() {
		// Make a group on the center of the screen
		GUI.BeginGroup (new Rect (Screen.width / 2 - 50, Screen.height / 2 - 50, 100, 100));
		// All rectangles are now adjusted to the group. (0,0) is the topleft corner of the group.

		GUI.Box (new Rect (0,0,100,25), "Get "+goals[level]+" Shapes");
		timer -= Time.deltaTime;
		if (timer <= 0) {
			Application.LoadLevel("L_"+(level+1));
		}


		GUI.EndGroup ();
	}
}
