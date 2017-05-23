using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour {

	void OnTriggerEnter(Collider collider) {
		// Checks if other collider is a Player based on Tag
		if (collider.CompareTag("Player")) {
			// If the player then hits the use button
			if (Input.GetButton ("Use")) {
				// This will run the command below
				Interact (collider.transform); 
			}
		}
	}

	// public virtual allows other scripts to call this and extends the OnTriggerEnter function above (eg. PressButton.cs)
	public virtual void OnInteract(Transform item) {
		// Insert desired behavior here
	}

	void Interact(Transform item) {
		OnInteract (item);
	}

}
