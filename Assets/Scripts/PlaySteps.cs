using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySteps : MonoBehaviour
{
   
 public void PlaySlimeStep()
 {
    AudioManager.PlaySound(SoundType.SlimeMovement);
 }
 public void PlayRobotStep()
 {
    AudioManager.PlaySound(SoundType.RobotSteps);
 }
 public void PlayRobotMovement()
 {
    AudioManager.PlaySound(SoundType.RobotMovement);
 }
}
