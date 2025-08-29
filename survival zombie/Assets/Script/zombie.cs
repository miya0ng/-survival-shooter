using System.Linq;
using System.Text;
using Unity.Android.Gradle;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class zombie : LivingEntity, IDamagable
{
    NavMeshAgent agent;
    public LayerMask targetLayer;
    private float traceDist = 100f;
    private float attackDist = 2f;

    public GameObject player;
    public Transform target;

    private float lastAttackTime;
    private float attackDelay;
    private float damage = 10;

    private AudioSource audioSource;
    public AudioClip zombieAttackClip;
    public ParticleSystem smog;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        target = FindTarget(traceDist);
        agent.SetDestination(target.position);
    }

    protected void AttackPlayer()
    {
        if (target == null || Vector3.Distance(transform.position, target.position) > attackDist)
        {
            return;
        }

        var lookPos = target.position;
        lookPos.y = transform.position.y;
        transform.LookAt(lookPos);

        if (Time.time - lastAttackTime > attackDelay)
        {
            lastAttackTime = Time.time;
            var damageable = target.GetComponent<IDamagable>();
            if (damageable != null)
            {
                damageable.OnDamage(damage, transform.position, -transform.forward);
            }
        }
    }

    protected Transform FindTarget(float radius)
    {
        var colliders = Physics.OverlapSphere(transform.position, radius, targetLayer.value);
        if (colliders.Length == 0)
        {
            return null;
        }

        var target = colliders.OrderBy(x => Vector3.Distance(x.transform.position, transform.position)).First();
        return target.transform;
    }

    protected void Die()
    {
        
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);
        audioSource.PlayOneShot(zombieAttackClip);
        smog.transform.position = hitPoint;
        smog.transform.forward = hitNormal;
        smog.Play();
    }
}
