using UnityEngine;
using UnityEngine.UI;

public class Enemy3Controller : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    public float enemyLife;
    void Start()
    {
        enemyLife = healthBar.value;
    }

    // Update is called once per frame
    void Update()
    {
        //Comprobar la salud del enemigo
        healthBar.value = enemyLife;
    }
}
