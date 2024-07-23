using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]

    private Rigidbody playerRb;
    [SerializeField] private float speed, rotationSpeed;
    private float moveVertical, moveHorizontal;
    private Vector3 movement;

    [Header("Player Models")]

    [SerializeField] private GameObject model1, model2, model3, model4;
    private List<GameObject> absorbedModels = new List<GameObject>();
    private GameObject currentModel;
    private int currentModelIndex = -1;

    [Header("Animations")]
    private Animator currentAnimator;


    [Header("States")]

    public bool isAttacking, isGrounded, isPushing, isHitted, isMoving;
    public bool isDead = false;
    private GameObject currentObstacle = null;
   

    void Start()
    {   
        isGrounded = true;

        playerRb = GetComponent<Rigidbody>();
        
     
        model1.SetActive(true);
        model2.SetActive(false);
        model3.SetActive(false);

        absorbedModels.Add(model1);
        currentModel = model1;
        currentModelIndex = 0;

        currentAnimator = model1.GetComponent<Animator>();
    }

    private void Update()
    {   
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
        if (Input.GetKeyDown(KeyCode.Space) && currentModel == model1)
        {
            Debug.Log("Absorbiendo");
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


    }

    void FixedUpdate()
    {   
        playerRb.MovePosition(playerRb.position + movement * speed * Time.fixedDeltaTime);
    }

    //Colisiones
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) { 
            //collision.gameObject.SetActive(false);
            AbsorbEnemy(collision.gameObject.name);
        }

        if (collision.gameObject.CompareTag("Ground"))
        {   
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (isPushing)
            {
                AttachObstacle(collision.gameObject);
            }
        }
    }

    //Mecanica de Absorbcion
    void AbsorbEnemy(string name)
    {
        Debug.Log(name);

        GameObject newModel = null;

        switch (name)
        {
            case "Enemy1":
                newModel = model2;
                break;
            case "Enemy2":
                newModel = model3;
                break;
            case "Enemy3":
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

    //Mecanica de ataque

    void StartAttack()
    {
        if (currentAnimator != null)
        {
            isAttacking = true;
            currentAnimator.SetBool("isAttacking", isAttacking);
            Invoke("EndAttack", 0.05f); 
        }
    }

    void EndAttack()
    {
        isAttacking = false;
        if (currentAnimator != null)
        {
            currentAnimator.SetBool("isAttacking", isAttacking);
        }
    }


    //Saltar

    void Jump()
    {   
        playerRb.AddForce(Vector3.up * 8f, ForceMode.Impulse);
        isGrounded = false;
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

}
