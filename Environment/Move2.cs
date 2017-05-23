using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2 : MonoBehaviour {

	// Creates a reference to which stage piece groups should move
	public GameObject referencePoint;

	// Determine move speed of stage blocks
	public float MoveSpeed;

	// Determine the maximum Z and minimum Z value to limit movement along the Z-axis
	public float maxBound;
	public float minBound;

	// Direction = true means it starts moving on towards positive Z while false makes it start moving to the negative.
	public bool Direction;

	// Use this for initialization
	void Start() {
	}

	// Update is called once per frame
	void Update () {
		MoveBlocks ();
	}

	void MoveBlocks() {
		if (Direction == true) {
			// Moves the stage block towards positive Z
			referencePoint.transform.position += transform.forward * MoveSpeed;
			// Once it reaches the maximum boundary, it sets Direction to false to it will move towards negative Z
			if (referencePoint.transform.position.z >= maxBound && Direction) {
				Direction = false;
			}
		} 
		if (Direction == false) {
			// Moves the stage block towards negative Z
			referencePoint.transform.position -= transform.forward * MoveSpeed;
			if (referencePoint.transform.position.z <= minBound && !Direction) {
				// Once it reaches the minimum boundary, it sets Direction to true to it will move towards positive Z
				Direction = true;
			}
		}
	}
}