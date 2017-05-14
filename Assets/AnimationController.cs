using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : Photon.MonoBehaviour {
	Animator animator;
	public float directionDampening = 0.25f;
	// Use this for initialization
	void Start () {
		this.animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!photonView.isMine && PhotonNetwork.connected) {
			return;
		}

		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

		if (v < 0) {
			v = 0;
		}
		print(h*h + v*v);
		animator.SetFloat ("Speed", h * h + v * v);
		animator.SetFloat ("Direction", h, directionDampening, Time.deltaTime);

		var animatorStateInfo = animator.GetCurrentAnimatorStateInfo (0);
		if (animatorStateInfo.IsTag ("run") && Input.GetButtonDown("Fire2")) {
			animator.SetTrigger ("Jump");
		}

	}
}
