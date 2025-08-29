using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;

//public static class TagManager
//{
//    public static readonly string Player = "Player";
//}

public class PlayerMove : MonoBehaviour
{
    private static readonly int MoveHash = Animator.StringToHash("Blend");

    public float moveSpeed = 5f;

    private PlayerInput playerInput;
    public Rigidbody rb;
    private Animator animator;
    //private float speed = 20f;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Lookat();

        // transform.Rotate()

        //transform.Rotate(0f, Input.GetAxis("Mouse X") * speed, 0f, 0f);
        //transform.Rotate(Input.GetAxis("Mouse Y") * speed, 0f, 0f);
        //Input.mousePosition

        //transform.LookAt(0f, Input.mousePosition * speed, 0f, 0f);

        // ¿Ãµø
        rb.MovePosition(rb.position + playerInput.velocity * moveSpeed * Time.fixedDeltaTime);

        animator.SetFloat(MoveHash, playerInput.velocity.magnitude);

      //  transform.Rotate();
    }

    private void Lookat()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 target = hit.point;
            target.y = transform.position.y;
            transform.LookAt(target);
        }

    }
}


