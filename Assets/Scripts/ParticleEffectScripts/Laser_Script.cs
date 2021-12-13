using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Script : MonoBehaviour
{
    float timer = 0;
    bool colliderFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
        timer += Time.deltaTime;
        if(timer > 0.45f && colliderFlag == false)
        {
            colliderFlag = true;
            GetComponentInChildren<Collider>().enabled = true;
        }
        if(timer > 2.0f)
        {
            Destroy(gameObject);
        }
    }
}
