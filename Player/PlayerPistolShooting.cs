using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Manages the laser beam coming out of the player's small pistol.
 * Very similar to regular rifle, hence the subclass of Playershooting.
 * But activated when character IS moving.
 */

public class PlayerPistolShooting : PlayerShooting {
    protected override void EffectOnorOff(float timer, bool isMoving) {
		string bumpername = whichSystemAreUOn + whichPlayerAreU + "Right Trigger";
		if (Input.GetAxis(bumpername) > 0.3f && timer >= timeBetweenBullets && Time.timeScale != 0 && isMoving) {
            Shoot();
        } else if (timer >= timeBetweenBullets * effectsDisplayTime) {
            DisableEffects();
        }
    }
}