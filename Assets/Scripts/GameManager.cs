using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    public static GameManager Instance;
    public GameState state;

    void Awake() 
    {   
    if (Instance != null)
    {
        Instance = this;    
    }
        
    }
    public enum GameState
    {

    }
   

}
