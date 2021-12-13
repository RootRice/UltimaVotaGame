using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Projectile_Script : MonoBehaviour
{
    public Vector3 target;
    public Vector3 movementVector;
    public float movementSpeed = 10.0f;

    public GameObject dropParticle;

    float timer = 0.0f;
    float prevAngle = 0f;
    public float destructTimer = 5f;
    public bool particlesOn = true;

    Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myAnimator.Play("SlimeAnimation", 0, Random.Range(0.0f, 1.0f));
        movementVector = (target - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(movementVector);
        transform.rotation = Quaternion.Euler(new Vector3(90.0f, transform.rotation.eulerAngles.y-90.0f, transform.rotation.eulerAngles.z));
        movementVector *= movementSpeed;
        Destroy(this.gameObject, destructTimer);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 0.133f)
        {
            float movementAngle = prevAngle - Random.Range(-25f, 25f);
            prevAngle = movementAngle - 90f;
            Vector3 dropVector = new Vector3(Mathf.Cos(Mathf.Deg2Rad * movementAngle), 0f, Mathf.Sin(Mathf.Deg2Rad * movementAngle)) * 0.37f;
            dropVector += gameObject.transform.position + new Vector3(0f, Random.Range(-0.1f, 0.1f));
            //Vector3 movementVector = Vector3.
            if(particlesOn)
            {
                Instantiate(dropParticle, dropVector, Quaternion.Euler(new Vector3(90f, 0f, movementAngle - 180 + Random.Range(-25f, 25f))), gameObject.transform);
            }
            //Instantiate(dropParticle, dropVector, Quaternion.Euler(new Vector3(90f, 0f, movementAngle - 90 + Random.Range(-25f,25f))), gameObject.transform);
            timer = 0f;
        }
        transform.position += movementVector*Time.deltaTime;
    }
}
