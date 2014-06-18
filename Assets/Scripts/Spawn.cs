using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {
	public GameObject sphere;
	public static int clicks = 0;
	public static GUIText click_text;

	// Use this for initialization
	void Start () {
		click_text = GameObject.Find ("Clicks").guiText;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown() {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hitInfo;
		
		if (collider.Raycast (ray, out hitInfo, Mathf.Infinity)) {
			// Sanity check
			if (clicks <= 0) return;
			clicks--;
			click_text.text = "Clicks: " + (Loader.click_table[Goal.level] - clicks) + " / " + Loader.click_table[Goal.level];
			GameObject ball = (GameObject)Instantiate(sphere, new Vector3(hitInfo.point.x, hitInfo.point.y, 0), Quaternion.identity);
			Movement.goal_score--; // don't count our explosion towards the score
			Movement.score--;
			ball.SendMessage ("Explode");
		}
	}
}
