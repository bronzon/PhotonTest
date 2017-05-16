using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownPlayerController : Photon.PunBehaviour {
    public float speed = 0.3f;
    private Rigidbody rigidbody;


    private void Start() {
        rigidbody = GetComponent<Rigidbody>();
        if (!photonView.isMine) {
            Destroy(this);
        }
    }

    void FixedUpdate() {
        var xVelocity = 0f;
        var zVelocity = 0f;

        if (Input.GetAxisRaw("Forwards") > 0) {
            zVelocity = speed;
        } else if (Input.GetAxisRaw("Backwards") > 0) {
            zVelocity = -speed;
        } else {
            zVelocity = 0;
        }

        if (Input.GetAxisRaw("Right") > 0) {
            xVelocity = speed;
        } else if (Input.GetAxisRaw("Left") > 0) {
            xVelocity = -speed;
        } else {
            xVelocity = 0;
        }

        rigidbody.velocity = new Vector3(xVelocity, 0, zVelocity);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float distance;

        if (plane.Raycast(ray, out distance)) {
            Vector3 target = ray.GetPoint(distance);
            Vector3 direction = target - transform.position;
            float rotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, rotation, 0);
        }
    }

    void OnCollisionEnter(Collision collision) {
        Debug.Log("collision");
    }


    public override void OnPhotonInstantiate(PhotonMessageInfo info) {
        print("someone instantiated");
        if (info.sender.Equals(PhotonNetwork.player)) {
            NetworkPlayers.INSTANCE.players.Add(info.photonView.gameObject);
            NetworkPlayers.INSTANCE.playerGameObjectByPlayer[info.sender] = gameObject;
        }
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer otherPlayer) {
        if (! NetworkPlayers.INSTANCE.playerGameObjectByPlayer.ContainsKey(otherPlayer)) {
            NetworkPlayers.INSTANCE.playerGameObjectByPlayer[otherPlayer] = null;
        }
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer) {
        if ( NetworkPlayers.INSTANCE.playerGameObjectByPlayer.ContainsKey(otherPlayer)) {
            NetworkPlayers.INSTANCE.players.Remove( NetworkPlayers.INSTANCE.playerGameObjectByPlayer[otherPlayer]);
            NetworkPlayers.INSTANCE.playerGameObjectByPlayer.Remove(otherPlayer);
        }
    }
}