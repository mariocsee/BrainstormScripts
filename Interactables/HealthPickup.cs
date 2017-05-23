using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour {

	public GameObject Self;
	public GameObject Player1;
	public GameObject Player2;
	PlayerHealth Player1Health;
	PlayerHealth Player2Health;
	public int healthIncrease;

	// Use this for initialization
	void Start () {
		// Gets PlayerHealth component from each Player
		Player1Health = Player1.GetComponent<PlayerHealth> ();
		Player2Health = Player2.GetComponent<PlayerHealth> ();
	}

	void OnTriggerEnter(Collider other) {
		// Checks if other is a player based on tag
		if (other.CompareTag("Player")) {
			// Checks if they're player 1 or two
			if (other.gameObject == Player1) {
				// Adds the health increase to their current health
				Player1Health.currentHealth = Player1Health.currentHealth + healthIncrease;
				// Caps the health at 100 maximum if the player's health goes above
				if (Player1Health.currentHealth > 100) {
					Player1Health.currentHealth = 100;
				}
				// Sets the UI HealthSlider to match the new updated health
				Player1Health.healthSlider.value = Player1Health.currentHealth;
				// Destroys self so the item cannot be "picked up" again
				Destroy(Self);
			} else if (other.gameObject == Player2) {
				// Adds the health increase to their current health
				Player2Health.currentHealth = Player2Health.currentHealth + healthIncrease;
				// Caps the health at 100 maximum if the player's health goes above
				if (Player2Health.currentHealth > 100) {
					Player2Health.currentHealth = 100;
				}
				// Sets the UI HealthSlider to match the new updated health
				Player2Health.healthSlider.value = Player2Health.currentHealth;
				// Destroys self so the item cannot be "picked up" again
				Destroy(Self);
			}
		}
	}
}
