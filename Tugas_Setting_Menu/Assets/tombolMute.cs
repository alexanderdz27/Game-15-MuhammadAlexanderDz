using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tombolMute : MonoBehaviour
{
    public void mute(bool value)
    {
       if (value)
       {
        Debug.Log("mute");
       }
       else
       {
        Debug.Log("unmute");
       }
    }
}
