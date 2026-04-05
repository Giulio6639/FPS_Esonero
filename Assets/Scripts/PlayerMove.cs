using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float playerSpeed = 20f;
    private CharacterController myCC;

    private Vector3 inputVector;
    private Vector3 movementVector;
    public float myGravity = -10f;
    public float momentumDamping = 5f;

    public Animator camAnim;
    private bool isWalking;

    void Start()
    {
        myCC = GetComponent<CharacterController>();    
    }

    void Update()
    {
        GetInput();
        MovePlayer();

        camAnim.SetBool("isWalking", isWalking);
    }

    void GetInput()
    {
        // se si preme WASD, allora ritora -1, 0, 1
        if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
        {
            inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            inputVector.Normalize();
            inputVector = transform.TransformDirection(inputVector);

            isWalking = true;
        }
        // se non si preme WASD, controlla com'era l'ultimo inputVector all'ultimo check, per poi lerparlo a 0
        else
        {
            inputVector = Vector3.Lerp(inputVector, Vector3.zero, momentumDamping * Time.deltaTime);

            isWalking = false;
        }

        movementVector = (inputVector * playerSpeed) + (Vector3.up * myGravity);
    }

    void MovePlayer()
    {
        myCC.Move(movementVector * Time.deltaTime); 
    }
}
