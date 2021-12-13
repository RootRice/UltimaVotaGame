using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment_Movement_Script : MonoBehaviour
{
    public float resetThreshold;
    public float resetQuantity;
    public float speed;
    public GameObject partner;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position -= new Vector3(speed * Time.deltaTime, 0f, 0f);
        if(gameObject.transform.position.x < resetThreshold)
        {
            gameObject.transform.position = partner.transform.position + new Vector3(resetQuantity, 0f, 0f);
            gameObject.transform.position -= new Vector3(speed * Time.deltaTime, 0f, 0f);
        }
    }
}
