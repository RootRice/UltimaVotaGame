using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Boss_Script : MonoBehaviour
{
    GameObject mainCharacter;
    [SerializeField] GameObject impactAnim;
    Boss_EyeManager_Script tailEyes;
    Boss_Helper_Script[] sideEyes;
    Boss_Maze_Script mazePattern;
    Boss_Spread_Script spreadPattern;

    int pattern;
    bool flag = false;
    int health = 200;
    float timer = -10;

   public AudioSource source;
   public AudioClip damageSFX;
    bool dmg1 = false;
    bool dmg2 = false;
    bool dmg3 = false;

    GameObject SpawnScript;
    // Start is called before the first frame update
    void Start()
    {
        pattern = Random.Range(0, 3);
        mainCharacter = GameObject.FindGameObjectWithTag("Main Character");
        tailEyes = FindObjectOfType<Boss_EyeManager_Script>();
        sideEyes = GetComponentsInChildren<Boss_Helper_Script>();
        mazePattern = GetComponent<Boss_Maze_Script>();
        spreadPattern = GetComponent<Boss_Spread_Script>();
        SpawnScript = GameObject.FindGameObjectWithTag("Spawner");
    }

    // Update is called once per frame
    void Update()
    {
        if(health < 0)
        {
            SpawnScript.GetComponent<SpawnManager_Script>().killcount++;
            //Victory here?
            gameObject.SetActive(false);
        }
        timer += Time.deltaTime;
        if(timer > 0f)
        {
            if(pattern == 0)
            {
                if (!flag)
                {
                    flag = true;
                    sideEyes[0].SetActive(false, 10f);
                    sideEyes[1].SetActive(false, 10f);
                    mazePattern.active = true;
                }
                if (timer > 11f)
                {
                    flag = false;
                    timer = -6f;
                    mazePattern.active = false;
                    pattern = Random.Range(0, 3);
                }
            }
            else if(pattern == 1)
            {
                if (!flag)
                {
                    flag = true;
                    sideEyes[0].SetActive(true, 10f);
                    sideEyes[1].SetActive(true, 10f);
                    tailEyes.active = true;
                }
                if (timer > 10f)
                {
                    flag = false;
                    timer = -2f;
                    tailEyes.active = false;
                    pattern = Random.Range(0, 3);
                }
            }
            else
            {
                if (!flag)
                {
                    flag = true;
                    spreadPattern.active = true;
                }
                if (timer > 10f)
                {
                    flag = false;
                    timer = -2f;
                    spreadPattern.active = false;
                    pattern = Random.Range(0, 3);
                }
            }
        }

        if (health == 150)
        {
            if (dmg1 == false)
            {
                source.PlayOneShot(damageSFX, 0.6f);
                dmg1 = true;
            }
        }

        if (health == 100)
        {
            if (dmg2 == false)
            {
                source.PlayOneShot(damageSFX, 0.6f);
                dmg2 = true;
            }
        }

        if (health == 50)
        {
            if (dmg3 == false)
            {
                source.PlayOneShot(damageSFX, 0.6f);
                dmg3 = true;
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {

        if (collision.tag == "PlayerProjectile")
        {
            GameObject impact = Instantiate(impactAnim, new Vector3(gameObject.transform.position.x - 0.3f, collision.transform.position.y, collision.transform.position.z), Quaternion.Euler(90f, 0f, 0f));
            impact.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            health -= 1;
            Destroy(collision.gameObject);
        }

        if (collision.tag == "PlayerLaser")
        {
            Instantiate(impactAnim, new Vector3(gameObject.transform.position.x - 0.3f, 2f, collision.transform.position.z), Quaternion.Euler(90f, 0f, 0f));
            source.PlayOneShot(damageSFX, 0.7f);
            health -= 3;
        }
    }


}
