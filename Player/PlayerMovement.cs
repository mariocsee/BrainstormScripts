using UnityEngine;

// PlayerMovement script is attached to Player 

/* The PlayerMovement script is responsible for:
 * a) moving the player around and rotating based on movement direction
 * b) running (increasing speed)
 * c) jumping (applying force upward)
 * d) strafing (controlling rotation while moving)
 * e) determining controls and input names
 * f) pausing the game (by gaining access to the in game menu control script)
 * g) player animation and sound effects
 */ 

public class PlayerMovement : MonoBehaviour
{
	// Speed related variables
	private float speed = 8f;
	private float runSpeed = 13f;
	private float jumpSpeed = 20f;
	private float fallSpeed = 12f;

	// Smoothing value to movements aren't extremely sudden
	private float turnSmoothing = 5f;

	// for movement and force
	private Vector3 movement;
	private Rigidbody rb;

	// True when grounded; false when in air. Necessary for jumping.
	private bool isGrounded;

	// health reference
    private PlayerHealth health;

	// Determining identity of player and system
	private string whoUAre;
	private string whichPlayerAreU;
	private string whichSystemAreUOn;

	// Reference to pause menu and menu controls
	public Canvas pauseMenu;
	private InGameMenuControl inGameMenuControl;
	public bool isPaused;

	// Initialize control names
	private string P_RightTrigger;
	private string P_Start;
	private string P_LeftHorizontal;
	private string P_LeftVertical;
	private string P_RightHorizontal;
	private string P_RightVertical;
	private string P_LeftBumper;
	private string P_Jump;

	private Animator anim; // animations
    private AudioSource[] sounds; // multiple audios

    void Awake()
	{
		// instantiate reference to components: rigidbody, animator, and player health
		rb = GetComponent<Rigidbody> ();
		health = GetComponent<PlayerHealth>();
        anim = GetComponent<Animator> ();

		// [0] is Jump, [1] is walking, [2] is shooting, [3] is dying
        // Note that the field "sounds" is an array.
        sounds = GetComponents<AudioSource>();

		// Game starts as unpaused
		isPaused = false;

		// Sets player control depending on which player this is
		whoUAre = gameObject.name;
		if (whoUAre == "Player1") {
			whichPlayerAreU = "P1_";
		} else if (whoUAre == "Player2") {
			whichPlayerAreU = "P2_";
		}

		whichSystemAreUOn = SystemInfo.operatingSystemFamily.ToString () + "_"; // returns MacOSX_ if game is running on Mac or Windows_ if Windows

		// Assigns controls based on which player you are and which system you are playing on (because controller mapping is different across platforms)
		P_RightTrigger = whichSystemAreUOn + whichPlayerAreU + "Right Trigger"; // eg "MacOSX_P1_Right Trigger"
		P_Start = whichSystemAreUOn + whichPlayerAreU + "Start"; // eg "Windows_P2_Start"
		P_LeftHorizontal = whichSystemAreUOn + whichPlayerAreU + "Left Horizontal";
		P_LeftVertical = whichSystemAreUOn + whichPlayerAreU + "Left Vertical";
		P_RightHorizontal = whichSystemAreUOn + whichPlayerAreU + "Right Horizontal";
		P_RightVertical = whichSystemAreUOn + whichPlayerAreU + "Right Vertical";
		P_LeftBumper = whichSystemAreUOn + whichPlayerAreU + "Left Bumper";
		P_Jump = whichSystemAreUOn + whichPlayerAreU + "Jump";

		// Instantiate reference to pause menu control
		inGameMenuControl = pauseMenu.GetComponent<InGameMenuControl> ();
    }

	void Update() {
		if (!isPaused) {
			// Can only happen when unpaused
			Jump ();
			if (rb.velocity.y < 0 && !isGrounded) {
				rb.velocity = Vector3.down * fallSpeed; // increase fall speed 
			}
		}
	}

	void FixedUpdate() {
		if (!isPaused) {
			// Can only happen when unpaused
			Pause ();
			Move();
			PlayerAnimation.Animate(isMoving(), anim, rb, isGrounded, (Input.GetAxis(P_RightTrigger)>0.3f), health.IsDead());
		}
    }

	void Pause() {
		// If start button on Xbox Controller is pressed
		if (Input.GetButton(P_Start)) {
			// Pauses the game
			inGameMenuControl.PauseGame();
		}
	}
		
	void Move () {
		// Input from left joystick
		float moveHorizontal = Input.GetAxisRaw (P_LeftHorizontal);
		float moveVertical = Input.GetAxisRaw (P_LeftVertical);

		// Input from right joystick
		float rotateHorizontal = Input.GetAxisRaw (P_RightHorizontal);
		float rotateVertical = Input.GetAxisRaw (P_RightVertical);

		// Movement determined by left joystick
		movement.Set (moveHorizontal, 0f, moveVertical);

		// If left bumper is pressed
		if (Input.GetButton(P_LeftBumper)) {
			// running
			movement = movement.normalized * runSpeed * Time.deltaTime; // runSpeed is configured higher than speed
		} 
		else {
			// walking
			movement = movement.normalized * speed * Time.deltaTime;
		}

		//clamp Y so no movement accidentally jumps
		Vector3 clampedMovement = movement;
		clampedMovement.y = 0f;

		// movement
		rb.MovePosition (transform.position + clampedMovement);

		// If the right joystick is not in use
		if (Mathf.Abs(rotateVertical) < 0.1f && Mathf.Abs(rotateHorizontal) < 0.1f) {
			// If the left joystick is in use
			if ((Mathf.Abs (moveHorizontal) > 0.5f || Mathf.Abs (moveVertical) > 0.5f)) {
				// rotate to movement direction (normal rotation based on movement direction)
				Quaternion targetRotation = Quaternion.LookRotation (new Vector3 (movement.x, 0f, movement.z), Vector3.up);
				Quaternion newRotation = Quaternion.Lerp (rb.rotation, targetRotation, turnSmoothing * Time.deltaTime);
				rb.MoveRotation(newRotation);
			}		
		} 
		// If right joystick in use
		else if (Mathf.Abs(rotateVertical) > 0.1f || Mathf.Abs(rotateHorizontal) > 0.1f)  {
			// rotate based on right joystick direction (strafing)
			Quaternion targetRotation = Quaternion.LookRotation (new Vector3 (rotateHorizontal, 0f, rotateVertical), Vector3.up);
			Quaternion newRotation = Quaternion.Lerp (rb.rotation, targetRotation, turnSmoothing * Time.deltaTime);
			rb.MoveRotation(newRotation);
		}
	}

	// Add upward force which makes character "jump"
	void Jump () {
		if(Input.GetButtonDown (P_Jump) && isGrounded) {
			rb.velocity = Vector3.up * jumpSpeed;
		}
	}

    //Checks if the player is moving or not
    public bool isMoving() {
        return (Mathf.Abs(movement.x) >= 0.01f) || (Mathf.Abs(movement.z) >= 0.01f);
    }

    //Animation will play this method at appropriate timing to play the sound.
    void playSound(int whichsound) {
        sounds[whichsound].Play();
    }

    /*Following OnCollsionStay & OnCollisionExit pair detects if player is physically on the platform.
     Will change value of ontheGround Accordingly.*/
    private void OnCollisionStay(Collision collision) {
		if(collision.collider.CompareTag("Platform")) {
			isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision) {
		if(collision.collider.CompareTag("Platform")) {
			isGrounded = false;
        }
    }

}