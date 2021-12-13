using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SpawnManager_Script : MonoBehaviour
{

    public AudioSource source;
    public AudioClip bossSFX;

    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;
    public GameObject boss;


    public bool isTutorial = false;

    public GameObject Txt1;
    float Text1counter = 0;
    float Text1timer = 10;
    public GameObject Txt2;
    public GameObject Txt3;
    public GameObject Txt4;
    public GameObject player;

    public int difficulty;
    public float timer;
    //public float secondcounter = 1;
 //   public float wavetimer = 0;
    public float killcount = 0;
    public float killsNeeded = 1;

    public float currentWave = 0;

    bool wave1complete = false;

    bool wave2complete = false;

    bool wave3complete = false;

    bool wave4complete = false;

    bool wave5complete = false;

    bool wave6complete = false;
    float wave6counter = 0;
    float wave6timer = 3;

    bool wave7complete = false;

    bool wave8complete = false;
    float wave8counter = 0;
    float wave8timer = 3;

    bool wave9complete = false;

    bool wave10complete = false;

    bool bosswavecomplete = false;
    float bosscounter = 0;
    float bosstimer = 5;
    bool audioplayed = false;



    GameObject currentEnemy;

    EnemyTemplate_Script enemyScript;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {


        

        if (isTutorial == false)
        {


            if (currentWave == 1 && wave1complete == false)
            {
                SpawnEnemy(1, new Vector3(27f, 1f, 5f), new Vector3(15f, 1f, 10f));
                killsNeeded = 1;
                wave1complete = true;
            }
            if (currentWave == 2 && wave2complete == false)
            {
                SpawnEnemy(1, new Vector3(27f, 1f, 5f), new Vector3(19f, 1f, 7f));
                SpawnEnemy(1, new Vector3(27f, 1f, 15f), new Vector3(20f, 1f, 12f));
                killsNeeded = 3;
                wave2complete = true;
            }
            if (currentWave == 3 && wave3complete == false)
            {
                SpawnEnemy(1, new Vector3(27f, 1f, 0f), new Vector3(17f, 1f, 3f));
                SpawnEnemy(1, new Vector3(27f, 1f, 7.5f), new Vector3(15f, 1f, 7.5f));
                SpawnEnemy(1, new Vector3(27f, 1f, 15f), new Vector3(16f, 1f, 12f));
                killsNeeded = 6;
                wave3complete = true;
            }
            if (currentWave == 4 && wave4complete == false)
            {
                SpawnEnemy(1, new Vector3(27f, 1f, 11f), new Vector3(20f, 1f, 10f));
                SpawnEnemy(1, new Vector3(27f, 1f, 2f), new Vector3(17f, 1f, 5.5f));
                SpawnEnemy(2, new Vector3(27f, 1f, 10f), new Vector3(15f, 1f, 10f));
                SpawnEnemy(1, new Vector3(27f, 1f, 17f), new Vector3(19f, 1f, 14.5f));
                killsNeeded = 10;
                wave4complete = true;
            }
            if (currentWave == 5 && wave5complete == false)
            {
                SpawnEnemy(1, new Vector3(27f, 1f, 7f), new Vector3(17f, 1f, 13f));
                SpawnEnemy(2, new Vector3(27f, 1f, 5f), new Vector3(14f, 1f, 7f));
                SpawnEnemy(2, new Vector3(27f, 1f, 15f), new Vector3(14f, 1f, 13f));
                SpawnEnemy(1, new Vector3(27f, 1f, 13f), new Vector3(18f, 1f, 7f));
                killsNeeded = 14;
                wave5complete = true;
            }
            if (currentWave == 6 && wave6complete == false)
            {
                killsNeeded = 18;
                timer += Time.deltaTime;
                if (timer >= wave6timer)
                {
                    wave6counter++;
                    SpawnEnemy(1, new Vector3(27f, 1f, 7f), new Vector3(17f, 1f, 13f));
                    timer = 0;

                }
                if(wave6counter >= 4)
                {
                    wave6complete = true;
                }

            }
            if (currentWave == 7 && wave7complete == false)
            {
                SpawnEnemy(3, new Vector3(27f, 1f, 8f), new Vector3(14f, 1f, 7f));
                SpawnEnemy(1, new Vector3(27f, 1f, 7f), new Vector3(17f, 1f, 15f));
                SpawnEnemy(1, new Vector3(27f, 1f, 16f), new Vector3(17f, 1f, 11f));
                killsNeeded = 21;
                wave7complete = true;

            }
            if (currentWave == 8 && wave8complete == false)
            {
                killsNeeded = 28;
                timer += Time.deltaTime;
                if (timer >= wave8timer)
                {
                    wave8counter++;
                    SpawnEnemy(1, new Vector3(27f, 1f, 5f), new Vector3(16f, 1f, 16f));
                    timer = 0;
                }
                if (wave8counter >= 4)
                {
                    SpawnEnemy(2, new Vector3(27f, 1f, 15f), new Vector3(14f, 1f, 7f));
                    SpawnEnemy(1, new Vector3(27f, 1f, 7f), new Vector3(17f, 1f, 15f));
                    SpawnEnemy(1, new Vector3(27f, 1f, 12f), new Vector3(16f, 1f, 5f));
                    wave8complete = true;
                }
            }

            if (currentWave == 9 && wave9complete == false)
            {
                killsNeeded = 31;
                SpawnEnemy(3, new Vector3(27f, 1f, 8f), new Vector3(12f, 1f, 7f));
                SpawnEnemy(3, new Vector3(27f, 1f, 8f), new Vector3(16f, 1f, 13f));
                SpawnEnemy(2, new Vector3(27f, 1f, 10f), new Vector3(15f, 1f, 10f));
                wave9complete = true;
            }

            if (currentWave == 10 && wave10complete == false)
            {
                SpawnEnemy(1, new Vector3(27f, 1f, 11f), new Vector3(20f, 1f, 10f));
                SpawnEnemy(1, new Vector3(27f, 1f, 2f), new Vector3(17f, 1f, 5.5f));
                SpawnEnemy(3, new Vector3(27f, 1f, 8f), new Vector3(12f, 1f, 7f));
                wave10complete = true;
                killsNeeded = 34;
            }

            if (currentWave == 11 && bosswavecomplete == false)
            {
                if (audioplayed == false)
                {
                    source.PlayOneShot(bossSFX);
                    audioplayed = true;
                }

                timer += Time.deltaTime;
                if (timer >= bosstimer)
                {
                    bosscounter++;
                    SpawnEnemy(5, new Vector3(55f, 0f, 10f), new Vector3(27f, 5.5f, 10f));
                    timer = 0;
                }
                if (bosscounter >= 1)
                {
                    bosswavecomplete = true;
                }

                killsNeeded = 35;
            }

        }

        if (isTutorial == true)
        {
            if (currentWave == 1 && wave1complete == false)
            {
                Txt1.SetActive(true);
                timer += Time.deltaTime;
                if (timer >= Text1timer)
                {
                    Text1counter++;
                    timer = 0;
                }
                if (Text1counter >= 1)
                {
                    Txt1.SetActive(false);
                    currentWave = 2;
                    wave1complete = true;
                }
            }
            if (currentWave == 2 && wave2complete == false)
            {
                SpawnEnemy(1, new Vector3(27f, 1f, 5f), new Vector3(15f, 1f, 10f));
                killsNeeded = 1;
                wave2complete = true;
            }

            if (killsNeeded == 1 && currentWave == 2)
            {
                Txt2.SetActive(true);
            }
            else
            {
                Txt2.SetActive(false);
            }

            if (currentWave == 3 && wave3complete == false)
            {
                SpawnEnemy(2, new Vector3(27f, 1f, 5f), new Vector3(19f, 1f, 7f));
                killsNeeded = 2;
                wave3complete = true;
            }
            if (killsNeeded == 2 && currentWave == 3)
            {
                Txt3.SetActive(true);
            }
            else
            {
                Txt3.SetActive(false);
            }

            if (currentWave == 4 && wave4complete == false)
            {
                SpawnEnemy(1, new Vector3(27f, 1f, 5f), new Vector3(19f, 1f, 7f));
                SpawnEnemy(1, new Vector3(27f, 1f, 15f), new Vector3(20f, 1f, 12f));
                player.GetComponent<MainCharacter_Script>().rageCount = 10;
                player.GetComponent<MainCharacter_Script>().zenCount = 10;
                killsNeeded = 4;
                wave4complete = true;
            }
            if (killsNeeded == 4 && currentWave == 4)
            {
                Txt4.SetActive(true);
            }
            else
            {
                Txt4.SetActive(false);
            }

        }

        if (killcount >= killsNeeded)
        {
            currentWave++;
        }

    }

    void SpawnEnemy(int enemyType, Vector3 spawnLoc, Vector3 targetLoc)
    {

        if (enemyType == 1)
        {

            currentEnemy = GameObject.Instantiate(enemy1, spawnLoc, Quaternion.identity) as GameObject;
            enemyScript = (EnemyTemplate_Script)currentEnemy.GetComponent(typeof(EnemyTemplate_Script));
            enemyScript.SetTarget(targetLoc);
            enemyScript.SetDodgeCount(Random.Range(1, 3));

        }
        else if (enemyType == 2)
        {

            currentEnemy = GameObject.Instantiate(enemy2, spawnLoc, Quaternion.identity) as GameObject;
            enemyScript = (EnemyTemplate_Script)currentEnemy.GetComponent(typeof(EnemyTemplate_Script));
            enemyScript.SetTarget(targetLoc);

        }
        else if (enemyType == 3)
        {
            currentEnemy = GameObject.Instantiate(enemy3, spawnLoc, Quaternion.identity) as GameObject;

        }
        else if (enemyType == 4)
        {

            currentEnemy = GameObject.Instantiate(enemy4, spawnLoc, Quaternion.identity) as GameObject;
            enemyScript = (EnemyTemplate_Script)currentEnemy.GetComponent(typeof(EnemyTemplate_Script));
            enemyScript.SetTarget(targetLoc);


        }
        else if (enemyType == 5)
        {
            currentEnemy = GameObject.Instantiate(boss, spawnLoc, Quaternion.Euler(-90f, 0, 0)) as GameObject;
            enemyScript = (EnemyTemplate_Script)currentEnemy.GetComponent(typeof(EnemyTemplate_Script));
            enemyScript.SetTarget(targetLoc);

        }

    }
}
