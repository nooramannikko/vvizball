﻿// TIE-21106 Software Engineering Methodology, 2015
// Roni Jaakkola, Marko Tuominen, Jaakko Husso, Noora Männikkö, 2015

using UnityEngine;
using System.Collections;

public class MovingPlatforms : MonoBehaviour {

	public Transform startPoint;
	public Transform endPoint;
	public Transform platform;

	public float platformSpeed;

	public bool gravity;
	
	Vector3 direction;
	Transform destination;

	void Start ()
	{
		SetDestination(startPoint);
	}

	void FixedUpdate()
	{
		if (gravity == false)
		{
			platform.rigidbody2D.MovePosition(platform.position + direction * platformSpeed * Time.fixedDeltaTime);

			if (Vector3.Distance (platform.position, destination.position) < platformSpeed * Time.fixedDeltaTime)
			{
				SetDestination (destination == startPoint ? endPoint : startPoint);
			}
		}
		else
		{
			if (Vector3.Distance (platform.position, destination.position) > platformSpeed * Time.fixedDeltaTime)
			{
				platform.rigidbody2D.MovePosition(platform.position + direction * platformSpeed * Time.fixedDeltaTime);
			}
		}
	}
	
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawLine (startPoint.position, endPoint.position);
	}

	void SetDestination(Transform dest)
	{
		destination = dest;
		direction = (destination.position - platform.position).normalized;
	}

	public void GravityChange()
	{
		SetDestination (destination == startPoint ? endPoint : startPoint);
	}
}
