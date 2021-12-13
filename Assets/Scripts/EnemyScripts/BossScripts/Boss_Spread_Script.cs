using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Boss_Spread_Script : MonoBehaviour
{
    float attackTimer = 0.0f;
    float intervalTimer = 0.0f;

    float spreadDensity = 5;

    float startAngle = 225f;
    float startZ = 3.5f;

    public GameObject projectile;
    public bool active = false;

    public AudioSource source;
    public AudioClip shotSFX;
    bool shotsfxComplete = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            Spreadattack();
        }
    }

    public bool Spreadattack()
    {

        attackTimer += Time.deltaTime;
        intervalTimer += Time.deltaTime;

        if(intervalTimer > 1.0f)
        {


            float stepAngle = 90 / spreadDensity;
            float angle = startAngle;

            float targetPosZ = startZ;
            float stepPosZ = 13f / spreadDensity;
            
            for (int i = 0; i <= spreadDensity; i++)
            {
                if (shotsfxComplete == false)
                {
                    source.PlayOneShot(shotSFX, 0.6f);
                    shotsfxComplete = true;
                }

                angle = 225 + stepAngle * i;
                targetPosZ = 3.5f + stepPosZ * i;
                Vector3 spawnLoc = gameObject.transform.position + new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle), 0f, Mathf.Cos(Mathf.Deg2Rad * angle)) * 3f;
                spawnLoc = new Vector3(spawnLoc.x, 1, spawnLoc.z);
                GameObject bullet = Instantiate(projectile, spawnLoc, Quaternion.Euler(0f, 0f, 0f));
                bullet.GetComponent<Enemy_Projectile_Script>().target = new Vector3(0f, 1f, targetPosZ);

            }
            if(startZ == 3.5f)
            {
                startZ += 0.75f;
                startAngle += 15f;
                spreadDensity = 4f;
            }
            else
            {
                startZ -= 0.75f;
                startAngle -= 15f;
                spreadDensity = 5f;
            }
            intervalTimer = 0f;
        }
        shotsfxComplete = false;
        return false;
    }
}
