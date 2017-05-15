using System.Collections;
using UnityEngine;

public class PlayerVisibilityController : Photon.PunBehaviour {

	void Start () {
		if (!photonView.isMine) {
			Destroy(this);
		} else {
			StartCoroutine(VisibilityCheck());
		}
	}

	private IEnumerator VisibilityCheck() {
		while (true) {
			var players = GameObject.FindGameObjectsWithTag("Player");
			foreach (var player in players) {
				if (player.gameObject == gameObject) {
					continue;
				}
				Vector3 direction = player.transform.position - transform.position;
				direction.Normalize();
				RaycastHit raycastHit;
				if (Physics.Raycast(transform.position, direction, out raycastHit, float.MaxValue)) {
					player.GetComponent<MeshRenderer>().enabled =! raycastHit.collider.CompareTag("wall");
				} else {
					player.GetComponent<MeshRenderer>().enabled = true;
				}

				Debug.DrawRay(transform.position, direction, Color.cyan, 10);
			}
			yield return new WaitForSeconds(0.1f);
		}
	}



}
