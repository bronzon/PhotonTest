using System.Collections;
using System.Collections.Generic;
using ExitGames.Demos.DemoAnimator;
using UnityEngine;

public class FollowIfLocal : Photon.MonoBehaviour {
	void Start() {

		if (photonView.isMine || !PhotonNetwork.connected ||PhotonNetwork.offlineMode) {
			var topDownCamera = Camera.main.gameObject.AddComponent<TopDownCamera>();
			topDownCamera.target = transform;
			topDownCamera.distanceFromTarget = 15;
		}
	}

}
