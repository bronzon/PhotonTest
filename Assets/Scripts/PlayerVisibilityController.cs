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

			foreach (var player in NetworkPlayers.INSTANCE.players) {
				if (player == gameObject) {
					continue;
				}
				Vector3 direction = player.transform.position - transform.position;
				direction.Normalize();
				RaycastHit raycastHit;

				player.GetComponent<MeshRenderer>().enabled = !(Physics.Raycast(transform.position, direction, out raycastHit, float.MaxValue) && raycastHit.transform.CompareTag("wall"));

			}
			yield return new WaitForSeconds(0.1f);
		}
	}
}
