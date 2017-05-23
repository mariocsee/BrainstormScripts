using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationTrigger : MonoBehaviour {

	// This script works by setting the Player position and rotation to the Destination position and rotation
	// to teleport/transport players to a specific location in the scene

	// Takes a Transform argument determining PlayerDestination
	public Transform playerDestination;

	void OnTriggerEnter(Collider other) {
		// Checks if other is a Player based on Tag
		if (other.CompareTag ("Player")) {
			// Sets player position and rotation as the Destination's
			other.transform.position = playerDestination.position;
			other.transform.rotation = playerDestination.rotation;
		}
	}
}
