using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop_Particle_Script : MonoBehaviour
{
    GameObject myParent;
    Vector3 dragVelocity;
    Vector3 expulsionVelocity;
    float scalarOverTime;
    float timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        scalarOverTime = Random.Range(0.4f, 0.6f);
        myParent = gameObject.transform.parent.gameObject;
        dragVelocity = -Vector3.Normalize(myParent.GetComponent<Enemy_Projectile_Script>().movementVector);
        expulsionVelocity = new Vector3(Mathf.Cos(transform.localRotation.eulerAngles.z * Mathf.Deg2Rad), 0f, Mathf.Sin(transform.localRotation.eulerAngles.z * Mathf.Deg2Rad)) * 3f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        gameObject.transform.position += (dragVelocity * scalarOverTime * Time.deltaTime) + (expulsionVelocity *(1f - scalarOverTime) * Time.deltaTime);
        scalarOverTime += Time.deltaTime;
        if(scalarOverTime > 1f)
        {
            scalarOverTime = 1f;
        }
        if(timer > 1.5f)
        {
            Destroy(gameObject);
        }
    }
}
