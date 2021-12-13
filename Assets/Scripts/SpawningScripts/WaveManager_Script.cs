using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager_Script : MonoBehaviour
{
    public Transform spawnpoint1;
    public Transform spawnpoint2;
    public Transform spawnpoint3;
    public Transform spawnpoint4;
    public Transform bossspawnpoint;
    public int randomoption;

    public float timer = 0;
    public float secondcounter = 1;
    public float wavetimer = 0;
    public float bosstimer = 0;

    bool spawnwave1;
    bool wave1complete = false;

    bool spawnwave2;
    bool wave2complete = false;

    bool spawnwave3;
    bool wave3complete = false;

    bool spawnwave4;
    bool wave4complete = false;

    bool spawnwave5;
    bool wave5complete = false;

    bool spawnwave6;
    bool wave6complete = false;

    bool bosswave;
    bool bosswavecomplete = false;

    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject boss;

    void Update()
    {

        //timer related stuff
        timer += Time.deltaTime;

        if (timer >= secondcounter)
        {
            wavetimer++;
            if (bosswave == true)
            {
                bosstimer++;
            }
            timer = 0;

        }

        if (wavetimer == 5)
        {
            spawnwave1 = true;
        }
        if (wavetimer == 10)
        {
            spawnwave2 = true;
        }
        if (wavetimer == 15)
        {
            spawnwave3 = true;
        }
        if (wavetimer == 20)
        {
            spawnwave4 = true;
        }
        if (wavetimer == 25)
        {
            spawnwave5 = true;
        }
        if (wavetimer == 30)
        {
            spawnwave6 = true;
        }
        if (wavetimer == 35)
        {
            bosswave = true;
        }

        //waves spawn here
        if (spawnwave1 == true && wave1complete == false)
        {
            Instantiate(enemy1, spawnpoint2.position, Quaternion.identity);

            wave1complete = true;
        }
        if (spawnwave2 == true && wave2complete == false)
        {
            Instantiate(enemy1, spawnpoint2.position, Quaternion.identity);
            Instantiate(enemy1, spawnpoint1.position, Quaternion.identity);
            wave2complete = true;
        }
        if (spawnwave3 == true && wave3complete == false)
        {
            Instantiate(enemy2, spawnpoint3.position, Quaternion.identity);

            wave3complete = true;
        }
        if (spawnwave4 == true && wave4complete == false)
        {
            Instantiate(enemy1, spawnpoint4.position, Quaternion.identity);
            Instantiate(enemy2, spawnpoint1.position, Quaternion.identity);
            wave4complete = true;
        }
        if (spawnwave5 == true && wave5complete == false)
        {
            for (int i = 0; i < 5; i++)
            {
                Instantiate(enemy1, spawnpoint2.position, Quaternion.identity);
            }
            wave5complete = true;
        }
        if (spawnwave6 == true && wave6complete == false)
        {
            for (int i = 0; i < 3; i++)
            {
                Instantiate(enemy1, spawnpoint3.position, Quaternion.identity);
            }
            Instantiate(enemy2, spawnpoint1.position, Quaternion.identity);
            wave6complete = true;
        }

        //boss spawning
        if (bosswave == true && bosswavecomplete == false)
        {
            Instantiate(boss, bossspawnpoint.position, Quaternion.identity);
            bosswavecomplete = true;
        }
        if (bosswave == true)
        {
            if(bosstimer >= 5)
            {
                Instantiate(enemy1, spawnpoint1.position, Quaternion.identity);
                bosstimer = 0;
            }
        }

    }
}
