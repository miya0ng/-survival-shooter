using UnityEngine;

public class PlayerHealth : LivingEntity
{
    protected override void Die()
    {
        base.Die();
        OnDeath += () => Debug.Log("Player Died");
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);
       //ebug.Log($"Player OnDamage: {damage}, Current Health: {Health}");
    }
}