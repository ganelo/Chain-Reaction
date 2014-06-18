using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		Vector3 vel = other.gameObject.rigidbody.velocity;
		Vector3 pos = other.transform.position;
		// Don't get stuck on the edge - shift slightly into the playing area
		if (this.name.Equals ("Left") || this.name.Equals ("Right")) {
			other.gameObject.rigidbody.velocity = new Vector3 (-vel.x, vel.y, 0);
			other.transform.position = new Vector3 (pos.x - pos.x/50, pos.y, pos.z);
		} else {
			other.gameObject.rigidbody.velocity = new Vector3 (vel.x, -vel.y, 0);
			other.transform.position = new Vector3 (pos.x, pos.y - pos.y/50, pos.z);
		}
	}
}
