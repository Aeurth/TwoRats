using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimationEvents : MonoBehaviour
{
    public static event Action<SoundType> soundEmited;

    public void StepingSound()
    {
        soundEmited?.Invoke(SoundType.walk);
    }
    public void JumpingSound()
    {
        soundEmited?.Invoke(SoundType.jump);
    }
}
