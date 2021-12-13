using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Minieye_Script : MonoBehaviour
{
    bool active = false;
    public GameObject projectile;
    GameObject mainCharacter;
    float bulletTimer;
    int bulletCounter;

    // Start is called before the first frame update
    void Start()
    {
        mainCharacter = GameObject.FindGameObjectWithTag("Main Character");
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            fire();
        }
    }

    void fire()
    {
        bulletTimer += Time.deltaTime;
        if (bulletTimer > 0.4f)
        {
            bulletTimer = 0;
            bulletCounter += 1;
            Vector3 spawnLoc = new Vector3(gameObject.transform.position.x, 1, gameObject.transform.position.z);
            GameObject bullet = Instantiate(projectile, spawnLoc, Quaternion.Euler(90.0f, 0.0f, 0.0f));
            bullet.GetComponent<Enemy_Projectile_Script>().target = mainCharacter.transform.position + new Vector3(0.68f, 0, 0f) ;
            if (bulletCounter > 2)
            {
                bulletCounter = 0;
                active = false;
            }
        }
    }

    public void Activate()
    {
        active = true;
    }
}
