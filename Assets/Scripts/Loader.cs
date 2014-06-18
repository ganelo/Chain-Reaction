using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

	public int[,] shapeCount;
	public static int[] click_table;
	public GameObject sphere, cube, capsule, wall;
	public static GUIText score_text, click_text;

	// Use this for initialization
	void Start () {
		score_text = GameObject.Find ("Score").guiText;
		click_text = GameObject.Find ("Clicks").guiText;

		// spheres, cubes, capsules
		shapeCount = new int[,] {{5, 0, 0},
							     {10, 0, 1},
							     {20, 1, 0},
							     {30, 1, 1},
		                         {50, 1, 3}};
		GameObject[] shapes = new GameObject[] {sphere, cube, capsule};
		click_table = new int[] {1,2,3,4,5};

		float left = Camera.main.ViewportToWorldPoint (Vector3.zero).x;
		float right = Camera.main.ViewportToWorldPoint (Vector3.one).x;
		float bottom = Camera.main.ViewportToWorldPoint (Vector3.zero).y;
		float top = Camera.main.ViewportToWorldPoint (Vector3.one).y;
		int[] direction = {-1, 1};

		Object left_wall = Instantiate (wall, new Vector3(left,0,0), Quaternion.Euler (0,0,-90));
		Object right_wall = Instantiate (wall, new Vector3(right,0,0), Quaternion.Euler (0,0,90));
		Object top_wall = Instantiate (wall, new Vector3(0,top,0), Quaternion.Euler (0,0,180));
		Object bottom_wall = Instantiate (wall, new Vector3(0,bottom,0), Quaternion.identity);
		Object back_wall = Instantiate(wall, new Vector3((left+right)/2,(top+bottom)/2,5), Quaternion.Euler (-90,0,0));
		left_wall.name = "Left";
		((GameObject)left_wall).transform.localScale = new Vector3((top-bottom)/10, 1, 1);
		right_wall.name = "Right";
		((GameObject)right_wall).transform.localScale = new Vector3((top-bottom)/10, 1, 1);
		top_wall.name = "Top";
		((GameObject)top_wall).transform.localScale = new Vector3((right-left)/10, 1,1);
		bottom_wall.name = "Bottom";
		((GameObject)bottom_wall).transform.localScale = new Vector3((right-left)/10, 1,1);
		back_wall.name = "Back";
		((GameObject)back_wall).transform.localScale = new Vector3((right-left)/10, 1, (top-bottom)/10);

		GameObject shape;
		float eps;
		for (int x = 0; x < 3; x++) {
			for (int i = 0; i < shapeCount[Goal.level,x]; i++) {

				shape = (GameObject)Instantiate(shapes[x]);

				eps = shape.renderer.bounds.extents.magnitude;
				
				// Random position on screen
				shape.transform.position = new Vector3 (Random.Range (left + eps, right - eps),
				                                        Random.Range (bottom + eps, top - eps),
				                                        0);
				// Random orientation - only relevant for Cube and Capsule
				shape.transform.Rotate (new Vector3 (0, 0, Random.Range (-90F, 90F)));
				// Random lateral force
				shape.rigidbody.AddForce (new Vector3 (Random.Range (150F, 300F)*direction[Random.Range(0,2)],
				                                       Random.Range (150F, 300F)*direction[Random.Range(0,2)],
				                                       0));
				// Random angular velocity - only relevant for Cube and Capsule
				shape.rigidbody.angularVelocity = new Vector3 (0, 0, shape.rigidbody.maxAngularVelocity*direction[Random.Range (0,2)]);
			}
		}
		Spawn.clicks = click_table [Goal.level];

		Movement.goal_score = 0;

		score_text.text = "Score: " + 0 + " / " + Goal.goals[Goal.level];
		click_text.text = "Clicks: " + 0 + " / " + Loader.click_table[Goal.level];
	}

	public static void EndLevel() {
		Movement.score += 10 * Spawn.clicks;
		if (Movement.goal_score < Goal.goals[Goal.level]) {
			Application.LoadLevel("GameOver");
		} else if (Goal.level < 4) {
			Application.LoadLevel("Goal_"+(Goal.level+2));
		} else
			Application.LoadLevel ("Winner");
	}
	
	// Update is called once per frame
	void Update () {
	}
}
