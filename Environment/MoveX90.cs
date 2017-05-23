using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveX90 : MonoBehaviour {

	public GameObject referencePoint;
	public float MoveSpeed;
	public float maxDist;
	public float minDist;
	public bool direction;
	
	// Update is called once per frame
	void Update () {
		MoveStuff ();
	}

	void MoveStuff() {
		if (direction == true) {
			referencePoint.transform.position += transform.right * MoveSpeed;
			if (referencePoint.transform.position.z >= maxDist) {
				direction = false;
			}
		} 
		if (direction == false) {
			referencePoint.transform.position -= transform.right * MoveSpeed;
			if (referencePoint.transform.position.z <= minDist && direction == false) {
				direction = true;
			}
		}
	}
}
