using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class makes sure that the player object moves with the platform
 * when it is on the moving platform
 */

public class HoldCharacterBetter: MonoBehaviour {

	// when the player is on the platform
 	private void OnTriggerEnter(Collider col) {
		if (col.CompareTag("Player")) {
            col.transform.parent = transform.parent;
			//this puts player object a child of the platform object
			//...which guarentees that player will move along with the moving platform. 
        }
    }
    
	// when the player is no longer on the platform...
    private void OnTriggerExit(Collider col) {
		if (col.CompareTag("Player")) {
			col.transform.parent = null;  // no longer the parent of a moving object
        }
    }
}
