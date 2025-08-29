using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
   // public static readonly int IdReload = Animator.StringToHash("Reload");

    public Gun gun;

    private Rigidbody gunRb;
    private Collider gunCollider;

    private PlayerInput input;
    //private Animator animator;


    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        //animator = GetComponent<Animator>();

        //gunInitPos = gun.transform.localPosition;
        //gunInitRot = gun.transform.localRotation;
    }

    //private void OnEnable()
    //{
    //    gunRb.isKinematic = true;
    //    gunCollider.enabled = false;
    //}

    private void OnDisable()
    {
        // 초기값으로 돌리기위해 
        //gunRb.linearVelocity = Vector3.zero;
        //gunRb.angularVelocity = Vector3.zero;

        //gunRb.isKinematic = false;
        //gunCollider.enabled = true;
    }

    private void Update()
    {
        if (input.Fire)
        {
           gun.Fire();
        }
    }
}