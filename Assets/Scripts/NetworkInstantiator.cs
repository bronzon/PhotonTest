using UnityEngine;

public class NetworkInstantiator : Photon.PunBehaviour {
    public static NetworkInstantiator INSTANCE;
    public Transform syncParent;
    public GameObject playerPrefab;
    public PlayerNetworkSync playerNetworkSyncPrefab;

    private PlayerManager playerManager;


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
            playerManager = playerObject.AddComponent<PlayerManager>();
            playerManager.playerNetworkSync = playerNetworkSyncObject;
            playerNetworkSyncObject.transform.SetParent(syncParent);
            playerNetworkSyncObject.onlySyncOutGoing = true;
        }
    }
}