using System.Collections.Generic;
using UnityEngine;

public class NetworkInstanceManagement : Photon.PunBehaviour {
    public static NetworkInstanceManagement INSTANCE;
    public Transform syncParent;
    public GameObject playerPrefab;
    public PlayerNetworkSync playerNetworkSyncPrefab;
    public PlayerManager localPlayer;

    public Dictionary<PhotonPlayer, PlayerNetworkSync> syncObjectByPlayer = new Dictionary<PhotonPlayer, PlayerNetworkSync>();

    void Awake() {
        INSTANCE = this;
        DontDestroyOnLoad(this);
    }

    public void SpawnPlayer(Vector3 pos, Quaternion rotation) {
        int photonViewId = PhotonNetwork.AllocateViewID();
        photonView.RPC("OnSpawnPlayer", PhotonTargets.AllBuffered, pos, rotation, photonViewId, PhotonNetwork.player);
    }

    [PunRPC]
    void OnSpawnPlayer(Vector3 pos, Quaternion rot, int photonViewId, PhotonPlayer photonPlayer) {
        var playerObject = Instantiate(playerPrefab);
        var playerNetworkSyncObject = Instantiate(playerNetworkSyncPrefab);
        playerNetworkSyncObject.photonView.viewID = photonViewId;
        playerNetworkSyncObject.playerObject = playerObject;

        if (photonPlayer.Equals(PhotonNetwork.player)) {
            playerObject.GetComponent<MeshRenderer>().material.color = Color.blue;
            localPlayer = playerObject.AddComponent<PlayerManager>();
            localPlayer.playerNetworkSync = playerNetworkSyncObject;
            playerNetworkSyncObject.transform.SetParent(syncParent);
            playerNetworkSyncObject.onlySyncOutGoing = true;
        }

        syncObjectByPlayer[photonPlayer] = playerNetworkSyncObject;
    }

    public void DespawnPlayer() {
        photonView.RPC("OnDespawnPlayer", PhotonNetwork.player);
        PhotonNetwork.LeaveRoom ();
    }

    [PunRPC]
    void OnDespawnPlayer(PhotonPlayer photonPlayer) {
        print("despawning " + photonPlayer.NickName);
        if (syncObjectByPlayer.ContainsKey(photonPlayer)) {
            var playerNetworkSync = syncObjectByPlayer[photonPlayer];
            var playerObject = playerNetworkSync.playerObject;

            syncObjectByPlayer.Remove(photonPlayer);

            Destroy(playerObject);
            Destroy(playerNetworkSync);

        }
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer) {
       OnDespawnPlayer(otherPlayer);
    }
}