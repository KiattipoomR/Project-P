using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
  public class SoundManager : MonoBehaviour
  {
    [SerializeField] AudioSource musicBox;
    [SerializeField] AudioSource soundEffectBox;
    [SerializeField] AudioClip defaultBackgroundMusic;
    [SerializeField] AudioClip uiToggleSound;

    public void PlayToggleUISound(bool state)
    {
      soundEffectBox.clip = uiToggleSound;
      if (state)
      {
        soundEffectBox.pitch = 1f;
      }
      else
      {
        soundEffectBox.pitch = 0.4f;
      }
      soundEffectBox.Play();
    }

    public void PlayBackgroundMusic(AudioClip musicClip, bool useDefaultIfNull)
    {
      if (musicClip == null && useDefaultIfNull)
      {
        musicClip = defaultBackgroundMusic;
      }
      if (musicBox.clip != musicClip)
      {
        musicBox.clip = musicClip;
        musicBox.Play();
      }
    }
  }
}