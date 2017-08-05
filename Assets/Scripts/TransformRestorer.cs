using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Restores the transform hierarchy when the Restore() method is called.
/// </summary>
public class TransformRestorer : MonoBehaviour {

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 initialScale;
    private Transform[] initialChildren;

    private void Awake()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        initialScale = transform.localScale;
        initialChildren = new Transform[transform.childCount];
        for (int i = 0; i < initialChildren.Length; i++)
        {
            Transform child = transform.GetChild(i);
            TransformRestorer childTransformRestorer = child.GetComponent<TransformRestorer>();
            if (childTransformRestorer == null)
            {
                child.gameObject.AddComponent<TransformRestorer>();
            }

            initialChildren[i] = child;
        }
    }

    public void Restore()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        transform.localScale = initialScale;

        if(initialChildren != null)
        {
			foreach (Transform child in initialChildren)
			{
				if (child != null && child != transform)
				{
					child.parent = transform;
					TransformRestorer childTransformRestorer = child.GetComponent<TransformRestorer>();
					if (childTransformRestorer != null)
					{
						childTransformRestorer.Restore();
					}
				}
			}
        }

    }

}
