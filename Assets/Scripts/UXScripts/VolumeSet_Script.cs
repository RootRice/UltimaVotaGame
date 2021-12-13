using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeSet_Script : MonoBehaviour
{
    public AudioMixer Mixer;

    public void SetLevel(float SliderValue)
    {
        Mixer.SetFloat("MainVol", Mathf.Log10(SliderValue) * 20);
    }

}
