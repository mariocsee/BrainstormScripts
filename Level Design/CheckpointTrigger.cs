using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour {

	// This script is used to save Players most recent checkpoints and saves it to their PlayerHealth component.
	// This script is attached to a GameObject with a Collider/Collider2D configured with IsTrigger as True.
	// Then set said GameObject in specific areas on the map where the player can save checkpoints

	void OnTriggerEnter (Collider other) {
		// Checks if other is a Player based on Tag
		if (other.CompareTag ("Player")) {
			// Sets the players current checkpoint as this location for respawning
			other.GetComponent<PlayerHealth>().currentCheckpoint = transform;
		}
	}
}
