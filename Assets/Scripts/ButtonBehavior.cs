using System;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    public AudioClip hoverSound;
    public AudioClip selectSound;

    public void PlayHoverSound()
    {
        AudioManager.Instance.PlaySound(hoverSound);
    }

    public void PlaySelectSound()
    {
        AudioManager.Instance.PlaySound(selectSound);
    }
}