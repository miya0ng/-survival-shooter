using UnityEngine;

public class PlayerHealth : LivingEntity
{
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }
    protected override void Die()
    {
        base.Die();
        gameManager.GameOver();
        OnDeath += () => Debug.Log("Player Died");
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);
       //ebug.Log($"Player OnDamage: {damage}, Current Health: {Health}");
    }
}