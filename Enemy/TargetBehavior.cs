using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Seperate class for moving target at the tutorial stage.
 */

public class TargetBehavior : MonoBehaviour {
    protected Animator anim;
    protected Rigidbody rb;
    protected bool isGrounded;
    protected EnemyHealth health;
    
    void Start() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        health = GetComponent<EnemyHealth>();
	}

    void FixedUpdate() {
       Animate();
    }

    //create to accomodate animation differences among the enemies. 
    void Animate() {
        EnemyAnimation.Animate(false, anim, rb, isGrounded, false, health.IsDead()); // apply animations 
    }

    /*Following OnCollsionStay & OnCollisionExit pair detects if the enemy object is physically on the platform.
     Will change value of isGrounded accordingly.*/
    private void OnCollisionStay(Collision collision)
    {
		if (collision.collider.CompareTag("Platform")) {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
		if (collision.collider.CompareTag("Platform")) {
            isGrounded = false;
        }
    }

}
