using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script for Shooting Enemy.
 * Does not inlude the script for the bullet,
 * but controlls animation and movement (namely following the player, facing the player when the distance is close enough)
 * */
public class EnemyBehavior : MonoBehaviour {

    protected UnityEngine.AI.NavMeshAgent agent;
    protected Animator anim;
    protected Rigidbody rb;
    protected bool isGrounded;
    protected bool isAttacking;
    protected EnemyHealth health; //its own health

    protected GameObject player1; // p1
    protected GameObject player2;
    protected GameObject currenttarget; // tell which character enemy is targeting

    protected PlayerHealth p1hp; // hp of both players.
    protected PlayerHealth p2hp;


    void Start () {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>(); // animatoin controller
        rb = GetComponent<Rigidbody>();
        health = GetComponent<EnemyHealth>();
        isAttacking = false;
        player1 = GameObject.Find("Player1"); // Finds player 1, using its name on the scene
        player2 = GameObject.Find("Player2"); // Finds player 2 by name
        p1hp = player1.GetComponent<PlayerHealth>();
        p2hp = player2.GetComponent<PlayerHealth>();
    }
    void FixedUpdate()
    {
        if (health.IsDead() == false) // behavior if the character is alive
        {
            ChooseTarget(player1, player2); // which player to attack
            ApproachAndAttack(); // follow and player attack animation
            Animate();// keep updating animation controller
        } else if (health.IsDead()) // when it is dead
        {
            agent.Stop(); // stop following player
            Animate(); // keep updating animation controller
        }
        

    }

    /*The enemy approaches towards the player
      When close enough, face towards the player and attack */
    void ApproachAndAttack() {
        Vector3 ctargetposition = currenttarget.transform.position; //current target position
        float remainingdistance =  Mathf.Abs(Vector3.Distance(ctargetposition, transform.position));
        //Above variable: Unity Logic is absolutely nonsense, so making own distance calculator. 
        if (remainingdistance <= agent.stoppingDistance)
        {
            agent.Stop(); // stop going toward the player, since already close enough
            float rotationspeed = 10f; // how quickly this enemy rorates. 
            Vector3 direction = ctargetposition - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationspeed * Time.deltaTime);
            //slowly transition from current rotation to the target rotation. 
            isAttacking = true; // play attack animation
        } else{
            isAttacking = false; // do not play attack animation
            agent.Resume(); // keep following the player. 
        }
       
    }

    //create to accomodate animation differences among the enemies. 
    protected virtual void Animate()
    {
        EnemyAnimation.Animate(isMoving(), anim, rb, isGrounded, isAttacking, health.IsDead()); // apply animations 

    }

    void ChooseTarget(GameObject p1, GameObject p2)
    {
        float distp1 = Vector3.Distance(p1.transform.position, transform.position); // distance btween enemy and p1
        float distp2 = Vector3.Distance(p2.transform.position, transform.position); // same, but btween p2
        float significantdistance = 0.2f; // distp1 and distp2 are significantly different if their difference is greater than 0.2f

        if(Mathf.Abs(distp1 - distp2) >= significantdistance) // if one of the player is significantly closer from the other...
        {
            if(distp1 < distp2) //p1 is closer
            {
                agent.SetDestination(p1.transform.position);
                currenttarget = p1;
            } else if (distp2 < distp1)
            {
                agent.SetDestination(p2.transform.position);
                currenttarget = p2;
            }
        } else // if neither of the players are "significantly" closer  to this (enemy)...
        {
            if (p1hp.getHealth() < p2hp.getHealth()) //p1 is weaker
            {
                agent.SetDestination(p1.transform.position); // get the weaker guy
                currenttarget = p1;
            } else if (p1hp.getHealth() > p2hp.getHealth())
            {
                agent.SetDestination(p2.transform.position);
                currenttarget = p2;
            }
        }
    }
    /*
     * is chararacter moving or not
     */
    public bool isMoving()
    {
        return (Mathf.Abs(agent.velocity.x) >= 0.4f) || (Mathf.Abs(agent.velocity.z) >= 1.1f);
    }

    /*Following OnCollsionStay & OnCollisionExit pair detects if the enemy object is physically on the platform.
     Will change value of isGrounded accordingly.*/
    private void OnCollisionStay(Collision collision)
    {
		if (collision.collider.CompareTag("Platform")) // if this object collides with the platform (floor)...
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
		if (collision.collider.CompareTag("Platform")) // if this object disengages (collisionexits) from the platform (floor)...
        {
            isGrounded = false;
        }
    }

}
