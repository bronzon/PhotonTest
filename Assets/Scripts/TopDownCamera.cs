using UnityEngine;

public class TopDownCamera : MonoBehaviour {
	public Transform target;
	public float distanceFromTarget = 8f;

	void Start() {
		MoveToTarget();
	}

	void FixedUpdate() {
		if (target) {
			MoveToTarget();
		}
	}

	void MoveToTarget() {
		transform.position = new Vector3(target.position.x, distanceFromTarget, target.position.z);
	}
}
