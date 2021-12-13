using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationReferenceX_Script : MonoBehaviour
{

    public GameObject myMainCharacter;
    public bool XorZ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(XorZ)
        {
            transform.eulerAngles = new Vector3(0f, 0f, myMainCharacter.transform.rotation.eulerAngles.z);
        }
        else
        {
            transform.eulerAngles = new Vector3(myMainCharacter.transform.rotation.eulerAngles.x, 0f, 0f);
        }

    }
}
