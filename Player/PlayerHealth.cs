using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

// The PlayerHealth script is attached to each player 

/* The PlayerHealth script is responsible for:
 * a) Keeping track of the player's health points and reflecting that onto the HUD Health UI Slider
 * b) Calling the death function when the player falls or health points reach 0
 * c) Respawning the player in the proper point
 * d) Taking damage (it's a public function called by enemies)
 * e) Keeping track of number of lives  and reflecting that onto the HUD Health UI Hearts
 * f) Calling game over when both players are dead
 * g) IsDead and CurrentHealth are public functions called by enemies to check for following
 */

public class PlayerHealth : MonoBehaviour
{
	// Initial integer values for start health and current health
    int startingHealth;
	// public because it is accessed by HealthPickup
    public int currentHealth;
	// Reference to health slider
    public Slider healthSlider;

	// References to playerMovement and playerShooting scripts
	PlayerMovement playerMovement;
	PlayerShooting playerShooting;
	bool isDead;

	// life count (public because it is accessed by the Health UI)
	public int lives;
	// Reference to parent of hearts images in health UI
	public Transform lifeHearts;
	// Reference to LifeCount script that keeps track of presenting hearts based on lives left
	private LifeCount lifeCount;

	// Checkpoints for respawning
	Rigidbody rb;
	GameObject checkpoint; 
	CheckpointTrigger checkpointTrigger;
	public Transform currentCheckpoint;
	private Vector3 respawnCheckpoint; 
	private Vector3 originalSpawn;

	// Reference to GameOver canvas where game over function is located
	public Canvas GameOver;

	// Reference to other player and to check if alive or dead
	public GameObject otherPlayer;
	private PlayerHealth otherPlayerHealth;
	public bool isDeadForever;


    void Awake ()
    {
		// Initialize references to movement and shooting
		playerMovement = GetComponent <PlayerMovement> ();
        playerShooting = GetComponentInChildren <PlayerShooting> ();

		// Initialize reference to the health of other player to check for isDeadForever when determining if both players are dead
		otherPlayerHealth = otherPlayer.GetComponent<PlayerHealth> ();

		// Set health to starting health value determined (in this case, 100)
        currentHealth = startingHealth;

		// Initial access to the UI hearts representing life in the HUD Health UI
		lifeCount = lifeHearts.GetComponent<LifeCount> ();

		// Get player RigidBody and original position
		rb = GetComponent<Rigidbody> ();

		// Save original position
		originalSpawn = rb.transform.position;

		// Set as false since the player just started
		isDeadForever = false;
        isDead = false;

		// Set initial lives count to 3
		lives = 3;
		startingHealth = 100;
    }

    public void TakeDamage (int amount)
    {
		// Subtract current health by amount specified (this amount is specified in Enemy scripts
        currentHealth -= amount;
		// Make the health slider (HP Bar) reflect the damage
        healthSlider.value = currentHealth;

		// If health goes to or below 0
        if(currentHealth <= 0 )
        {
			// the player dies / call death function
			Death ();
        }
    }

	/* There exists a quad trigger outside and below the level that checks if the player fell off the map
	* This will trigger a death 
	*/
	void OnTriggerEnter(Collider collision) {
		if(collision.CompareTag("FallDeath")) {
			Death ();
		}
	}

    void Death ()
    {
		isDead = true;

		// Disable PlayerMovement and Shooting while dead
        playerMovement.enabled = false;
        playerShooting.enabled = false;

		// Subtract a live
		lives = lives - 1;
		// Destory one of the hearts in the health UI
		lifeCount.DestroyHearts ();

		// If there are remaining lifes
		if (lives > 0) {
			// bring me to life... WAKE ME UP WAKE ME UP INSIDE SAAAAVE ME
			Respawn ();
		} 
		// If this player runs out of lives,
		else if (lives == 0 || lives < 0) {
			// this player is dead forever.
			isDeadForever = true;

			// Set Health Slider (HP Bar) to alpha of 0 in 1 second / Make it disappear
			healthSlider.GetComponentsInChildren<Image> () [0].CrossFadeAlpha (0.0f, 1.0f, true);
			healthSlider.GetComponentsInChildren<Image> () [1].CrossFadeAlpha (0.0f, 1.0f, true);

			// If the other player is also dead, 
			if (otherPlayerHealth.isDeadForever) {
				// game over function is triggered!
				GameOver.GetComponent<GameOverMenuControl>().GameOver();
			} 
		}
    }

	void Respawn() {
		// If there exists no currentCheckpoint saved
		if (!currentCheckpoint) {
			// Use the original spawn as the respawn point
			respawnCheckpoint = originalSpawn;
		} else if (currentCheckpoint) {
			// else set respawn point at the currentCheckpoint
			respawnCheckpoint = currentCheckpoint.position;
		}

		isDead = false;

		// Respawn the character at the respawn point
		rb.transform.position = respawnCheckpoint;

		// Reset HP to 100
		currentHealth = startingHealth;
		// Set healthSlider to reflect the refilled HP
		healthSlider.value = currentHealth;

		// PlayerMovement and Shooting are re-enabled
        playerMovement.enabled = true;
		playerShooting.enabled = true;
	} 

	// For enemies to check if player isDead or if at a certain health
    public bool IsDead()
    {
        return isDead;
    }

    public int getHealth()
    {
        return currentHealth;
    }
}
