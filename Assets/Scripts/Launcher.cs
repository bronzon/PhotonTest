using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : Photon.PunBehaviour {
	public string version = "1";
	public PhotonLogLevel logLevel = PhotonLogLevel.Informational;
	public string playerName;

	// Use this for initialization
	void Awake () {
		PhotonNetwork.autoJoinLobby = false;
		PhotonNetwork.automaticallySyncScene = true;
		PhotonNetwork.logLevel = logLevel;
		PhotonNetwork.playerName =  playerName + " ";
	}


	void Start() {
		Connect ();
	}

	void Connect () {
		PhotonNetwork.ConnectUsingSettings (version);
	}

	public override void OnConnectedToMaster () {
		Debug.Log ("joining random room");
		PhotonNetwork.JoinRandomRoom();
	}

	public override void OnDisconnectedFromPhoton()	{
		Debug.LogWarning("Disconnected from PUN");        
	}

	public override void OnPhotonRandomJoinFailed (object[] codeAndMsg) {
		PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 4 }, null);
	}

	public override void OnJoinedRoom () {
		Debug.Log ("joined room");
	}
}
