using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum SoundType
{
    walk,
    run,
    jump,
    fall,
    grounded,
    carpetGrounded,
    pickup
}
public class SoundPlayer : MonoBehaviour
{
    public AudioSource musicSource;   // Background music source
    public AudioSource sfxSourceOneShot;     // Sound effect source
    public AudioSource sfxSourceContinuous; // for falling sound

    [SerializeField]
    AudioClip[] SFXclips;
    [SerializeField]
    AudioClip[] WalkWood;
    [SerializeField]
    AudioClip[] WalkCarpet;
    [SerializeField]
    AudioClip[] music;

    AudioClip currentCLip;
    bool groundCarpet = true;

    private void Awake()
    {
        AnimationEvents.soundEmited += PlaySFX;
        MovePlayerRB.PlayerGrounded += PlaySFX;
        Player.OnItemCollected += PlaySFX;
        NotifyFalling.Falling += PlayFallingSound;
        

    }

    // Start is called before the first frame update
    void Start()
    {
        currentCLip = SFXclips[0];
        // Start playing background music
        musicSource.clip = music[Random.Range(0, music.Length)];
        musicSource.loop = true; // Loop background music
        musicSource.Play();
        sfxSourceContinuous.clip = SFXclips[1];
        sfxSourceContinuous.loop = true;
    }

    // Play a sound effect
    public void PlaySFX( SoundType type)
    {
        if (type == SoundType.walk)
        {
            if(!groundCarpet) 
            {
                int random = Random.Range(0, WalkWood.Length);
                currentCLip = WalkWood[random];
            }
            else
            {
                int random = Random.Range(0, WalkCarpet.Length);
                currentCLip = WalkCarpet[random];
            }
            
        }
        else if (type == SoundType.jump) 
            currentCLip = SFXclips[0];
        else if (type == SoundType.grounded)
        {
            currentCLip = SFXclips[3];
            groundCarpet = false;
            sfxSourceContinuous.Stop();// stop falling sound
        }
        else if (type == SoundType.carpetGrounded)
        {
            currentCLip = SFXclips[4];
            groundCarpet = true;
            sfxSourceContinuous.Stop();// stop falling sound
        }
        sfxSourceOneShot.PlayOneShot(currentCLip);
    }
    public void PlaySFX(ItemType type)
    {
            currentCLip = SFXclips[2];

        sfxSourceOneShot.PlayOneShot(currentCLip);
    }
    public void PlayFallingSound(bool flag)
    {
        if (flag)
        {
            sfxSourceContinuous.Play();
        }
        else
        {
            sfxSourceContinuous.Stop();
        }
        
       
         
        
    }
}
