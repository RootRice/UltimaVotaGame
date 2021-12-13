using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impact_Script : MonoBehaviour
{
    float destructionTimer = 0f;
    float speed = 1f;
    float scalar = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        destructionTimer += Time.deltaTime;
        transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
        if(destructionTimer > 0.45f)
        {
            Destroy(gameObject);
        }
    }
}
