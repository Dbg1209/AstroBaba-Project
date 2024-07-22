using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed, rotationSpeed;
    [SerializeField] private GameObject model1, model2, model3;
    private float moveVertical, moveHorizontal;
    private Rigidbody playerRb;
    private Vector3 movement;
    private Animator currentAnimator;
    public bool isAttacking, isGrounded, isPushing;
    public bool isDead = false;

    private GameObject currentObstacle = null;

    void Start()
    {   
        isGrounded = true;

        playerRb = GetComponent<Rigidbody>();
        
     
        model1.SetActive(true);
        model2.SetActive(false);
        model3.SetActive(false);

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

        // Actualizar la animación de movimiento
        if (currentAnimator != null)
        {
            bool isMoving = movement.magnitude > 0.1f;
            currentAnimator.SetBool("isMoving", isMoving);
            currentAnimator.SetBool("isAttacking", isAttacking);
            currentAnimator.SetBool("isDead", isDead);
        }

        //Atacar con Z
        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartAttack();
        }

        //Saltar con espacio
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        // Empuje con X
        if (Input.GetKey(KeyCode.X))
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
            else if (!isPushing)
            {
                collision.gameObject.transform.SetParent(null);
            }
        }
    }

    //Mecanica de Absorbcion
    void AbsorbEnemy(string name)
    {
        Debug.Log(name);
        model1.SetActive(false);
        model2.SetActive(false);
        model3.SetActive(false);

        switch (name)
        {
            case "Enemy_Capsula":
                model2.SetActive(true);
                currentAnimator = model2.GetComponent<Animator>();
                break;
            case "Enemy_Cube":
                model3.SetActive(true);
                currentAnimator = model3.GetComponent<Animator>();
                break;
            default:
                break;
        }
        
        //Actualizar el isMoving con el nuevo parametro
        if (currentAnimator != null)
        {
            bool isMoving = movement.magnitude > 0.1f;
            currentAnimator.SetBool("isMoving", isMoving);
            currentAnimator.SetBool("isAttacking", isAttacking);
            currentAnimator.SetBool("isDead", isDead);
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
        playerRb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
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

}
