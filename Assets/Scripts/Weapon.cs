// TIE-21106 Software Engineering Methodology, 2015
// Roni Jaakkola, Marko Tuominen, Jaakko Husso, Noora Männikkö, 2015

using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
	private bool state;

	void Start ()
	{
		state = true;
	}
	
	// Do things when collected
	public virtual void collect()
	{
		renderer.enabled = false;
		collider2D.enabled = false;
	}

	// Do things when used
	public virtual void useWeapon(Vector3 startPoint, Vector3 dir)
	{
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name == "Player") {
			Debug.Log ("Player collected a weapon!");
			collect ();
			state = false;
		}
	}

	public void Reset()
	{
		if (!state)
		{
			renderer.enabled = true;
			collider2D.enabled = true;
			state = true;
		}
	}

	public virtual int shotsLeft()
	{
		return 0;
	}

	public virtual bool isShooting(){
		return false;
	}
}

