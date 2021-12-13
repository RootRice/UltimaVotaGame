using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_Wake_Particle_Script : MonoBehaviour
{
    [SerializeField] Transform track;
    [SerializeField] Vector3 offset;
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(track.position.x, 0f, track.position.z) + offset;
    }
}
