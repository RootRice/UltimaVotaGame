using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornLoss_Script : MonoBehaviour
{
    Rigidbody myRigidbody;
    public bool isDead = false;

    float scalar = 30f;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead)
        {
            gameObject.transform.localScale -= new Vector3(scalar*Time.deltaTime, scalar * Time.deltaTime, scalar * Time.deltaTime);
            gameObject.transform.position -= new Vector3(5 * Time.deltaTime, 0f, 0f);
            gameObject.transform.Rotate(new Vector3(100 * Time.deltaTime, 25 * Time.deltaTime, 75 * Time.deltaTime));
            if(gameObject.transform.localScale.x < 0)
            {
                Destroy(gameObject);
            }

            
        }
        
    }

    public void Delete()
    {
        isDead = true;
        myRigidbody.isKinematic = false;
        myRigidbody.AddForce(new Vector3(10f, 0, 0));
    }
}
