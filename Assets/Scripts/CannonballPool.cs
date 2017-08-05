using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CannonballPool instantiates a pool of cannonballs that can be reused.
/// </summary>
public class CannonballPool : MonoBehaviour {

    public Cannonball cannonballPrefab;
    public int instanceCount = 10;

    private Cannonball[] cannonballs;

    private void Awake()
    {
        cannonballs = new Cannonball[instanceCount];

        for (int i = 0; i < instanceCount; i++)
        {
            Cannonball cannonball = Instantiate(cannonballPrefab) as Cannonball;
            cannonball.transform.parent = transform;
            cannonball.gameObject.SetActive(false);

            cannonballs[i] = cannonball;
        }
    }

    public Cannonball GetCannonball()
    {
        Cannonball foundCannonball = null;
        foreach(Cannonball cannonball in cannonballs)
        {
            if (cannonball.isActiveAndEnabled == false)
            {
                foundCannonball = cannonball;
                break;
            }
        }

        return foundCannonball;
    }

}
