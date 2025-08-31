using System;
using UnityEngine;

public class PlayerHealth : LivingEntity
{
    private static readonly int DieHash = Animator.StringToHash("Die");
    public Animator animator;
    private GameManager gameManager;
    private Rigidbody rb;
    public uiHud ui;
    private void Awake()
    {
        ui.playerHpBar.value = 1;
        rb=GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }

    public void Update()
    {
        ui.playerHpBar.value = Health / MaxHealth;
    }
    protected override void Die()
    {
        base.Die();
        rb.isKinematic = true;
        gameManager.GameOver();
        animator.SetTrigger(DieHash);
        OnDeath += () => Debug.Log("Player Died");
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);
       //ebug.Log($"Player OnDamage: {damage}, Current Health: {Health}");
    }
}