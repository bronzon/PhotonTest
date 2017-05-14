using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Demos.DemoAnimator;

public class PlayerController : Photon.PunBehaviour, IPunObservable {

	public GameObject beams;
	public float health = 1.0f;

	bool isFiring;

	void Awake() {		
		beams.SetActive(false);
	}

	void Start() {
		CameraWork cameraWork = GetComponent<CameraWork> ();
		if (photonView.isMine || !PhotonNetwork.connected) {
			cameraWork.OnStartFollowing ();
		}
	}

	void OnTriggerEnter(Collider other) {
		if (!photonView.isMine) {
			return;
		}

		if (other.tag == "beam") {
			health -= 0.1f;
		}
	}

	void OnTriggerStay(Collider other) {
		if (!photonView.isMine) {
			return;
		}

		if (other.tag == "beam") {
			health -= 0.1f * Time.deltaTime;
		}
	}


	void Update() {
		if (photonView.isMine) {
			ProcessInputs ();
		}


		if (isFiring != beams.GetActive ()) {
			beams.SetActive(isFiring);
		}

		if (health <= 0) {
			GameManager.INSTANCE.QuitGame ();
		}
	}

	public void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info) {
		if (stream.isWriting) {
			stream.SendNext (isFiring);
			stream.SendNext (health);
		} else {
			isFiring = (bool)stream.ReceiveNext ();
			health = (float)stream.ReceiveNext ();
		}
	}

	void ProcessInputs() {

		if (Input.GetButtonDown ("Fire1") ) {
			if (!isFiring) {
				isFiring = true;
			}
		}

		if (Input.GetButtonUp ("Fire1") ) {
			if (isFiring) {
				isFiring = false;
			}
		}
	}

}