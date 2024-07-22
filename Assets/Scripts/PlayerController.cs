using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed, rotationSpeed;
    private float moveVertical, moveHorizontal;
    private Rigidbody playerRb;
    private Vector3 movement;
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
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
    }

    void FixedUpdate()
    {
        playerRb.MovePosition(playerRb.position + movement * speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) { 
            
        }
    }

    void AbsorbEnemy()
    {

    }

    void PushEnemy()
    {

    }
}
