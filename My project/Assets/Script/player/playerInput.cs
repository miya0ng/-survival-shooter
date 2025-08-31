using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static readonly string verticalAxis = "Vertical";
    public static readonly string horizontalAxis = "Horizontal";
    public static readonly string fireButton = "Fire1";

    public float vAxis { get; private set; }
    public float hAxis { get; private set; }
    public bool Fire { get; private set; }
    public bool Reload { get; private set; }

    public Vector3 velocity;
    private void Update()
    {

        vAxis = Input.GetAxis(verticalAxis);
        hAxis = Input.GetAxis(horizontalAxis);

        velocity = new Vector3(hAxis, 0f, vAxis);
        if (velocity.magnitude > 1f)
        {
            velocity.Normalize();
        }

        Fire = Input.GetButton(fireButton);
    }
}