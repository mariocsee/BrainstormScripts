using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
	public float range = 100f;

    protected float timer;
    protected Ray shootRay = new Ray();
    protected RaycastHit shootHit;
    protected int shootableMask;
    protected ParticleSystem gunParticles;
    protected LineRenderer gunLine;
    protected Light gunLight;
    protected float effectsDisplayTime = 0.2f;

	protected Transform player;
	protected string whichPlayerAreU;
	protected string whichSystemAreUOn;
    protected PlayerMovement playermovement;

    protected void Awake ()
    {
        shootableMask = LayerMask.GetMask ("Shootable");
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunLight = GetComponent<Light> (); 

		player = transform.parent.parent.parent.parent;
        playermovement = player.gameObject.GetComponent<PlayerMovement>();

		if (player.name == "Player2") {
			whichPlayerAreU = "P2_";
		} else if (player.name == "Player1") {
			whichPlayerAreU = "P1_";
		}

		whichSystemAreUOn = SystemInfo.operatingSystemFamily.ToString () + "_";
    }


    protected void Update () {
        timer += Time.deltaTime;
        EffectOnorOff(timer, playermovement.isMoving()); 
    }

    protected virtual void EffectOnorOff(float timer, bool isMoving) {
		string bumpername = whichSystemAreUOn + whichPlayerAreU + "Right Trigger";
		if (Input.GetAxis(bumpername) > 0.3f && timer >= timeBetweenBullets && Time.timeScale != 0 && !isMoving) {
            Shoot();
        } else if (timer >= timeBetweenBullets * effectsDisplayTime) {
            DisableEffects();
        }
    }


    public void DisableEffects () {
		gunLine.enabled = false;
		gunLight.enabled = false;
    }


    protected void Shoot () {
        timer = 0f;

        gunLight.enabled = true;

        gunParticles.Stop ();
        gunParticles.Play ();

        gunLine.enabled = true;
        gunLine.SetPosition (0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;


        if (Physics.Raycast (shootRay, out shootHit, range, shootableMask)) {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
            if(enemyHealth != null) {
                enemyHealth.TakeDamage (damagePerShot, shootHit.point);
            }
			if (shootHit.collider.CompareTag("ShootableItem")) {
				shootHit.collider.GetComponent<ShootableItem> ().TriggerShot();
			}

            gunLine.SetPosition (1, shootHit.point);
        } else {
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }
}

