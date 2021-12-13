using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock_Movement_Script : MonoBehaviour
{
    Vector3 speed;
    void Start()
    {
        
        if(transform.position.y < -4)
        {
            //float zMov = 0;
            //if(transform.position.z > 10)
            //{
            //    zMov = Random.Range(-1f, -2f);
            //}
            //else
            //{
            //    zMov = Random.Range(1f, 2f);
            //}
            //speed = new Vector3(Random.Range(-30f, -35f), Random.Range(-2f, -5f),zMov);
            //Destroy(gameObject, 4f);
        }
        else
        {
            speed = Vector3.left * 70f;
            Destroy(gameObject, 2f);
        }
        transform.localScale = Vector3.one * Random.Range(0.75f, 1.25f);
    }
    
    void Update()
    {
        transform.position += speed*Time.deltaTime;
    }
}
