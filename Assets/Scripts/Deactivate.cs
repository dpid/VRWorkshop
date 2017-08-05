using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple helper class that deactivates the game object when it awakens.
/// </summary>
public class Deactivate : MonoBehaviour
{
    private void Awake()
    {
        this.gameObject.SetActive(false);
    }
}
