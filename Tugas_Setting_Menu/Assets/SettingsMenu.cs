using System.Net.Mime;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Debug = UnityEngine.Debug;
using UnityEngine.UI;
public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;
    Resolution[] resolutions;
    void Start() 
    {

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        int currentResolutionIndex = 0;
        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" +resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
             resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
                
            }
        }
        
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        
    }


    public void SetVolume (float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat("volume", volume);
    }

    // public void SetMute(bool isOn)
    // {
    //     GetComponent<AudioSource>().volume.mute();
    // }

    public void SetGraphicLow ()
    {
        
        Debug.Log("Graphic: Low");
    }
    
    public void SetGraphicMedium ()
    {
        Debug.Log("Graphic: Medium");
    }
    
    public void SetGraphicHigh ()
    {
        Debug.Log("Graphic: High");
    }

}
