using System;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(Launcher))]
public class LauncherEditor : Editor {

	public override void OnInspectorGUI () {
		base.OnInspectorGUI ();
		if (GUILayout.Button ("Connect")) {
			Launcher launcher = target as Launcher;
			launcher.Connect ();
		}
	}
}



