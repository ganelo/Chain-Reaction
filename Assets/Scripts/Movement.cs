using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	
	public Material exploded_mat;
	public bool isExploded = false;
	public static GUIText score_text;
	float clock;
	public static int goal_score = 0;
	public static int score = 0;

	// Use this for initialization
	void Start () {
		score_text = GameObject.Find ("Score").guiText;
	}

	void FixedUpdate() {
		Vector3 scl = transform.localScale;
		// Is this shape exploded?
		if (!isExploded)
			return;
		if (clock > 0)
			clock -= Time.deltaTime;
		else {
			// Time's up, so shrink
			transform.localScale = new Vector3(scl.x - Time.deltaTime,
			                                   scl.y - Time.deltaTime,
			                                   scl.z);
			// Too small to see, so die
			if (scl.x < 0.1) {
				GameObject[] shapes = GameObject.FindGameObjectsWithTag ("Shape");
				int exploded = 0;
				for (int i = 0; i < shapes.Length; i++) {
					if (shapes[i].GetComponent<Movement>().isExploded)
						exploded++;
				}
				if ((Spawn.clicks <= 0 || GameObject.FindGameObjectsWithTag("Shape").Length==1) && exploded == 1) {
					Loader.EndLevel();
				}
				transform.localScale *= Random.Range (1f, 1.3f);
				if (scl.x < 0.1)
					Destroy (this.gameObject);
			}
		}
		if (name.Contains ("Cube")) {
			Gravity (10, 1.5F);
		}
	}

	void Gravity(int radius, float multiplier) {
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
		for (int i = 0; i < hitColliders.Length; i++) {
			if (hitColliders[i].tag.Equals ("Wall")) {
				continue;
			}
			Vector3 relativePos = hitColliders[i].transform.position - transform.position;
			hitColliders[i].rigidbody.AddForce(relativePos.x*-multiplier*Time.deltaTime,
			                                   relativePos.y*-multiplier*Time.deltaTime,
			                                   0,
			                                   ForceMode.VelocityChange);
		}
	}
	
	public void Explode() {
		goal_score++;
		score++;
		score_text.text = "Score: " + goal_score + " / " + Goal.goals[Goal.level];
		isExploded = true;
		rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		if (name.Contains ("Sphere")) {
			transform.localScale = new Vector3(transform.localScale.x*4,
			                                   transform.localScale.y*4,
			                                   transform.localScale.z);
			clock = 2;
		} else if (name.Contains ("Capsule")) {
			transform.localScale = new Vector3(transform.localScale.x,
			                                   100,
			                                   transform.localScale.z);
			clock = 0;
		} else if (name.Contains("Cube")) {
			renderer.material = exploded_mat;
			clock = 4;
		}
	}
	
	void OnTriggerEnter(Collider other) {
		// Did we hit a wall?
		if (other.tag == "Wall")
			return;

		// Only act if 1 party is exploded
		if (isExploded == other.GetComponent<Movement>().isExploded) {
			return;
		}

		if (!isExploded && other.GetComponent<Movement>().isExploded) {
			Explode ();
			return;
		} 

		// We are exploded and other is not
		other.SendMessage ("Explode");
	}
}
