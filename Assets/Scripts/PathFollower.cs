using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// The game object will move along the child transform positions of a path object.
/// </summary>
public class PathFollower : MonoBehaviour {

	public Transform path;

	public float moveSpeed = 0.5f;
	public float turnSpeed = 1.0f;
	public float hopStrength = 0.05f;

    public bool isGrounded = true;
    public bool isLooping = false;

	public UnityEvent OnStarted;
	public UnityEvent OnFinished;
    public UnityEvent OnHop;

	private int pathNodeIndex = -1;
	private Transform pathNode;

	private float hopDamper = 0.1f;
	private float hopOffset = 0.0f;
	private float hopPositionOffset = 0.0f;

	private Vector3 lastMovePosition;

	void Awake()
	{
		StartPath(path);
	}

	void Update()
	{
		if (pathNode != null)
		{
			Turn();
			Move();
		}

		Hop();
	}

	public void RestartPath()
	{
		StartPath(path);
	}

	public void StartPath(Transform path)
	{
		this.path = path;

		if (path != null)
		{
			pathNodeIndex = -1;
			SetNextPathNode();

			if (pathNode != null)
			{
				transform.position = pathNode.position;
                transform.rotation = pathNode.rotation;
				SetNextPathNode();
                OnStarted.Invoke();
			}
		}

	}

	private void SetNextPathNode()
	{
		pathNodeIndex += 1;
        pathNode = null;

        if (path.childCount > 0)
        {
			if (pathNodeIndex < path.childCount)
			{
				pathNode = path.GetChild(pathNodeIndex);
			}
			else if (isLooping)
			{
				pathNodeIndex = 0;
				pathNode = path.GetChild(pathNodeIndex);
			}
            else
            {
                OnFinished.Invoke();    
            }
        }	
	}

	private void Turn()
	{
        if (turnSpeed > 0.0f)
        {
			float turnStep = turnSpeed * Time.deltaTime;
			Vector3 direction = pathNode.position - transform.position;
			direction.y = 0;
			Quaternion targetRotation = Quaternion.LookRotation(direction);
			transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, turnStep); 
        }
	}

	private void Move()
	{
        if (moveSpeed > 0.0f)
        {
			Vector3 transformPosition = transform.position;
			Vector3 pathNodePosition = pathNode.position;

			if (isGrounded)
			{
				transformPosition.y = 0;
				pathNodePosition.y = 0;
			}

			float step = moveSpeed * Time.deltaTime;
			Vector3 position = Vector3.MoveTowards(transformPosition, pathNodePosition, step);

			if (isGrounded)
			{
				Vector3 raycastPosition = position + Vector3.up;
				RaycastHit hit;

				if (Physics.Raycast(raycastPosition, Vector3.down, out hit))
				{
					position.y = hit.point.y;
				}
			}

			transform.position = position;
			lastMovePosition = position;

			float distance = Vector3.Distance(transformPosition, pathNodePosition);
			if (distance <= step)
			{
				SetNextPathNode();
			}   
        }		
	}

	private void Hop()
	{
        if (hopStrength > 0.0f)
        {
			if (hopPositionOffset <= 0.0f)
			{
				hopOffset = hopStrength;
                OnHop.Invoke();
			}
			else
			{
				hopOffset -= hopStrength * hopDamper;
			}

			hopPositionOffset += hopOffset;

			Vector3 hopPosition = lastMovePosition;
			hopPosition.y += hopPositionOffset;

			transform.position = hopPosition;
        }	
    }

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

	public void SetTurnSpeed(float speed)
	{
        turnSpeed = speed;
	}

	public void SetHopStrength(float strength)
	{
        hopStrength = strength;
	}
}
