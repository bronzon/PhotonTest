using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : Photon.PunBehaviour {
	public string version = "1";
	public PhotonLogLevel logLevel = PhotonLogLevel.Informational;
	public string playerName;
	bool connecting = false;

	// Use this for initialization
	void Awake () {
		PhotonNetwork.autoJoinLobby = false;
		PhotonNetwork.automaticallySyncScene = true;
		PhotonNetwork.logLevel = logLevel;
		PhotonNetwork.playerName =  playerName + " ";
	}


	void Start() {

	}

	public void Connect () {
		connecting = true;
		PhotonNetwork.ConnectUsingSettings (version);
	}

	public override void OnConnectedToMaster () {		
		if (connecting) {
			Debug.Log ("joining random room");
			connecting = false;
			PhotonNetwork.JoinRandomRoom ();
		}
	}

	public override void OnDisconnectedFromPhoton()	{
		Debug.LogWarning("Disconnected from PUN");        
	}

	public override void OnPhotonRandomJoinFailed (object[] codeAndMsg) {
		PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 4 }, null);
	}

	public override void OnJoinedRoom () {
		Debug.Log ("joined room");
		if (PhotonNetwork.room.PlayerCount == 1) {
			PhotonNetwork.LoadLevel ("Level1");
		}
	}
}
