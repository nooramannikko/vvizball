// TIE-21106 Software Engineering Methodology, 2015
// Roni Jaakkola, Marko Tuominen, Jaakko Husso, Noora Männikkö, 2015

using UnityEngine;
using System.Collections;

public class LaserShot : MonoBehaviour
{
	float speed;
	float range;
	public Vector3 start;

	public Vector3 direction;


	// Use this for initialization
	void Start ()
	{
		speed = 15f;
		range = 20f;
		//start = transform.position;
		direction = Vector3.left;
		renderer.enabled = false;
		collider2D.enabled = false;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		transform.Translate (new Vector3(direction.x * speed * Time.fixedDeltaTime,0,0), Space.World);
		if (Vector3.Distance (start, transform.position) >= range) {
			renderer.enabled = false;
			collider2D.enabled = false;
		}
		Collider2D[] colliders = Physics2D.OverlapPointAll (new Vector2 (transform.position.x, transform.position.y));
		for (var i = 0; i < colliders.Length; i++) {
			string tag = colliders[i].gameObject.tag;
			if (tag == "Platform" || tag == "Damage" || tag == "GravityLock")
			{
				renderer.enabled = false;
				collider2D.enabled = false;
				break;
			}
		}

	}

	public void setPosition(Vector3 begin, Vector3 dir)
	{
		//Debug.Log ("Set beam position");
		start = begin;
		direction = dir;
		transform.position = start;
		renderer.enabled = true;
		collider2D.enabled = true;
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		// Destroy if the shot hits a platfor of an enemy
		bool hit = false;
		if (collider.tag == "Enemy") {
			Debug.Log ("Shot hit an enemy!");
			//renderer.enabled = false;
			//collider2D.enabled = false;
			hit = true;
		}
		if (hit) {
			renderer.enabled = false;
			collider2D.enabled = false;
		}
	}

	public bool isShooting(){
		return renderer.enabled;
	}

}

