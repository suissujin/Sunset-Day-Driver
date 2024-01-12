using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteHandler : MonoBehaviour
{
    public void MuteAudioHandler()
    {
        AudioManagerScript.instance.MuteAudio();
    }
}
