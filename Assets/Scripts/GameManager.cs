using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager  : Photon.PunBehaviour {
	public static GameManager INSTANCE;
	public GameObject playerPrefab;
	public GameObject playerInstance;

	void Start()  {
		INSTANCE = this;
		playerInstance = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0f, 0.5f, 0f), Quaternion.identity, 0);
	}

	public override void OnLeftRoom () {
		SceneManager.LoadScene ("Launcher");
	}

	public void QuitGame() {
		PhotonNetwork.Destroy(playerInstance);
		PhotonNetwork.LeaveRoom ();
	}

	public void SwitchLevel() {
		if (PhotonNetwork.isMasterClient) {
			int index = SceneManager.GetActiveScene ().buildIndex;
			SceneManager.LoadScene(index == 1 ? 2 : 1);
		}
	}

	public override void OnPhotonPlayerConnected (PhotonPlayer newPlayer) {
		Debug.Log( "OnPhotonPlayerConnected() " + newPlayer.NickName );
		if (PhotonNetwork.isMasterClient) {
			//blabla
		}
	}

	public override void OnPhotonPlayerDisconnected (PhotonPlayer otherPlayer) {
		Debug.Log( "OnPhotonPlayerConnected() " + otherPlayer.NickName );
		//blabla
	}

}
