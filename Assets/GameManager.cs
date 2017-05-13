using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager  : Photon.PunBehaviour {
	public override void OnLeftRoom () {
		SceneManager.LoadScene ("Launcher");
	}

	public void QuitGame() {
		PhotonNetwork.LeaveRoom ();
	}

	public void SwitchLevel() {
		int index = SceneManager.GetActiveScene ().buildIndex;
		if (index == 1) {
			SceneManager.LoadScene (2);
		} else {
			SceneManager.LoadScene (1);
		}
	}
}
