using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* 
 * Spawns the enmy object on the desired location
 */

public class spawn : MonoBehaviour {

    public GameObject enemy; // navagent of a character
//    private Vector3 spawnpoint; // where the object is being spawned
    public int mobno; // # of enemies wanted
	public GameObject rallyingpoint; // where will the enemies be placed. 

	// Use this for initialization
	void Start () {
//        spawnpoint = GameObject.Find(rallyingpoint).transform.position;

	}
	
	// Update is called once per frame
	void SpawnAgent () {
        if(mobno >= 1)
        {
			GameObject na = (GameObject)Instantiate(enemy, rallyingpoint.transform.position, Quaternion.identity);
            na.name = "Enemy-gun";
            --mobno;
            //Creates an instance of AI character prefab and locates it on the position of the spawner (sphere)
            //Sets the goal post for our copies.
            Invoke("SpawnAgent", Random.Range(2, 5));
        }
    }

    private void OnTriggerEnter(Collider collider) {
		if (collider.CompareTag("Player")) {
            Invoke("SpawnAgent", 1); // Invoke the function after 1 sec.
        }
    }
}
