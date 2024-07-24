using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
   
   public void ToMainMenu()
   {
    SceneManager.LoadScene("Main Menu");
   }
   public void RestartGame()
   {
    SceneManager.LoadScene("sceneName");
   }
}
