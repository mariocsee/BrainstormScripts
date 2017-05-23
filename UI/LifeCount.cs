using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCount : MonoBehaviour {

	// player and health access
	public Transform player;
	private PlayerHealth playerHealth;

	// array of images
	private Image[] hearts;

	void Start () {
		// health reference to gain access to lives count
		playerHealth = player.GetComponent<PlayerHealth> ();
		// instantiate reference to photos (fill the array)
		hearts = this.GetComponentsInChildren<Image> ();
	}

	public void DestroyHearts() {
		// beginning lives is 3 so checking for 2 means a drop in life
		if (playerHealth.lives == 2) {
			// hides heart by making alpha 0f (invisible
			hearts [2].CrossFadeAlpha (0.0f, 1.0f, true);
		}
		if (playerHealth.lives == 1) {
			// hides subsequent heart
			hearts [1].CrossFadeAlpha (0.0f, 1.0f, true);
		}
		if (playerHealth.lives == 0) {
			// hides subsequent heart
			hearts [0].CrossFadeAlpha (0.0f, 1.0f, true);
		}
	}
}
