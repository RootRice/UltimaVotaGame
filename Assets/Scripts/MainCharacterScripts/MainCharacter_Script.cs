using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter_Script : MonoBehaviour
{
    public AudioSource Source;
    public AudioClip ShotSFX;
    public AudioClip ZenSFX;
    public AudioClip RageSFX;
    public AudioClip DamageSFX;
    public AudioClip DeathSFX;
    public AudioClip DodgeSFX;

    public GameObject projectile;
    public float shottimer = 0.2f;
    public bool shotready = true;

    public GameObject rotationReference;
    public GameObject rotationReferenceX;
    public GameObject rotationReferenceZ;
    public GameObject[] tail;
    public GameObject[] horns;
    Vector3[] tailOrigins = new Vector3[6];
    public float tailRotModifier = 0.5f;
    public float tailFlex = 1.0f;
    private Quaternion tailTargetPos;

    public GameObject pelvis;

    public Animator myAnimator;
    private float wingTimer = 0f;

    private float speed = 5f;
    private float rotationSpeed = 150f;

    private bool dodging = false;
    private float dodgeLength = 7;
    private float elapsedDodgeTime = 0;
    private float dodgeDuration = 0.3f;
    private float dodgeSpeed = 30;
    private Vector3 dodgeTarget;
    private Vector3 dodgeOrigin;

    bool vulnerable = true;
    float invulnTimer;


    public int zenCount = 0;
    public float zenTimer = 10;
    bool zenActive = false;
    public int rageCount = 0;
    public float rageTimer = 10;
    bool rageActive = false;
    public GameObject playerLaser;

    public int health = 3;
    bool isDead = false;
    SkinnedMeshRenderer renderer;
    
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponentInChildren<SkinnedMeshRenderer>();
        myAnimator = GetComponentInChildren<Animator>();
        for(int i = 0; i < 6; i++)
        {
            tailOrigins[i] = tail[i + 1].transform.localRotation.eulerAngles;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (dodging)
            {
                ApplyDodge();
            }
            else
            {
                ApplyMovement();
                shoot();
                powerups();
            }

            if (!vulnerable)
            {
                invulnTimer += Time.deltaTime;
                if (invulnTimer > 0.3f)
                {
                    invulnTimer = 0;
                    vulnerable = true;
                }
            }
        }
        else
        {
            transform.localScale -= new Vector3(1 * Time.deltaTime, 1 * Time.deltaTime, 1 * Time.deltaTime);
            transform.position -= new Vector3(0f, 0.5f * Time.deltaTime, 0f);
            if (transform.localScale.x < 0f)
            {
                health = -3;
            }
        }

    }

    private void ApplyMovement()
    {
        wingTimer += Time.deltaTime;
        Vector3 translation = new Vector3(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, 0, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime);
        Vector3 rotation = new Vector3(Input.GetAxisRaw("Vertical") * Time.deltaTime * rotationSpeed, 0, Input.GetAxisRaw("Horizontal") * Time.deltaTime * -rotationSpeed);
        if (translation.x + transform.position.x > 0 && translation.x + transform.position.x < 37.0f)
        {

            gameObject.transform.position += new Vector3(translation.x, 0, 0);

            if (transform.rotation.eulerAngles.z + rotation.z < 5.0f || transform.rotation.eulerAngles.z + rotation.z > 350.0f)
            {

                transform.Rotate(new Vector3(0.0f, 0f, rotation.z));

            }


        }
        if (translation.z + transform.position.z < 16.5f && translation.z + transform.position.z > 3.5f)
        {

            if (transform.rotation.eulerAngles.x + rotation.x < 25.0f || transform.rotation.eulerAngles.x + rotation.x > 335.0f)
            {

                transform.Rotate(new Vector3(rotation.x, 0f, 0f));

            }




            gameObject.transform.position += new Vector3(0, 0, translation.z);


        }

        if (rotation.z == 0f && transform.rotation.eulerAngles.z != 0)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationReferenceZ.transform.rotation, rotationSpeed / 2 * Time.deltaTime);
        }
        if (rotation.x == 0f && transform.rotation.eulerAngles.x != 0)
        {

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationReferenceX.transform.rotation, rotationSpeed / 2 * Time.deltaTime);
        }
        if (transform.rotation.eulerAngles == Vector3.zero && wingTimer > 5f)
        {
            wingTimer = -5f;
            myAnimator.Play("Wingflap");
        }
        if (Input.GetButtonDown("Dodge") && dodging == false)
        {
            Source.PlayOneShot(DodgeSFX);
        }
        if (Input.GetButtonDown("Dodge"))
        {
            //myAnimator.SetBool("Dashing", true);
            myAnimator.Play("Dash 1");
            dodgeOrigin = gameObject.transform.position;
            dodgeTarget = gameObject.transform.position + new Vector3(dodgeLength * Input.GetAxisRaw("Horizontal"), 0, dodgeLength * Input.GetAxisRaw("Vertical"));
            dodging = true;
        }


        RotateTail(transform.eulerAngles.x);


    }

    private void RotateTail(float dragonRotation)
    {
        float rotationFactor = 0f;
        float angleInformer = 0f;
        if (dragonRotation > 180f)
        {
            rotationFactor = -(360f -dragonRotation)*tailRotModifier;
            angleInformer = 1;
        }
        else
        {
            rotationFactor = dragonRotation*tailRotModifier;
            angleInformer = -1;
        }
        

        //for(int i = 1; i < 7; i++)
        //{
        //    Vector3 adjustedPos = new Vector3(Mathf.Sin(Mathf.Deg2Rad * (270 - (rotationFactor * i*tailFlex))), 0f, Mathf.Cos(Mathf.Deg2Rad * (270 - (rotationFactor * i * tailFlex)))) * scalarTailPos[i-1];
        //    tail[i].transform.localPosition = new Vector3(adjustedPos.x, 0f, adjustedPos.z);
        //    tail[i].transform.position = new Vector3(tail[i].transform.position.x, 0f, tail[i].transform.position.z);
        //    Vector3 direction = tail[i].transform.position - tail[i - 1].transform.position;
        //    tail[i].transform.Rotate(new Vector3(tail[i].transform.rotation.x, Vector3.Angle(new Vector3(1f, 0f, 0f), direction), tail[i].transform.rotation.z));
        //}
        for (int i = 1; i < 7; i++)
        {
            Vector3 adjustedPos = new Vector3(Mathf.Sin(Mathf.Deg2Rad * (90 - (rotationFactor * i * tailFlex))), 0f, Mathf.Cos(Mathf.Deg2Rad * (90 - (rotationFactor * i * tailFlex))));
            float yAngle = Vector3.Angle(new Vector3(1f, 0f, 0f), adjustedPos);
            tail[i].transform.localRotation = Quaternion.Euler(tailOrigins[i-1].x, yAngle*angleInformer, tailOrigins[i - 1].z);
        }
    }

    private void ApplyDodge()
    {
        RecalculateDodge();
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, dodgeTarget, dodgeSpeed * Time.deltaTime);
        if (gameObject.transform.position.x < 0)
        {
            gameObject.transform.position = new Vector3(0f, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        else if (gameObject.transform.position.x > 37)
        {
            gameObject.transform.position = new Vector3(37f, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        if (gameObject.transform.position.z < 3.5f)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 3.5f);
        }
        else if (gameObject.transform.position.z > 16.5f)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 16.5f);
        }

        elapsedDodgeTime += Time.deltaTime;
        if (elapsedDodgeTime > 0.3f)
        {
            myAnimator.SetBool("Dashing", false);
            myAnimator.SetBool("ShouldIdle", true);
            //myAnimator.Play("Idle");
            elapsedDodgeTime = 0;
            dodging = false;
        }

    }

    private void RecalculateDodge()
    {
        Vector3 translation = new Vector3(Input.GetAxisRaw("Horizontal") * dodgeSpeed * Time.deltaTime, 0, Input.GetAxisRaw("Vertical") * dodgeSpeed * Time.deltaTime);
        float angle = Vector3.Angle(dodgeTarget - dodgeOrigin, translation);
        if (angle < 135)
        {
            if (translation.x + dodgeTarget.x > 0 && translation.x + dodgeTarget.x < 24.0f)
            {
                dodgeTarget.x += translation.x;
            }
            if (translation.z + dodgeTarget.z < 16.5f && translation.z + dodgeTarget.z > 3.5f)
            {
                dodgeTarget.z += translation.z;
            }
        }
        else
        {
            Debug.Log(Vector3.Angle(dodgeTarget - dodgeOrigin, translation));
        }



    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "EnemyProjectile")
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
            if (dodging == true)
            {
                zenCount++;
            }
        }
        else if (collision.tag == "EnemyLaser")
        {
            TakeDamage(2);
        }

        
    }

    public void TakeDamage(int damageTaken)
    {

        if (vulnerable && !dodging)
        {
            vulnerable = false;
            health -= damageTaken;
            Source.PlayOneShot(DamageSFX, 0.8f);
            if (health < 1)
            {
                isDead = true;
                myAnimator.Play("Death");
                Source.PlayOneShot(DeathSFX);
            }
            else
            {
                myAnimator.Play("Hit");
                if (damageTaken < 2)
                {
                    horns[health - 1].transform.parent = null;
                    horns[health - 1].GetComponent<HornLoss_Script>().Delete();
                }
                else
                {
                    horns[health - 1].transform.parent = null;
                    horns[health - 1].GetComponent<HornLoss_Script>().Delete();
                    horns[health].transform.parent = null;
                    horns[health].GetComponent<HornLoss_Script>().Delete();
                }
            }
        }
    }

    private void shoot()
    {
        if (Input.GetButton("Fire1") && shotready == true)
        {
            Instantiate(projectile, transform.position + new Vector3(1.5f, 0, 0), Quaternion.Euler(90.0f, 0.0f, 0.0f)); //Changed it so projectiles don't fly down/up when character tilts backwards/forwards, instead always spawn with neutral rotation
            shotready = false;
            myAnimator.Play("Shoot", 1, 0f);
            Source.PlayOneShot(ShotSFX, 0.3f);
        }

        if (shotready == false)
        {
            shottimer -= 1 * Time.deltaTime; //Shot timer now decreases based on time rather than clock cycles

        }

        if (shottimer <= 0.0f)
        {
            shotready = true;
            shottimer = 0.2f;
        }
    }

    private void powerups()
    {
        renderer.material.SetFloat("_RWing", zenCount/5f);
        renderer.material.SetFloat("_LWing", rageCount / 3f);
        if (zenCount >= 5)
        {
            if (Input.GetButton("Powerup1"))
            {
                zenActive = true;
                renderer.material.SetFloat("_ShieldActive", 1f);

                zenCount = 0;
            }
        }
        if (rageCount >= 3)
        {
            if (Input.GetButton("Powerup2"))
            {
                rageActive = true;

                rageCount = 0;
            }
        }

        if (zenActive == true && zenTimer == 10)
        {
            Source.PlayOneShot(ZenSFX, 0.5f);
        }
        if (zenActive == true)
        {
            vulnerable = false;
            zenTimer -= Time.deltaTime;
        }


        if (zenTimer <= 0)
        {
            zenTimer = 10;
            vulnerable = true;
            zenActive = false;
            renderer.material.SetFloat("_ShieldActive", 0f);
        }

        if (rageActive == true)
        {
            Instantiate(playerLaser, gameObject.transform.position + new Vector3(1.5f,0f,0f), Quaternion.Euler(new Vector3(0f, 0f, 0f)), gameObject.transform);
            Source.PlayOneShot(RageSFX, 0.6f);
            rageActive = false;
        }
    

    }

}
