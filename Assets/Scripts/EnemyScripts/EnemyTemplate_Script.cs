using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTemplate_Script : MonoBehaviour
{
    Vector3 targetLoc;
    float speed = 1;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(gameObject.transform.position != targetLoc)
        {

            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, targetLoc, speed*Time.deltaTime);

        }

    }

    public void SetTarget(Vector3 target)
    {

        targetLoc = target;

    }

    public void SetDodgeCount(int dodgeCount)
    {
        gameObject.GetComponent<Enemy_DodgeProtocol_Script>().dodgeCount = dodgeCount;
    }
}
