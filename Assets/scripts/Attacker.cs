using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
public class Attacker : MonoBehaviour
{

	//[Range (-1f, 1.5f)] public float walkSpeed;
	[Tooltip ("Average number of seconds between appearences")]
	public float seenEverySeconds;

	private float walkSpeed;
	private GameObject currentTarget;
	private Animator animator;

	void Start ()
	{
		animator = GetComponent<Animator> ();
	}

	
	// Update is called once per frame
	void Update ()
	{
		transform.Translate (Vector3.left * walkSpeed * Time.deltaTime);
		if (!currentTarget) {
			animator.SetBool ("isAttacking", false);
		}

	}

	void OnTriggerEnter2D ()
	{
		Debug.Log (name + "trigger eneter");
	}

	public void SetSpeed (float speed)
	{
		walkSpeed = speed;
	}

	public void StrikeCurrentTarget (float damage)
	{
		if (currentTarget) {
			Health health = currentTarget.GetComponent<Health> ();
			if (health) {
				health.DealDamage (damage);
			}
		}
	}

	public void Attack (GameObject obj)
	{
		currentTarget = obj;
	}
}
