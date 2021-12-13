using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSpawnManager : MonoBehaviour
{
    GameObject Spawnscript;

    // Start is called before the first frame update
    void Start()
    {
        Spawnscript = GameObject.FindGameObjectWithTag("Spawner");

        Spawnscript.GetComponent<SpawnManager_Script>().isTutorial = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
