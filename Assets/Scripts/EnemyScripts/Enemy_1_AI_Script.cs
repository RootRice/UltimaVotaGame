using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1_AI_Script : MonoBehaviour
{
    public AudioSource Source;
    public AudioClip ShotSFX;
    public AudioClip DamageSFX;
    public AudioClip DeathSFX;

    public GameObject projectile;
    GameObject mainCharacter;
    GameObject SpawnScript;
    float bulletTimer;
    float rechargeTime;
    int bulletCounter;

    public GameObject impactAnim;

    int health = 3;
    // Start is called before the first frame update
    void Start()
    {
        mainCharacter = GameObject.FindGameObjectWithTag("Main Character");
        SpawnScript = GameObject.FindGameObjectWithTag("Spawner");
        bulletTimer = Random.Range(-1f, 1f);

    }

    // Update is called once per frame
    void Update()
    {
        if(health < 1)
        {
            mainCharacter.GetComponent<MainCharacter_Script>().rageCount++;
            SpawnScript.GetComponent<SpawnManager_Script>().killcount++;
            Source.PlayOneShot(DeathSFX);
            Destroy(gameObject.transform.parent.gameObject);
        }
        if (rechargeTime > 1.5f)
        {
            bulletTimer += Time.deltaTime;
            if (bulletTimer > 0.4f)
            {
                bulletTimer = 0;
                bulletCounter += 1;
                Vector3 target = new Vector3(mainCharacter.transform.position.x, 0.0f, mainCharacter.transform.position.z) - gameObject.transform.position;
                GameObject bullet = Instantiate(projectile, gameObject.transform.position, Quaternion.Euler(90.0f, 0.0f, 0.0f));
                Source.PlayOneShot(ShotSFX, 0.3f);
                bullet.GetComponent<Enemy_Projectile_Script>().target = mainCharacter.transform.position + new Vector3(0.68f, 0, 0f);
                if(bulletCounter > 2)
                {
                    bulletCounter = 0;
                    rechargeTime = Random.Range(-0.1f, 0.5f);
                }
            }
        }
        else
        {
            rechargeTime += Time.deltaTime;
        }
        transform.localPosition = new Vector3(0, 0, Mathf.Sin(Time.timeSinceLevelLoad));
    }

    private void OnTriggerEnter(Collider collision)
    {
        
        if(collision.tag == "PlayerProjectile")
        {
            GameObject impact = Instantiate(impactAnim, new Vector3(gameObject.transform.position.x - 0.3f, collision.transform.position.y, collision.transform.position.z), Quaternion.Euler(90f, 0f, 0f));
            impact.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            Source.PlayOneShot(DamageSFX, 0.8f);
            health -= 1;
            Destroy(collision.gameObject);
        }

        if (collision.tag == "PlayerLaser")
        {
            Instantiate(impactAnim, new Vector3(gameObject.transform.position.x - 0.3f, 2f, collision.transform.position.z), Quaternion.Euler(90f, 0f, 0f));
            Source.PlayOneShot(DamageSFX, 0.8f);
            health -= 3;
        }
    }
}
