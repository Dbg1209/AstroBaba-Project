using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField] Animator enemyAnimator;
    [SerializeField] private Slider healthBar;
    public float enemyLife, damageAmount;
    void Start()
    {
        enemyLife = healthBar.value;

    }

    // Update is called once per frame
    void Update()
    {
        //Comprobar la salud del enemigo
        healthBar.value = enemyLife;

        if (enemyLife <= 0 && !enemyAnimator.GetBool("isDead"))
        {
            enemyAnimator.SetBool("isDead", true);
            // Llamar a la corrutina para desactivar después de 5 segundos
            StartCoroutine(DeactivateAfterDelay(5f)); 
        }
    }

    private IEnumerator DeactivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }


    // Método para manejar las colisiones
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Leg"))
        {
            TakeDamage(damageAmount);
        }
    }

    private void TakeDamage(float amount)
    {
        enemyLife -= amount;
    }
}
