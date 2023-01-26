using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string Name;
    public AudioClip Clip;
    [Range(0f, 1f)]
    public float Volume = 1f;
    public bool Loop;
    public SoundType Type;
}

public enum SoundType
{
    BGM, SFX
}
