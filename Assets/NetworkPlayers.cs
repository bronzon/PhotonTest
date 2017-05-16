using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayers : Photon.PunBehaviour {
    public List<GameObject> players = new List<GameObject>();
    public Dictionary<PhotonPlayer, GameObject> playerGameObjectByPlayer = new Dictionary<PhotonPlayer, GameObject>();
    public static NetworkPlayers INSTANCE;

    void Awake() {
        DontDestroyOnLoad(this);
    }

    void Start() {
        INSTANCE = this;
    }

}