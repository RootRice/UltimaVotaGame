using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Maze_Script : MonoBehaviour
{
    float attackTimer = 0.0f;
    float intervalTimer = 0.0f;

    float spreadDensity = 20;

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
            Mazeattack();
        }
    }

    public bool Mazeattack()
    {
        attackTimer += Time.deltaTime;
        intervalTimer += Time.deltaTime;

        if (intervalTimer > 0.65f)
        {
            float stepAngle = 90 / spreadDensity;
            float angle = startAngle;
            float targetPosZ = startZ;
            float stepPosZ = 26f / spreadDensity;

            float randomOpening = Random.Range(startAngle+15, startAngle +70);

            for (int i = 0; i <= spreadDensity; i++)
            {

                if (shotsfxComplete == false)
                {
                    source.PlayOneShot(shotSFX, 0.6f);
                    shotsfxComplete = true;
                }
                angle = 225 + stepAngle * i;
                targetPosZ = stepPosZ * i;
                if(angle < randomOpening- 6 || angle > randomOpening+ 6)
                {
                    Vector3 spawnLoc = gameObject.transform.position + new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle), 0f, Mathf.Cos(Mathf.Deg2Rad * angle)) * 5f;
                    spawnLoc = new Vector3(spawnLoc.x, 1, spawnLoc.z);
                    GameObject bullet = Instantiate(projectile, spawnLoc, Quaternion.Euler(0f, 0f, 0f));
                    bullet.GetComponent<Enemy_Projectile_Script>().target = new Vector3(0f, 1f, targetPosZ);
                    bullet.GetComponent<Enemy_Projectile_Script>().movementSpeed = 5.0f;
                    bullet.GetComponent<Enemy_Projectile_Script>().destructTimer = 10.0f;
                    bullet.GetComponent<Enemy_Projectile_Script>().particlesOn = false;
                }

                

            }

            intervalTimer = 0f;
        }
        shotsfxComplete = false;
        return false;
    }
}
