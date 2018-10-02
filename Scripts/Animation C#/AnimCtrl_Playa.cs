using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCtrl_Playa : MonoBehaviour 
{
	PlayerStats pStats;
	PlayerCtrl playerCtrl;
	Animator _anim;

	public Component[] rbBones;

	public GameObject bloodPart;

	GameObject neck;

	// Use this for initialization
	void Start () 
	{
		playerCtrl = GetComponent<PlayerCtrl> ();	
		_anim = GetComponentInChildren<Animator> ();
		pStats = GetComponent<PlayerStats> ();

		neck = transform.Find("CC_scale/CrazyCock/Bn_Root/Bn_Pelvis/Bn_Chest/Bn_Neck_01").gameObject;

		rbBones = GetComponentsInChildren <Rigidbody>();
		foreach (Rigidbody rb in rbBones)
		{
			rb.isKinematic = true;
		}


	}

	public void PlayDeath (Vector3 killedByPos)
	{
		//_anim.SetBool ("Walk", false);

		StartCoroutine (WaitDie (killedByPos));

	}
	IEnumerator WaitDie(Vector3 killedByPos)
	{
		Random.InitState(System.DateTime.Now.Millisecond);
		int _rng = Random.Range (1, 3);
	
		yield return new WaitForSecondsRealtime (_rng * 0.1f);

		//Place In coroute wait partially for attack 
		Instantiate (bloodPart, neck.transform.position, Quaternion.identity, neck.transform);
		neck.transform.localScale = new Vector3 (0f, 0f, 0f);

		_anim.enabled = false;
		foreach (Rigidbody rb in rbBones)
		{
			rb.isKinematic = false;

			if (rb.transform.name == "Bn_Chest")
			{
				Vector3 dir = transform.position - killedByPos;

				Random.InitState(System.DateTime.Now.Millisecond);
				int rng =	Random.Range (0, 17);

				rb.AddForce (dir * (rng * 150));
			}
		}			


	}


	// Update is called once per frame
	void Update () 
	{
		if (playerCtrl.isMoving)
		{
			_anim.SetBool ("Walk", true);
		} else
		{
			_anim.SetBool ("Walk", false);
		}

		if (pStats.NRGized)
		{
			_anim.SetBool ("CrazyMode", true);
		} else
		{
			_anim.SetBool ("CrazyMode", false);
		}
	}


}
