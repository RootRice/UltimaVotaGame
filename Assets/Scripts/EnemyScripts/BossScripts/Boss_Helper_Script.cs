using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Helper_Script : MonoBehaviour
{

    bool attackMode = false;
    float activeTimer = 0f;

    float circleSize;
    LineRenderer myLineRenderer;
    bool circleSpawned;
    bool attacking;

    bool firing = false;
    bool locked = false;
    [SerializeField] GameObject laser;
    float angle;
    
    MeshRenderer eyeRenderer;
    MainCharacter_Script myMainCharacterScript;
    GameObject myMainCharacter;

    float attackTimer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        eyeRenderer = GetComponent<MeshRenderer>();
        myLineRenderer = GetComponent<LineRenderer>();
        myMainCharacterScript = FindObjectOfType<MainCharacter_Script>();
        myMainCharacter = GameObject.FindGameObjectWithTag("Main Character");
        //SetActive(true, 72);
    }

    // Update is called once per frame
    void Update()
    {
        if (activeTimer < 0)
        {
            activeTimer += Time.deltaTime;
            if (!attackMode)
            { 
               WaveAttack();
            }
            else
            {
                LaserAttack();
            }
        }
        else if (circleSpawned)
        {
            WaveAttack();
        }
        else
        {
            activeTimer = 0;
            attackTimer = 0;
        }
        
        
    }

    void WaveAttack()
    {
        if (circleSpawned)
        {
            attackTimer += Time.deltaTime;
            circleSize += 6f * Time.deltaTime;
            DrawPolygon(30, circleSize, gameObject.transform.position, 0.3f, 0.3f);
            CircleCollision();
            if (attackTimer > 6f)
            {
                circleSize = 0;
                circleSpawned = false;
                myLineRenderer.enabled = false;
                attackTimer = Random.Range(-1.1f, 1.1f);
            }
        }
        else
        {
            attackTimer += Time.deltaTime;
            if (attackTimer > 0f)
            {
                circleSpawned = true;
                myLineRenderer.enabled = true;
                attackTimer = 0;
            }
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

    void LaserAttack()
    {

        attackTimer += Time.deltaTime;
        if(attackTimer > 0.5f && !locked)
        {
            angle = Vector3.Angle(gameObject.transform.position - myMainCharacter.transform.position + new Vector3(0.68f, 0, 0f), new Vector3(0f, 0f, 1f));
            if (gameObject.transform.position.x < myMainCharacter.transform.position.x)
            {
                angle = 360 - angle;
            }
            locked = true;
        }
        if (attackTimer > 1f && !firing)
        {
            Vector3 spawnLoc = new Vector3(gameObject.transform.position.x, 1, gameObject.transform.position.z);
            Instantiate(laser, spawnLoc, Quaternion.Euler(new Vector3(0f, angle, 0f)));
            firing = true;
        }
        else if (attackTimer > 1.6f)
        {
            firing = false;
            attacking = false;
            locked = false;
            attackTimer = -5.0f;
            eyeRenderer.material.SetColor("_Color", new Color(1, Mathf.Lerp(1f, 0f, attackTimer), Mathf.Lerp(1f, 0f, attackTimer), 1f));
        }
        else if (firing)
        {
            eyeRenderer.material.SetColor("_Color", Color.Lerp(Color.magenta, Color.white, attackTimer - 0.6f));
        }
        else
        {
            eyeRenderer.material.SetColor("_Color", Color.Lerp(Color.white, Color.magenta, attackTimer));
        }
    }

    public void SetActive(bool mode, float timer)
    {
        attackMode = mode;
        activeTimer = -timer;
        if(mode == false)
        {
            attackTimer = Random.Range(-0.9f, 0f);
        }
        angle = Vector3.Angle(gameObject.transform.position - myMainCharacter.transform.position, new Vector3(0f, 0f, 1f));
        if(gameObject.transform.position.x < myMainCharacter.transform.position.x)
        {
            angle = 360 - angle;
        }
    }
}
