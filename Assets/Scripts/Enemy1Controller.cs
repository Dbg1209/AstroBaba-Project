using UnityEngine;
using UnityEngine.UI;

public class Enemy1Controller : MonoBehaviour
{
    [SerializeField] Animator enemyAnimator;
    [SerializeField] private Slider healthBar;
    public float enemyLife;
    public bool isMoving, isHitted, isDead, isJumping;
    void Start()
    {
        enemyLife = healthBar.value;
    }

    // Update is called once per frame
    void Update()
    {
        //Comprobar la salud del enemigo
        healthBar.value = enemyLife;

        //if (isHitted)
        //{
        //    enemyAnimator.SetBool("isHitted", isHitted);
        //}


        //if (isDead)
        //{
        //    enemyAnimator.SetBool("isDead", isDead);
        //}


        //if (isJumping) {
        //    enemyAnimator.SetBool("isJumping", isJumping);
        //}


        if (isMoving) {
            enemyAnimator.SetBool("isMoving", isMoving);
        }
        else
        {
            enemyAnimator.SetBool("isMoving", isMoving);
        }

    }
}
