using System.Runtime.Remoting.Messaging;
using UnityEngine;

[RequireComponent(typeof(PhotonTransformView), typeof(PhotonView))]
public class PlayerNetworkSync : Photon.MonoBehaviour {
	public GameObject playerObject;
	public bool onlySyncOutGoing = false;


	void Update () {
		if (onlySyncOutGoing) {
			return;
		}
		playerObject.transform.rotation = transform.rotation;
		playerObject.transform.position = transform.position;
	}

	public void UpdateRemote() {
		transform.rotation = playerObject.transform.rotation;
		transform.position = playerObject.transform.position;
	}
}
