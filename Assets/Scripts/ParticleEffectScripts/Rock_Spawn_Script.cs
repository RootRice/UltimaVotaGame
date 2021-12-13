using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock_Spawn_Script : MonoBehaviour
{

    public GameObject rock;
    public float timer = 0.0f;
    public int quantity = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Vector3 spawnPos = new Vector3(Random.Range(60f, 77f), 0, Random.Range(-3f, 0f));
        if (spawnPos.z > 0.3f && spawnPos.z < 20f)
        {
            spawnPos.y = -5.5f;
        }
        else
        {
            spawnPos.y = Random.Range(-1.75f, -0.4f);
        }
        if (timer > 0.25f)
        {
            for(int i = 0; i < quantity; i++)
            {
                GameObject.Instantiate(rock, spawnPos, Random.rotation);
                spawnPos = new Vector3(Random.Range(60f, 77f), 0, Random.Range(-3f, 3f));
                if (spawnPos.z > 0.3f)
                {
                    spawnPos.y = -5.5f;
                }
                else
                {
                    spawnPos.y = Random.Range(-1.75f, -0.4f);
                }
            }
            spawnPos = new Vector3(Random.Range(60f, 77f), 0, Random.Range(23f, 17f));
            if (spawnPos.z < 20f)
            {
                spawnPos.y = -5.5f;
            }
            else
            {
                spawnPos.y = Random.Range(-1.75f, -0.4f);
            }
            for (int i = 0; i < quantity; i++)
            {
                GameObject.Instantiate(rock, spawnPos, Random.rotation);
                spawnPos = new Vector3(Random.Range(60f, 77f), 0, Random.Range(23f, 17f));
                if (spawnPos.z < 20f)
                {
                    spawnPos.y = -5.5f;
                }
                else
                {
                    spawnPos.y = Random.Range(-1.75f, -0.4f);
                }
            }
            timer = 0f;
            
        }
    }
}
