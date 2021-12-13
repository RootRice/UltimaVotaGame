using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_3_AI_Script : MonoBehaviour
{

    public AudioSource Source;
    public AudioClip ShotSFX;
    public AudioClip DamageSFX;
    public AudioClip DeathSFX;

    Vector3 targetPosition;
    float defensiveOffset = 0.0f;
    float speed = 2.0f;

    float attackTimer = 0.0f;
    float rechargeTimer = 0.0f;
    bool attacking = false;
    bool firing = false;
    float angle;
    Vector3 laserDir;

    float vulnTimer = 0.0f;
    bool vulnerable = false;

    GameObject myPlayer;
    GameObject SpawnScript;
    public GameObject laser;
    public GameObject impactAnim;

    SkinnedMeshRenderer []enemyRenderer;

    MeshRenderer eyeRenderer;
    

    int health = 12;
    // Start is called before the first frame update
    void Start()
    {
        eyeRenderer = GetComponentInChildren<MeshRenderer>();
        SpawnScript = GameObject.FindGameObjectWithTag("Spawner");
        myPlayer = GameObject.FindGameObjectWithTag("Main Character");
        targetPosition = new Vector3(defensiveOffset + gameObject.transform.position.x, gameObject.transform.position.y, myPlayer.transform.position.z);
        enemyRenderer = GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health < 1)
        {
            SpawnScript.GetComponent<SpawnManager_Script>().killcount++;
            myPlayer.GetComponent<MainCharacter_Script>().rageCount++;
            Source.PlayOneShot(DeathSFX);
            Destroy(gameObject);
        }
        if(vulnerable)
        {
            vulnTimer += Time.deltaTime;
            if(vulnTimer > 2.75f)
            {
                vulnTimer = 0.0f;
                vulnerable = false;
                defensiveOffset = 0.0f;
                foreach(SkinnedMeshRenderer renderer in enemyRenderer)
                {
                    renderer.material.SetFloat("_ShieldActive", 1f);
                }
            }
        }
        if(!attacking)
        {
            Move();
            rechargeTimer += Time.deltaTime;

            if(rechargeTimer > 10f)
            {
                attacking = true;
                vulnerable = true;
                foreach (SkinnedMeshRenderer renderer in enemyRenderer)
                {
                    renderer.material.SetFloat("_ShieldActive", 0f);
                }
                laserDir = Vector3.Normalize(gameObject.transform.position - myPlayer.transform.position + new Vector3(0.68f, 0, 0f));
                laserDir = new Vector3(laserDir.x, 0f, laserDir.z)*5f;
                angle = Vector3.Angle(gameObject.transform.position - myPlayer.transform.position, new Vector3(0f, 0f, 1f));
                if(gameObject.transform.position.x < myPlayer.transform.position.x)
                {
                    angle = 360 - angle;
                }
            }
        }
        else
        {
            Attack();
        }
    }
    void Attack()
    {
        
        attackTimer += Time.deltaTime;
        if (attackTimer > 0.8f && !firing)
        {
            Instantiate(laser, gameObject.transform.position, Quaternion.Euler(new Vector3(0f, angle, 0f)), gameObject.transform);
            Source.PlayOneShot(ShotSFX);
            firing = true;
        }
        else if(attackTimer > 1.4f)
        {
            firing = false;
            attacking = false;
            rechargeTimer = 0f;
            attackTimer = 0.0f;
            defensiveOffset = 3.0f;
            eyeRenderer.material.SetColor("_Color", new Color(1, Mathf.Lerp(1f, 0f, attackTimer), Mathf.Lerp(1f, 0f, attackTimer), 1f));
        }
        else if(firing)
        {
            eyeRenderer.material.SetColor("_Color", Color.Lerp(Color.magenta, Color.white, attackTimer -0.6f));
            transform.position += laserDir * Time.deltaTime;
            if (gameObject.transform.position.x > 39f)
            {
                gameObject.transform.position = new Vector3(39f, gameObject.transform.position.y, gameObject.transform.position.z);
            }
            if (gameObject.transform.position.z < 3.5f)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 3.5f);
            }
            else if(gameObject.transform.position.z > 16.5f)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 16.5f);
            }
        }
        else
        {
            eyeRenderer.material.SetColor("_Color", Color.Lerp(Color.white, Color.magenta, attackTimer));
        }
    }

    void Move()
    {
        targetPosition = new Vector3(defensiveOffset + transform.parent.position.x, gameObject.transform.position.y, myPlayer.transform.position.z);
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPosition, speed * Time.deltaTime);
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        
        if (collision.tag == "PlayerProjectile")
        {
            Destroy(collision.gameObject);
            GameObject impact = Instantiate(impactAnim, new Vector3(gameObject.transform.position.x - 0.3f, collision.transform.position.y, collision.transform.position.z), Quaternion.Euler(90f, 0f, 0f));
            impact.transform.localScale = new Vector3(1.75f, 1.75f, 1.75f);
            if (vulnerable)
            {
                health -= 1;
                Source.PlayOneShot(DamageSFX);
            }
            
        }
        if (collision.tag == "PlayerLaser")
        {
            Instantiate(impactAnim, new Vector3(gameObject.transform.position.x - 0.3f, 2f, collision.transform.position.z), Quaternion.Euler(90f, 0f, 0f));
            health -= 3;
            Source.PlayOneShot(DamageSFX);
        }

    }
}
