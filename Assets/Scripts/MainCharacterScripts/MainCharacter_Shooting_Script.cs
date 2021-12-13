using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter_Shooting_Script : MonoBehaviour
{
    public GameObject projectile;
    public float shottimer = 0.2f;
    public bool shotready = true;

    void Update()
    {
        if(Input.GetButton("Fire1") && shotready == true)
        {
            Instantiate(projectile, transform.position + new Vector3(1.5f,0,0), Quaternion.Euler(90.0f, 0.0f, 0.0f)); //Changed it so projectiles don't fly down/up when character tilts backwards/forwards, instead always spawn with neutral rotation
            shotready = false;
        }

        if(shotready == false)
        {
            shottimer -= 1*Time.deltaTime; //Shot timer now decreases based on time rather than clock cycles
        }

        if (shottimer <= 0.0f)
        {
            shotready = true;
            shottimer = 0.2f;
        }
    }
}
