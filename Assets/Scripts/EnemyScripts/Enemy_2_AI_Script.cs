using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2_AI_Script : MonoBehaviour
{
    public AudioSource Source;
    public AudioClip ShotSFX;
    public AudioClip DamageSFX;
    public AudioClip DeathSFX;
    public AudioClip ShieldSFX;
    bool shotTrigger = false;

    GameObject myMainCharacter;
    GameObject SpawnScript;
    MainCharacter_Script myMainCharacterScript;
    LineRenderer myLineRenderer;

    public GameObject impactAnim;
    public GameObject eye;
    public GameObject eyeLid;
    public GameObject wake1;
    public GameObject wake2;

    bool circleSpawned = false;
    float circleSize = 0f;

    bool shieldOn = false;
    float shieldTimer;

    private int health = 5;
    bool dead = false;

    float vertexCounter = 30;

    float elapsedTime = 0f;
    SkinnedMeshRenderer[] enemyRenderer;
    // Start is called before the first frame update
    void Start()
    {
        myLineRenderer = gameObject.GetComponentInChildren<LineRenderer>();
        myMainCharacter = GameObject.FindGameObjectWithTag("Main Character");
        SpawnScript = GameObject.FindGameObjectWithTag("Spawner");
        myMainCharacterScript = myMainCharacter.GetComponent<MainCharacter_Script>();
        enemyRenderer = GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health < 1 && !dead)
        {
            foreach(SkinnedMeshRenderer renderer in enemyRenderer)
            {
                renderer.enabled = false;
            }
            eye.SetActive(false);
            eyeLid.SetActive(false);
            wake1.SetActive(false);
            wake2.SetActive(false);
            GetComponent<Collider>().enabled = false;
            
            dead = true;
        }
        if (shieldOn && shieldTimer == 0.0f)
        {
            Source.PlayOneShot(ShieldSFX, 0.2f);
        }
        if(shieldOn)
        {
            shieldTimer += Time.deltaTime;
            if(shieldTimer > 4f)
            {
                shieldOn = false;
                foreach (SkinnedMeshRenderer renderer in enemyRenderer)
                {
                    renderer.material.SetFloat("_ShieldActive", 0f);
                }
                shieldTimer = 0;
                GetComponent<MeshRenderer>().material.color = new Color(128,128,128);
            }
            
        }

        elapsedTime += Time.deltaTime;
        if(elapsedTime > 4f)
        {
            if (dead)
            {
                SpawnScript.GetComponent<SpawnManager_Script>().killcount++;
                myMainCharacter.GetComponent<MainCharacter_Script>().rageCount++;
                Source.PlayOneShot(DeathSFX, 0.5f);
                Destroy(gameObject);

            }
            if (!circleSpawned)
            {
                elapsedTime = 0;
                myLineRenderer.enabled = true;
                circleSpawned = true;
                shotTrigger = true;
            }
            else
            {
                
                elapsedTime = 2f;
                myLineRenderer.enabled = false;
                circleSize = 0f;
                circleSpawned = false;
            }
        }

        if(circleSpawned)
        {
            circleSize += 6f*Time.deltaTime;
            DrawPolygon(30, circleSize, gameObject.transform.position, 0.3f, 0.3f);
            CircleCollision();
        }
        if(circleSpawned && shotTrigger == true)
        {
            Source.PlayOneShot(ShotSFX, 0.3f);
            shotTrigger = false;
        }
    }

    void DrawPolygon(int vertexNumber, float radius, Vector3 centerPos, float startWidth, float endWidth)
    {
        myLineRenderer.startWidth = startWidth;
        myLineRenderer.endWidth = endWidth;
        myLineRenderer.loop = true;
        float angle = 2 * Mathf.PI / vertexNumber;
        myLineRenderer.positionCount = vertexNumber;

        for (int i = 0; i < vertexNumber; i++)
        {
            Matrix4x4 rotationMatrix = new Matrix4x4(new Vector4(Mathf.Cos(angle * i), 0, Mathf.Sin(angle * i), 0),
                                                     new Vector4(-1 * Mathf.Sin(angle * i), 0, Mathf.Cos(angle * i), 0),
                                       new Vector4(0, 0, 1, 0),
                                       new Vector4(0, 0, 0, 1));
            Vector3 initialRelativePosition = new Vector3(0, radius, 0);
            myLineRenderer.SetPosition(i, centerPos + rotationMatrix.MultiplyPoint(initialRelativePosition));

        }
    }

    void CircleCollision()
    {
        float characterCircle = 0.5f;
        float distance = Vector3.Magnitude(myMainCharacter.transform.position - gameObject.transform.position);
        if (distance < characterCircle + circleSize && distance > characterCircle + circleSize - 1.2f)
        {
            myMainCharacterScript.TakeDamage(1);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {

        if(collision.tag == "PlayerProjectile")
        {
            GameObject impact = Instantiate(impactAnim, new Vector3(gameObject.transform.position.x - 0.3f, collision.transform.position.y, collision.transform.position.z), Quaternion.Euler(90f, 0f, 0f));
            impact.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
            Destroy(collision.gameObject);
            if (!shieldOn)
            {
                shieldOn = true;
                Source.PlayOneShot(DamageSFX,0.5f);
                health -= 1;

                //Temporary
                foreach (SkinnedMeshRenderer renderer in enemyRenderer)
                {
                    renderer.material.SetFloat("_ShieldActive", 1f);
                }
            }
            

        }
        if (collision.tag == "PlayerLaser")
        {
            Instantiate(impactAnim, new Vector3(gameObject.transform.position.x - 0.3f, 2f, collision.transform.position.z), Quaternion.Euler(90f, 0f, 0f));
            Source.PlayOneShot(DamageSFX);
            health -= 3;
        }
    }


}
