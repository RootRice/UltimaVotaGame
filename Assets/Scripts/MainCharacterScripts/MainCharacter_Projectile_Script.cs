using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter_Projectile_Script : MonoBehaviour
{

    public float movementspeed = 50.0f;
    public float damage;

    Animator myAnimator;


    private void Start()
    {
        myAnimator = GetComponent<Animator>();
        myAnimator.Play("BulletAnim", -1, Random.Range(0.0f, 1.0f));
    }
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * movementspeed);
        Destroy(this.gameObject, 5f);

    }
}
