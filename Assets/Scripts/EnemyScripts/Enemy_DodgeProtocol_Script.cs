using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_DodgeProtocol_Script : MonoBehaviour
{
    public int dodgeCount = 2;
    bool shouldDodge = false;
    Vector3 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldDodge)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 50 * Time.deltaTime);
            if(transform.position == targetPosition)
            {
                dodgeCount -= 1;
                if (dodgeCount > 0)
                {
                    shouldDodge = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerProjectile" && !shouldDodge)
        {
            shouldDodge = true;
            targetPosition = transform.position + new Vector3(0, 0, (Random.Range(0, 2) * 2 - 1) * 3);
            if(targetPosition.z > 16)
            {
                targetPosition = transform.position + new Vector3(0, 0, - 5);
            }
            else if(targetPosition.z < 4)
            {
                targetPosition = transform.position + new Vector3(0, 0, 5);
            }
        }
    }
}
