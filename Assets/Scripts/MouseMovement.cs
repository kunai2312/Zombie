using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    float xRotation = 0f;
    float yRotation = 0f;
    public float topClamp = -90f;
    public float bottomClamp = 90f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //khóa chuột giữa màn hình
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //nhận đầu vào của chuột
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //xoay chuột(xR :xoay lên xuống của camera)
        xRotation -= mouseY;

        //giới hạn góc xoay
        xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp);

        //xoay chuột(yR: xoay ngang của camera)
        yRotation += mouseX;

        //xoay camera:
        //localRotation:xoay theo cha(player)
        //Quaternion.Euler(X(lên-xuống),Y(trái-phải),Z(nghiêng))
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

    }
}
