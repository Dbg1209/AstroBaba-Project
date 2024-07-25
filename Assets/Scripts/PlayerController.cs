using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]

    private Rigidbody playerRb;
    [SerializeField] private float speed, rotationSpeed, playerLife, playerDamage;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Collider legCollider1, legCollider2;
    private float moveVertical, moveHorizontal;
    private Vector3 movement;
    public float baba;
    

    [Header("Player Models")]

    [SerializeField] private GameObject model1, model2, model3, model4;
    private List<GameObject> absorbedModels = new List<GameObject>();
    private GameObject currentModel;
    private int currentModelIndex = -1;

    [Header("Animations")]
    private Animator currentAnimator;
    [SerializeField] GameObject endCinematic, spriteModel1, spriteModel2, spriteModel3,absorbAlert, spaceBarAlert;
    [SerializeField] TMP_Text babaText;

    [Header("Canvas")]
    [SerializeField] private GameObject alertCanvas, formCanvas;

    [Header("States")]

    public bool isAttacking, isGrounded, isPushing, isHitted, isMoving, isGameOver, isAbsorbing;
    public bool isDead = false;
    private GameObject currentObstacle = null;
   

    void Start()
    {
        GameManager.Instance.HideAll();
        GameManager.Instance.TimeScale();
        endCinematic.SetActive(false);

        isGrounded = true;

        baba = 0f;

        playerRb = GetComponent<Rigidbody>();
        playerLife = healthBar.value;
        
        alertCanvas.SetActive(false);
        formCanvas.SetActive(false);
     
        model1.SetActive(true);
        model2.SetActive(false);
        model3.SetActive(false);
        model4.SetActive(false);

        spriteModel1.SetActive(true);
        spriteModel2.SetActive(false);
        spriteModel3.SetActive(false);
        absorbAlert.SetActive(false);
        spaceBarAlert.SetActive(true);

        absorbedModels.Add(model1);
        currentModel = model1;
        currentModelIndex = 0;
        
        legCollider1.enabled = false;
        legCollider2.enabled = false;

        currentAnimator = model1.GetComponent<Animator>();
    }

    private void Update()
    {   
        //Estado del juego
        isGameOver = false;

        //Comprobar salud Player
        healthBar.value = playerLife;

        //Actualizar baba
        babaText.text = "Baba: " + baba + " / 6";

        //Condicion de GameOver
        if (playerLife <= 0) 
        {
            isGameOver = true;
            GameManager.Instance.GameOver();
        }

        //Controles de movimientos
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        //Rotacion
        float rotation = moveHorizontal * rotationSpeed * Time.deltaTime;
        transform.Rotate(0f, rotation, 0f);

        //Vector Vertical
        movement = transform.forward * moveVertical;
        isMoving = movement.magnitude > 0.1f;

        // Actualizar la animación de movimiento
        if (currentAnimator != null)
        {
            currentAnimator.SetBool("isMoving", isMoving);
            currentAnimator.SetBool("isAttacking", isAttacking);
            currentAnimator.SetBool("isHitted", isHitted);
            currentAnimator.SetBool("isDead", isDead);
        }

        //Atacar con Z
        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartAttack();
        }

        //Absorber
        if (Input.GetKey(KeyCode.Space) && currentModel == model1)
        {
            isAbsorbing = true;
            spaceBarAlert.SetActive(false);
        }
        else
        {
            isAbsorbing = false;
        }

        //Saltar con Espacio
        if (Input.GetKeyDown(KeyCode.Space) && (currentModel == model2 || currentModel == model4))
        {   
            Jump();
        }

        // Empuje con Espacio
        if (Input.GetKey(KeyCode.Space) && currentModel == model3)
        {
            isPushing = true;
        }
        else
        {
            isPushing = false;
        }

        // Si estamos dejando de empujar, desvincular el objeto
        if (!isPushing && currentObstacle != null)
        {
            DetachObstacle();
        }

        // Cambiar de modelo con Q
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeModel();
        }

        //Comprobar Sprite
        SwitchSprite();

    }

    void FixedUpdate()
    {   
        playerRb.MovePosition(playerRb.position + movement * speed * Time.fixedDeltaTime);
    }

    //Colisiones
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) {

            AbsorbEnemy(collision.gameObject.name);


            AttackEnemy(collision.gameObject);
        }


        if (collision.gameObject.CompareTag("Ground"))
        {   
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Ship"))
        {
            //Temporal
            alertCanvas.SetActive(true);
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            isGrounded = true;

            if (currentModel != model3) 
            { 
                formCanvas.SetActive(true);
            }

            if (isPushing)
            {
                AttachObstacle(collision.gameObject);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle")) 
        {
            Invoke("DeactivateFormCanvas", 2f);
        }

        if (collision.gameObject.CompareTag("Ship"))
        {
            if (baba < 6) 
            {
                Invoke("DeactivateAlertCanvas", 2f);
            }
            else if(baba == 6) 
            {
                endCinematic.SetActive(true);
            }
        }
    }

    //Mecanica de Absorbcion
    void AbsorbEnemy(string name)
    {
        if (isAbsorbing)
        {
            Debug.Log(name);

            GameObject newModel = null;

            switch (name)
            {
                case "T_Enemy_A":
                    newModel = model2;
                    break;
                case "L1_Enemy_B":
                    newModel = model3;
                    break;
                case "L2_Enemy_C_1":
                    newModel = model4;
                    break;
                default:
                    Debug.LogWarning("Unknown item type!");
                    return;
            }

            // Si el nuevo modelo no está en la lista de modelos absorbidos, añadirlo
            if (!absorbedModels.Contains(newModel))
            {
                absorbedModels.Add(newModel);

                // Activar el nuevo modelo
                SetCurrentModel(newModel);
            }
        }

    }

    //Mecanica de ataque

    void StartAttack()
    {
        if (currentAnimator != null)
        {
            legCollider1.enabled = true;
            legCollider2.enabled = true;
            isAttacking = true;
            currentAnimator.SetBool("isAttacking", isAttacking);
            Invoke("EndAttack", 0.5f);

        }
    }

    void EndAttack()
    {
        isAttacking = false;
        if (currentAnimator != null)
        {
            currentAnimator.SetBool("isAttacking", isAttacking);
        }
        legCollider1.enabled = false;
        legCollider2.enabled = false;

    }

    void AttackEnemy(GameObject enemy)
    {   
        if (isAttacking)
        {   
           enemy.GetComponent<EnemyStatus>().enemyLife -= playerDamage;
        }
    }

    //Saltar

    void Jump()
    {
        if (isGrounded) {
            playerRb.AddForce(Vector3.up * 8f, ForceMode.Impulse);
            isGrounded = false;
        }    
    }

    //Mecanica de empujar
   
    void AttachObstacle(GameObject obstacle)
    {
        if (obstacle != null)
        {
            currentObstacle = obstacle;
            currentObstacle.transform.SetParent(transform);
        }
    }

    void DetachObstacle()
    {
        if (currentObstacle != null)
        {
            currentObstacle.transform.SetParent(null);
            currentObstacle = null;
        }
    }

    //Mecanica de intercambio de modelos / Transformaciones
    void SetCurrentModel(GameObject newModel)
    {
        if (currentModel != null)
        {
            currentModel.SetActive(false);
        }

        currentModel = newModel;
        currentModel.SetActive(true);
        currentAnimator = currentModel.GetComponent<Animator>();
    }

    void ChangeModel()
    {
        if (absorbedModels.Count == 0) return;

        // Desactivar el modelo actual
        if (currentModel != null)
        {
            currentModel.SetActive(false);
        }

        // Cambiar al siguiente modelo en la lista
        currentModelIndex = (currentModelIndex + 1) % absorbedModels.Count;
        currentModel = absorbedModels[currentModelIndex];
        currentModel.SetActive(true);
        currentAnimator = currentModel.GetComponent<Animator>();
    }

    //Logica del Canvas y Alertas
    private void DeactivateFormCanvas()
    {
        formCanvas.SetActive(false);
    }

    private void DeactivateAlertCanvas()
    {
        alertCanvas.SetActive(false);
    }

    private void SwitchSprite()
    {
        if (baba >= 3 && baba < 6) 
        { 
            spriteModel1.SetActive(false);
            spriteModel2.SetActive(true);
        }

        if (baba == 6) 
        {
            spriteModel2.SetActive(false);
            spriteModel3.SetActive(true);
            absorbAlert.SetActive(true);
        }
    }
}
