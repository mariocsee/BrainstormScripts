using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayer : MonoBehaviour {

	// This script is so check whether the players have arrived on a platform.
	// This script should be attached to a GameObject that has its Collider or Collider2D configured with isTrigger as True.
	// This is twice for each Player and is used for checking if both players have reached the end of the level

	public string playerName;
	public bool isPlayerHere;

	// Use this for initialization
	void Start () {
		isPlayerHere = false;
	}
		
	void OnTriggerEnter(Collider other) {
		// Checks if other is a Player based on Tag and if other is of the correct PlayerName
		if (other.CompareTag ("Player") && other.gameObject.name == playerName) {
			// Sets isPlayerHere as True while the player has entered but not exited the trigger
			isPlayerHere = true;
		} 
	}

	void OnTriggerExit(Collider other) {
		// Checks if other is a Player based on Tag and if other is of the correct PlayerName
		if (other.CompareTag ("Player") && other.gameObject.name == playerName) {
			// Sets isPlayerHere as False when the player has exited the trigger
			isPlayerHere = false;
		} 
	}
}
