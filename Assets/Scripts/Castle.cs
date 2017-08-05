using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Castle takes damage and invokes an OnDeath event when health is zero.
/// It also uses a TransformRestorer to allow the Castle to rebuild. 
/// </summary>
public class Castle : MonoBehaviour
{
    public int health = 10;
    public Flagpole flagpole;

    public UnityEvent OnDeath;

    public bool isDead
    {
        get
        {
            return health <= 0;    
        }
    }

    private int initialHealth;
    private TransformRestorer transformRestorer;

    private void Start()
    {
        InitHealth();
        InitTransformRestorer();
    }

	private void InitTransformRestorer()
	{
		transformRestorer = gameObject.AddComponent<TransformRestorer>();
	}

    private void SetIsKinematic(bool isKinemenatic)
    {
        Rigidbody[] rigidbodyChildren = GetComponentsInChildren<Rigidbody>();
        foreach(Rigidbody childRigidbody in rigidbodyChildren)
        {
            childRigidbody.isKinematic = isKinemenatic;
        }
    }

    private void InitHealth()
    {
		initialHealth = health;
        SetHealth(health);
    }

    public void AddHealth(int value)
    {
        health += value;
        SetFlagpoleRaisedPercentage();
    }

    public void SetHealth(int value)
    {
        health = value;
        SetFlagpoleRaisedPercentage();
    }

	private void SetFlagpoleRaisedPercentage()
	{
        if (flagpole != null)
        {
            float percentage = health * 1.0f / initialHealth;
            flagpole.SetFlagRaisedPercentage(percentage);
        }	
	}

    public void Restore()
    {
        SetIsKinematic(false);
        transformRestorer.Restore();
        SetIsKinematic(true);

        SetHealth(initialHealth);

        if (flagpole != null)
        {
			flagpole.SetFlagToTop();
			flagpole.enabled = true;  
        }

    }

    public void TakeDamage(int damage = 1)
    {
        if (health > 0)
        {
            AddHealth(-damage);

			if (health <= 0)
			{
				Die();
			}
        }
    }

    public void Die()
    {
        health = 0;

        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach(Rigidbody childRigidbody in rigidbodies){
            childRigidbody.isKinematic = false;
            childRigidbody.AddExplosionForce(5.0f, transform.position, 5.0f, 1.0f, ForceMode.Impulse);
        }

        if (flagpole != null)
        {
			flagpole.SetFlagToBottom();
			flagpole.enabled = false; 
        }

        OnDeath.Invoke();
    }
	
}
