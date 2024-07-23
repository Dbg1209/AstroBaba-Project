using UnityEngine;
using UnityEngine.UI;

public class Enemy1Controller : MonoBehaviour
{
    [SerializeField] Animator enemyAnimator;
   
    public bool isMoving, isHitted, isDead, isJumping;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        

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
