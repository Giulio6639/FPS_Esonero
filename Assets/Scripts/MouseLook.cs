using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivity = 1.5f;
    public float smoothing = 1.5f;

    private float xMousePos;
    private float yMousePos;

    private float smoothedMousePosX;
    private float smoothedMousePosY;

    private float currentLookingPosX;
    private float currentLookingPosY;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        GetInput();
        ModifyInput();
        MovePlayer();
    }

    void GetInput()
    {
        xMousePos = Input.GetAxisRaw("Mouse X");
        yMousePos = Input.GetAxisRaw("Mouse Y"); 
    }

    void ModifyInput()
    {
        xMousePos *= sensitivity + smoothing;
        yMousePos *= sensitivity + smoothing;

        smoothedMousePosX = Mathf.Lerp(smoothedMousePosX, xMousePos, 1f / smoothing);
        smoothedMousePosY = Mathf.Lerp(smoothedMousePosY, yMousePos, 1f / smoothing);
    }

    void MovePlayer()
    {
        currentLookingPosX += smoothedMousePosX;

        currentLookingPosY -= smoothedMousePosY;

        currentLookingPosY = Mathf.Clamp(currentLookingPosY, -90f, 90f);

        transform.localRotation = Quaternion.Euler(currentLookingPosY, currentLookingPosX, 0f);
    }
}