using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuButtons_Script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Level1Click()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void TutorialClick()
    {
        SceneManager.LoadScene("Tutorial scene");
    }
}
