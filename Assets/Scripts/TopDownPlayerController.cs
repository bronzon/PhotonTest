﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownPlayerController : Photon.MonoBehaviour {
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
}