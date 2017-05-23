using UnityEngine;
using System;

/* This class Determines when enemy will fire the bullet
 */
public class EnemyShooting : MonoBehaviour
{
    public int damagePerShot = 8;
    public double timingrange1 = 0.28; // animation frame in which laser will appear
    public double timingrange2 = 0.33; // frame in which laser will disappear
    public float range = 30f;

    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
     
    Animator enemyanimator;
    string whichPlayerUAre;

    void Awake()
    {
        shootableMask = LayerMask.GetMask("Player");
        gunParticles = GetComponent<ParticleSystem>(); // Fire effect at the puzzle.
        gunLine = GetComponent<LineRenderer>(); //Laser Beam
        gunAudio = transform.root.gameObject.GetComponent<AudioSource>(); // Audio source 
        gunLight = GetComponent<Light>();
        
        enemyanimator = transform.root.gameObject.GetComponent<Animator>(); // 

  
    }


    void Update() {
        AnimatorStateInfo currentstate = enemyanimator.GetCurrentAnimatorStateInfo(0);
        double currentframe = Convert.ToDouble(currentstate.normalizedTime) - Math.Truncate(Convert.ToDouble(currentstate.normalizedTime));
        bool statecondition = (currentstate.IsTag("bang")) && (currentframe >= timingrange1 && currentframe <= timingrange2);
        //basically, is the animation on a) shooting clip? and b) on the right frame of the clip? 

        if (statecondition) // if shooting  is true
        {
			Shoot();
        }
        else if (!statecondition) // if you
        {
            DisableEffects();
        }
        
    }


    public void DisableEffects() // turns of all the lasr effect 
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
        gunAudio.Stop();
    }


    void Shoot()
    {
        gunAudio.Play();

        gunLight.enabled = true;

        gunParticles.Stop();
        gunParticles.Play();

        gunLine.enabled = true;
        gunLine.SetPosition(0,transform.position);


        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;


        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask)) // if the laser actually hits the player
        {
            PlayerHealth playerHealth = shootHit.collider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damagePerShot); // deduct player health
            }

            //gunLine.SetPosition(1, shootHit.point);
            float hitdistance = Vector3.Distance(shootRay.origin, shootHit.transform.position);
            gunLine.SetPosition(1, transform.position + transform.forward * hitdistance);

        }
        else
        {
            //gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range); 
            //gunLine.SetPosition(1, shootHit.point); // Bullet (should) stop right before the object. 
            gunLine.SetPosition(1, transform.position + transform.forward * range);
        }
    }
}