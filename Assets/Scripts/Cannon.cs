using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cannon shoots out a cannonball instance.
/// </summary>
public class Cannon : MonoBehaviour
{
    public CannonballPool cannonballPool;
    public float strength;

    public void Fire()
    {
        if (isActiveAndEnabled)
        {
			Cannonball cannonball = cannonballPool.GetCannonball();

			if (cannonball != null)
			{
				cannonball.gameObject.SetActive(true);
				cannonball.transform.position = transform.position;
				cannonball.transform.localRotation = Quaternion.identity;

				Vector3 force = transform.forward * strength;
                Vector3 torque = new Vector3(Random.Range(1.0f, 10.0f), Random.Range(1.0f, 10.0f), Random.Range(1.0f, 10.0f));

                Rigidbody cannonballRigidbody = cannonball.GetComponent<Rigidbody>();
				cannonballRigidbody.AddForce(force, ForceMode.Impulse);
                cannonballRigidbody.AddTorque(torque);
			}
        }
    }
}
