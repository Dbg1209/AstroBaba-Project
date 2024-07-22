using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed, rotationSpeed;
    [SerializeField] private GameObject model1, model2, model3;
    private float moveVertical, moveHorizontal;
    private Rigidbody playerRb;
    private Vector3 movement;
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
     
        model1.SetActive(true);
        model2.SetActive(false);
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
            //collision.gameObject.SetActive(false);
            AbsorbEnemy(collision.gameObject.name);
        }
    }

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
                break;
            case "Enemy_Cube":
                model3.SetActive(true);
                break;
            default:
                Debug.LogWarning("Unknown item type!");
                break;
        }
    }

    void PushObstacle()
    {

    }
}
