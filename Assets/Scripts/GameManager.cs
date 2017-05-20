using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager  : Photon.PunBehaviour {
	public static GameManager INSTANCE;

	void Start()  {
		INSTANCE = this;
		StartCoroutine(SpawnNextFrame());
	}

	private IEnumerator SpawnNextFrame() {
		yield return new WaitForSeconds(4);
		NetworkInstantiator.INSTANCE.SpawnPlayer(new Vector3(0,-5, 0), Quaternion.identity);
	}

	public override void OnLeftRoom () {
		SceneManager.LoadScene ("Launcher");
	}

	public void QuitGame() {
		PhotonNetwork.LeaveRoom ();
	}

	public void SwitchLevel() {
		if (PhotonNetwork.isMasterClient) {
			int index = SceneManager.GetActiveScene ().buildIndex;
			SceneManager.LoadScene(index == 1 ? 2 : 1);
		}
	}

}
