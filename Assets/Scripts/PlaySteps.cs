using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySteps : MonoBehaviour
{
 public void PlaySound()
 {
    AudioManager.PlaySound(SoundType.Movement);
 }
}
