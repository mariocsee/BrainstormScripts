using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTrigger : MonoBehaviour {

    public Transform playerDestination;

	// consumption = 1 for one time use
	// consumption > 1 for multiple time use
	// set number to unrealistic high for unlimited uses
	public float consumption;

    void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
            other.transform.position = playerDestination.position; // assign player destination position
            other.transform.rotation = playerDestination.rotation; // and rotation
			consumption = consumption - 1; // subtract consumption per use

			// Once consumption runs out
			if (consumption <= 0) {
				// This object is destroyed so teleportation cannot be used anymore
				Destroy(transform.root.gameObject);	
			}
        }
    }
}
