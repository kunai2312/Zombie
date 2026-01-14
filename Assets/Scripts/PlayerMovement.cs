using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f * 2;
    public float jumHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    Vector3 velocity;
    bool isGrounded;
    bool isMoving;
    private Vector3 lastPosition = new Vector3(0f, 0f, 0f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //kiểm tra có chạm đất không
        //Physics.CheckSphere(pos, radius, mask, QueryTriggerInteraction.Ignore(ít sd));
        //pos:vị trí muốn kt || radius:bán kính hình cầu || mask: kt collider thuộc layer này
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //giữ nhân vật dính đất tránh bug
        //velocity.y: vận tốc lên-xuống
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //nhận đầu vào
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //tạo hướng di chuyển
        //dùng transform. để di chuyển theo hướng của nhân vật
        //dùng "+" : W+D,W+A... đi chéo
        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        //di chuyển nhân vật theo hướng nhìn,tốc độ,không phụ thuộc FPS
        controller.Move(move * speed * Time.deltaTime);

        //kt nhân vật có thể nhảy kh
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //công thức:v² = v₀² + 2as (a:gravity || s:jumpHeight || v=0)
            // ==> v₀ = √(-2 × gravity × jumpHeight)
            velocity.y = Mathf.Sqrt(jumHeight * -2f * gravity);
        }

        //rơi xuống
        velocity.y += gravity * Time.deltaTime;

        //thực hiện nhảy
        controller.Move(velocity * Time.deltaTime);

        //kt xem nhân vật có di chuyển hay kh, và chạm đất kh
        if (lastPosition != gameObject.transform.position && isGrounded == true)
        {
            //di chuyển
            isMoving = true;

        }
        else
        {
            //đứng yên,nhảy
            isMoving = false;
        }

        //lưu vị trí hiện tại để tiếp tục so sánh
        lastPosition = gameObject.transform.position;




    }
}
