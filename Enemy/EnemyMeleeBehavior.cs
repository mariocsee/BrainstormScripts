using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Manages the behavior of Melee enemy, which exists but is not implemented yet.
 *
 */
public class EnemyMeleeBehavior : EnemyBehavior {

    int attacktype;
    bool makingcontact;
    // The method the attack type at the end of each animation 
    void SetAttackType()
    {
        attacktype = Random.Range(1, 11); // 3 attacks, but 1 from 11 to give varying probability for each attack
    }

    /*This method actually deals damage.
     * The attack frame of the animation executes this method.*/
    void DealDamage(int damage) 
    {
        if (makingcontact)
        {
            if (currenttarget.name == "Player1")
            {
                p1hp.TakeDamage(damage);
            } else if (currenttarget.name == "Player2")
            {
                p2hp.TakeDamage(damage);
            }
        } 
    }

    //create to accomodate animation differences among the enemies. 
    protected override void Animate()
    {
        EnemyMeleeAnimation.Animate(isMoving(), anim, rb, isGrounded, isAttacking, health.IsDead(), attacktype); // apply animations 
    }
    /*Following OnCollsionStay & OnCollisionExit pair detects if the enemy object is physically on the platform.
    Will change value of isGrounded accordingly.*/

    private void OnTriggerEnter(Collider col)
    {
		if (col.CompareTag("Player"))
        {
            makingcontact = true;

        }
    }
    private void OnTriggerExit(Collider col)
    {
		if (col.CompareTag("Player"))
        {
            makingcontact = false;
        }
    }

}
