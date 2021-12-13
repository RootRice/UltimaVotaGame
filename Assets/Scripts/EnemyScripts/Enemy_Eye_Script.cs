using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Eye_Script : MonoBehaviour
{
    GameObject myPlayer;
    [SerializeField] float offset = 0;
    // Start is called before the first frame update
    void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Main Character");
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(myPlayer.transform.position + new Vector3(0f, 30f -offset, 0f) - gameObject.transform.position);
    }
}
