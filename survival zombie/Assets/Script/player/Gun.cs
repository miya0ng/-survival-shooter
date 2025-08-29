using System.Collections;
using TMPro.EditorUtilities;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // public UiHud uiHud;
    public float damage = 25f;

    public float timeBetFire = 0.12f;
    public float reloadTime = 1.8f;

    public float fireDistance = 50f;

    //public Animator animator;
    public AudioClip shootClip;

    public ParticleSystem shellEffect;
    public Transform firePos;

    public LineRenderer lineRenderer;
    private AudioSource audioSource;

    //public int ammoRemain; // ³²Àº ÃÑ¾Ë
    //public int magAmmo;    // ÅºÃ¢ Å©±â

    private float lastFireTime;

    private void Awake()
    {
        //lineRenderer = GetComponent<LineRenderer>();
        audioSource = GetComponent<AudioSource>();

        lineRenderer.enabled = false;
        lineRenderer.positionCount = 2;
    }

    private void OnEnable()
    {
        lastFireTime = 0f;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            Fire();
        }
    }

    private IEnumerator CoShotEffect(Vector3 hitPosition)
    {
        audioSource.PlayOneShot(shootClip);
        shellEffect.Play();
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, firePos.position);
        lineRenderer.SetPosition(1, hitPosition);

        yield return new WaitForSeconds(0.1f);

        lineRenderer.enabled = false;
    }

    public void Fire()
    {
        if ( Time.time > (lastFireTime + timeBetFire))
        {
            lastFireTime = Time.time;
            Shoot();
        }
    }

    private void Shoot()
    {
        Vector3 hitPosition = Vector3.zero;
        RaycastHit hit;

        if (Physics.Raycast(firePos.position, firePos.forward, out hit, fireDistance))
        {
            hitPosition = hit.point;

            var target = hit.collider.GetComponent<IDamagable>();
            if (target != null)
            {
                target.OnDamage(damage, hit.point, hit.normal);
            }
        }
        else
        {
            hitPosition = firePos.position + firePos.forward * fireDistance;
        }

        StartCoroutine(CoShotEffect(hitPosition));
    }
}
