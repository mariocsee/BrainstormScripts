using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move4 : MonoBehaviour {

	// Creates reference to moving platform
	public GameObject platform;
	// platform movespeed
	public Vector3 Movespeed;
	// platform move time
	public float MoveTime;
	// true when trigger is received from raycast hit / false when that hasn't occurred
	public bool pushed;
	// keep track of time
	float timer;

	// Use this for initialization
	void Start() {
		pushed = false;
	}

	// Update is called once per frame
	void Update () {
		if (pushed) {
			// keep track of time 
			timer += Time.deltaTime;
			if (timer <= MoveTime) {
				// move platform based on speed
				platform.transform.Translate (Movespeed * Time.deltaTime);
			}
		}
	}

	public void Trigger() {
		pushed = true;
	}
}