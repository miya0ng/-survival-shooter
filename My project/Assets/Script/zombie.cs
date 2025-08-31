using System.Linq;
using System.Text;
using Unity.Android.Gradle;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class zombie : LivingEntity, IDamagable
{

    private static readonly int DieHash = Animator.StringToHash("Die");
    private static readonly int targetHash = Animator.StringToHash("HasTarget");

    NavMeshAgent agent;
    public Animator animator; 

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
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }
    private void Update()
    {
        target = FindTarget(traceDist);
        agent.SetDestination(target.position);
        AttackPlayer();

        Debug.Log($"zombie health: {Health}");
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
                Debug.Log("player OnDamage");
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

        if( Vector3.Distance(transform.position, target.transform.position) <= traceDist)
        {
            agent.isStopped = false;
            animator.SetBool(targetHash, true);
        }
        else
        {
            agent.isStopped = true;
            animator.SetBool(targetHash, false);
        }

        return target.transform;
    }

    protected override void Die()
    {
        base.Die();
        OnDeath += () => Debug.Log("zombie Died");
        agent.isStopped = true;
        animator.SetTrigger(DieHash);
        animator.SetBool(targetHash, false);
        Destroy(gameObject, 3f);
    }
    public void StartSinking()
    {
        //Destroy(gameObject, 3f);
    }
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);
        Debug.Log("zombie OnDamage");
        audioSource.PlayOneShot(zombieAttackClip);
        smog.transform.position = hitPoint;
        smog.transform.forward = hitNormal;
        smog.Play();
    }
}
