using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Flagpole animates the raising and lowering of a flag to a given percentage of height.
/// </summary>
public class Flagpole : MonoBehaviour {

    public float flagSpeed = 0.1f;

    public Transform flag;
    public Transform poleTop;
    public Transform poleBottom;

    private Vector3 targetFlagPosition;

    private void Start()
    {
        targetFlagPosition = flag.position;
    }

	private void Update()
	{
        UpdateFlagPosition();
	}

    private void UpdateFlagPosition()
    {
		if (targetFlagPosition != flag.position)
		{
			float step = flagSpeed * Time.deltaTime;
			flag.position = Vector3.Lerp(flag.position, targetFlagPosition, step);
		}
    }

	public void SetFlagRaisedPercentage(float percentage)
    {
        targetFlagPosition = Vector3.Lerp(poleBottom.position, poleTop.position, percentage);
    }

	public void SetFlagToTop()
	{
		flag.position = poleTop.position;
	}

	public void SetFlagToBottom()
	{
		flag.position = poleBottom.position;
	}


}
