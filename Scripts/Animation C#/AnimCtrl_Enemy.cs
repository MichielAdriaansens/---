using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor.AI;
using UnityEngine.AI;

public class AnimCtrl_Enemy : MonoBehaviour 
{
	Move02 _Move;
	Animator _Anim;

	public GameObject Leg1;
	public GameObject Leg2;
	public GameObject Spine;
	public GameObject Head;
	public GameObject Jaw;

	public GameObject leg1FX;
	public GameObject leg2FX;
	public GameObject SpineFX;
	public GameObject HeadFX;
	public GameObject JawFX;



	// Use this for initialization
	void Start () 
	{
		_Move =  this.GetComponent<Move02>();
		_Anim = this.GetComponentInChildren<Animator> ();	


		//place Limbs manually
		#region CheckManualLimbs
		if(Leg1 == null)
		{
			print("Assign object to leg1");
		}

		if(Leg2 == null)
		{
			print("Assign object to leg2");
		}

		if(Spine == null)
		{
			print("Assign object to Spine");
		}

		if(Head == null)
		{
			print("Assign object to Head");
		}

		if(Jaw == null)
		{
			print("Assign object to Jaw");
		}
		//FX
		if(leg1FX == null)
		{
			print("Assign object to leg1FX");
		}

		if(leg2FX == null)
		{
			print("Assign object to leg2FX");
		}

		if(SpineFX == null)
		{
			print("Assign object to SpineFX");
		}

		if(HeadFX == null)
		{
			print("Assign object to HeadFX");
		}

		if(JawFX == null)
		{
			print("Assign object to JawFX");
		}

		#endregion
	}

	public void Attack()
	{
		_Anim.SetBool ("Attack", true);
	}
	//
	public void DeathAnim()
	{
		_Anim.enabled = false;	
		Explode ();
	}

	void Explode()
	{	//instantiate shit
		//addforce



		SpawnLimbs(leg1FX, Leg1);
		SpawnLimbs(leg2FX, Leg2);
		SpawnLimbs(SpineFX, Spine);
		SpawnLimbs(HeadFX, Head);
		SpawnLimbs(JawFX, Jaw);

		Vector3 explosionPos = this.transform.position;
		Collider[] getBlown = Physics.OverlapSphere(explosionPos,1f);

		Random.InitState(System.DateTime.Now.Millisecond);
		int rng = Random.Range (1,7);
	//	print (rng);
		foreach(Collider hit in getBlown)
		{
			if (hit.GetComponent<Rigidbody> () != null)
			{

				Rigidbody rb = hit.GetComponent<Rigidbody> ();
				rb.AddExplosionForce (rng * 100, explosionPos, 1.5f, 2f);
			}
		}

	}

	void SpawnLimbs(GameObject limb, GameObject limbPos)
	{
		Instantiate (limb,limbPos.transform.position,limbPos.transform.rotation);
	}

	void Walk()
	{
		if (_Move.isMoving)
			{
				_Anim.SetBool ("Walk", true);
			}
			else
			{
				_Anim.SetBool ("Walk", false);
			}	
	}


	// Update is called once per frame
	void Update () 
	{
		Walk ();
	}
}
