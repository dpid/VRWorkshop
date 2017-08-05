using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Cannonball makes an explosion when it enters a collider.
/// Any enemies within the explosion radius will be killed.
/// </summary>
public class Cannonball : MonoBehaviour
{

    public ParticleSystem explosionPrefab;

    public float explosionRadius = 0.5f;
	public float explosionForce = 5.0f;
	public float explosionUpwardForce = 1.0f;

    public UnityEvent OnExplode;

    private new Rigidbody rigidbody;
    private new Renderer renderer;
    private Vector3 initialScale;

    private ParticleSystem explosion;

    private float lifetime = 5.0f;
    private float explosionDisableDelay = 0.5f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        renderer = GetComponent<Renderer>();
        initialScale = transform.localScale;

        explosion = Instantiate(explosionPrefab) as ParticleSystem;
        explosion.Stop();
    }

    private void OnEnable()
    {
        Show();
        Invoke("Disable", lifetime); // disables the cannonball in case it never hits anything
    }

    private void OnDisable()
    {
        Hide();
        CancelInvoke();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Hide();
        Invoke("Disable", explosionDisableDelay);
        MakeExplosion();    
    }

    private void Show()
    {
        rigidbody.isKinematic = false;
        renderer.enabled = true;
	}

    private void Hide()
    {
        rigidbody.isKinematic = true;
        renderer.enabled = false;
	}

    private void Disable()
    {
        gameObject.SetActive(false);
    }

    private void MakeExplosion()
    {
        MakeParticleSystem();
        AddExplosionForce();
        OnExplode.Invoke();
    }

    private void MakeParticleSystem()
    {
        if (explosion != null)
        {
            explosion.transform.position = transform.position;
            explosion.Play();
        }
    }

    private void AddExplosionForce()
	{

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
		foreach (Collider collider in colliders)
		{
			Enemy enemy = collider.gameObject.GetComponentInParent<Enemy>();
			if (enemy != null)
			{
                enemy.Die();
			}

			Rigidbody rigidBody = collider.GetComponentInParent<Rigidbody>();
			if (rigidBody != null)
			{
				rigidBody.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpwardForce, ForceMode.Impulse);
			}
		}
	}

}
