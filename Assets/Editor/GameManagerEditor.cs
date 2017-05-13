using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor {

	public override void OnInspectorGUI () {
		base.OnInspectorGUI ();
		if (GUILayout.Button ("Quit")) {
			GameManager manager = target as GameManager;
			manager.QuitGame ();
		}
	}
}
