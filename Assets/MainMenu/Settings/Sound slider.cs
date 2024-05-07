using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Soundslider : MonoBehaviour
{
    [SerializeField] Slider VolumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        //checks to see if there is a saved player preference for the volume slider if none detected slider is set to max value(1) otherwise load preference
        if (!PlayerPrefs.HasKey("SoundVolume"))
        {
            PlayerPrefs.SetFloat("SoundVolume", 1);
            Load();
        }
        else
        {
            
            Load();
        }
    }
    //sets volume to slider value
    public void ChangeVolume()
    {
        AudioListener.volume = VolumeSlider.value;
        Save();
    }
    //using PlayerPrefs to save the value of the volume slider so it does not have to be changed every launch
    private void Load()
    {
        VolumeSlider.value = PlayerPrefs.GetFloat("SoundVolume");
    }
    private void Save()
    {
        PlayerPrefs.SetFloat("SoundVolume", VolumeSlider.value);
        PlayerPrefs.Save();
    }
}
