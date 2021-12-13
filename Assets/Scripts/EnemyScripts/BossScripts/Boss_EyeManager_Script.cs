using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_EyeManager_Script : MonoBehaviour
{
    [SerializeField] GameObject[] eyes;
    GameObject myMainCharacter;
    float timer = 0f;
    public bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        myMainCharacter = GameObject.FindGameObjectWithTag("Main Character");
        //SelectEyes();
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            timer += Time.deltaTime;
            if(timer > 1.8f)
            {
                timer = 0f;
                SelectEyes();
            }
        }
    }

    public void SelectEyes()
    {
        List<GameObject> attackers = new List<GameObject>();
        foreach(GameObject eye in eyes)
        {
            if(Vector3.Magnitude(eye.transform.position - myMainCharacter.transform.position) > 10f)
            {
                attackers.Add(eye);
            }
        }
        attackers[Random.Range(0, attackers.Count)].GetComponent<Boss_Minieye_Script>().Activate();
    }
}
