using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * CharacterAnimation is an animation template class
 * Checks the state of player and plays corrsponding animation
 */
public static class CharacterAnimation{

    /*
     * See if a target character is touching the ground or not
     */
    public static bool GroundDetecting(Rigidbody rb) {
        float ground_distance = 3f; // distance between player and the ground. 
        LayerMask Ground = LayerMask.NameToLayer("Ground"); // objects in ground are in "Ground" layer.
        return (Physics.Raycast(rb.transform.position, Vector3.down, ground_distance, Ground) && (rb.velocity.y <= 0.2f));
        //will return true if character is only 3 unit away from the ground
        //this is because landing animation must play few seconds before the character touches the ground
    }

    /*Actual animate method*/
    public static void Animate(bool movingornot, Animator anim, Rigidbody rb, bool grounded, bool isAttacking, bool isDead)
    {   
        anim.SetBool("isRunning", movingornot); 
        anim.SetBool("isGrounded", GroundDetecting(rb) || grounded); // True if (3 unit away from the ground OR actually touching the ground)
        anim.SetBool("isAttacking", isAttacking);
        anim.SetBool("isDead", isDead);
    }
}

/*
 * Class specifically designed for player
 */
public static class PlayerAnimation {
    public static void Animate(bool movingornot, Animator anim, Rigidbody rb, bool grounded, bool weapon, bool isDead)  {
        CharacterAnimation.Animate(movingornot, anim, rb, grounded, weapon, isDead);
    }
}

/*
 * Class specifically designed for enemy(with gun)
 */
public static class EnemyAnimation {
    public static void Animate(bool movingornot, Animator anim, Rigidbody rb, bool grounded, bool isAttacking, bool isDead) {
        CharacterAnimation.Animate(movingornot, anim, rb, grounded, isAttacking, isDead);
    }
}

/*
 * Class specifically designed for enemy(melee)
 */

public static class EnemyMeleeAnimation {
    public static void Animate(bool movingornot, Animator anim, Rigidbody rb, bool grounded, bool isAttacking, bool isDead, int attacktype) {
        CharacterAnimation.Animate(movingornot, anim, rb, grounded, isAttacking, isDead);
        anim.SetInteger("attacktype", attacktype);
    }
}
