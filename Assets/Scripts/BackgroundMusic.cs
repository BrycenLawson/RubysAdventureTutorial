using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
public AudioClip musicClipOne;

public AudioClip musicClipTwo;

public AudioClip musicClipThree;

public AudioSource musicSource;

void Start()
    {
        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;

    }

void Update()
{
    if (EnemyController.count + HardEnemyController.count >= 4)
        {
          musicSource.clip = musicClipTwo;
          if(!musicSource.isPlaying) musicSource.Play();
        }

    if (RubyController.currentHealth == 0)
        {
          musicSource.clip = musicClipThree;
          if(!musicSource.isPlaying) musicSource.Play();
        }  
    
    if (Input.GetKeyDown(KeyCode.L))
        {
          musicSource.loop = true;
         }

     if (Input.GetKeyUp(KeyCode.L))
        {
          musicSource.loop = false;
        }
    }
}
        

