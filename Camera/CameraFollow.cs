using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The CameraFollow script is attached to the main camera

/* The CameraFollow script determines the following:
* a) the camera follows both players based on their midpoint, so both players are seen
* b) it locks the players to a maximum distance apart from each other for core mechanics and to ensure both are on screen
* c) the camera distance zooms in and out depending on the distance between the players
* d) if one of the players die, that the camera focuses on the surviving player
*/

public class CameraFollow : MonoBehaviour {

	// Movement/following smoothing
	public float dampTime = 0.3f;

	// Keep track of Player1 (target1) and Player2 (target2)
	public Transform target1;
	public Transform target2;

	// Distance vector3 between Player1 and Player2
	public Vector3 distance;
	// Camera offset (basically zoom)
	public float CamOffset;
	// Keep track of average X, Y, Z values between Player1 and Player2
	private float MidX;
	private float MidZ;
	// Midpoint vector3 between Player1 and Player2
	private Vector3 Midpoint;

	// xBounds * 2 is the maximum distance between players on the X-axis
	private float xBounds;
	// zBounds * 2 is the maximum distance between players on the Z-axis
	private float zBounds;

	// PlayerHealths for both Player1 and Player2
	private PlayerHealth target1Health;
	private PlayerHealth target2Health;
	private Vector3 velocity;

	// Do not forget to assign public variables in Unity when necessary

	void Start () 
	{
		Vector3 velocity = Vector3.zero;
		// max X-axis distance is then 80
		xBounds = 40.0f;
		// max Z-axis distance is then 30
		zBounds = 15.0f;
		// max distance is set between Player1 and Player2 to ensure both are still onscreen and for core mechanics

		// Gets the PlayerHealth component in each player to see if they're still alive
		target1Health = target1.GetComponent<PlayerHealth> ();
		target2Health = target2.GetComponent<PlayerHealth> ();

	}

	void Update() {
		// If one of the players dies, the camera takes the only surviving player as both targets
		if (target1Health.isDeadForever) {
			target1 = GameObject.Find ("Player2").transform;
		} else if (target2Health.isDeadForever) {
			target2 = GameObject.Find ("Player1").transform;
		}
			
		// Distance between targets
		distance = target1.position - target2.position;

		// If distance becomes negative in any case make it positive
		if(distance.x < 0)
			distance.x = distance.x * -1;
		if(distance.z < 0)
			distance.z = distance.z * -1;

		// Keeping track of the midpoint (only X and Z axis) between the players
		MidX = (target2.position.x + target1.position.x) /2; 
		MidZ = (target2.position.z + target1.position.z) /2;
		Midpoint = new Vector3 (MidX, 0, MidZ);

		// X bounds makes sure the the players can only be separated for the set distance on the X-axis
		if(target1.position.x < (transform.position.x -xBounds))
		{
			Vector3 pos = target1.position;
			pos.x =  transform.position.x -xBounds;
			target1.position = pos;
		} 
		if(target2.position.x < (transform.position.x -xBounds))
		{
			Vector3 pos = target2.position;
			pos.x =  transform.position.x -xBounds;
			target2.position = pos;
		} 
		if(target1.position.x > (transform.position.x +xBounds))
		{
			Vector3 pos = target1.position;
			pos.x =  transform.position.x +xBounds;
			target1.position = pos;
		} 
		if(target2.position.x > (transform.position.x +xBounds))
		{
			Vector3 pos = target2.position;
			pos.x =  transform.position.x +xBounds;
			target2.position = pos;
		}

		// Z Bounds makes sure the the players can only be separated for the set distance based on Z-axis
		if(target1.position.z < (Midpoint.z -zBounds))
		{
			Vector3 pos = target1.position;
			pos.z =  Midpoint.z - (zBounds);
			target1.position = pos;
		} 
		if(target2.position.z < (Midpoint.z -zBounds))
		{
			Vector3 pos = target2.position;
			pos.z = Midpoint.z - (zBounds);
			target2.position = pos;
		}
		if(target1.position.z > (Midpoint.z +zBounds))
		{
			Vector3 pos = target1.position;
			pos.z = Midpoint.z + (zBounds);
			target1.position = pos;
		}
		if(target2.position.z > (Midpoint.z +zBounds))
		{
			Vector3 pos = target2.position;
			pos.z =  Midpoint.z + (zBounds);
			target2.position = pos;
		}


		// Changing the CameraOffset based on how far the players are from each other
		if (distance.x > 20.0f) {
			CamOffset = distance.x * 0.5f;
			// Maximum camOffset (camera distance) is 35f
			if (CamOffset >= 35f)
				CamOffset = 35f;
		} else if (distance.x < 20.0f) {
			CamOffset = distance.x * 0.5f;
		} else if (distance.z < 20.0f) {
			CamOffset = distance.x * 0.5f;
		}

		// If either target is present
		if (target1 || target2)
		{
			// Calculates the necessary movement
			Vector3 delta = Midpoint - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 20f + CamOffset)); //(new Vector3(0.5, 0.5, point.z));
			// Sets destination
			Vector3 destination = transform.position + delta;
			// Smoothly moves camera from current location to destination
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}
	}

}

