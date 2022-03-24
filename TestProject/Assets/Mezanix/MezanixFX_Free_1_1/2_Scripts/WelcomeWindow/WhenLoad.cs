using UnityEngine;
using System.Collections;

using UnityEditor;


[InitializeOnLoad]

public class WhenLoad
{
	static int n = -1;

	static int showWelcomeWindowFrame = 500;

	static WhenLoad ()
	{
		//PlayerPrefs.SetInt (Welcome.mezanixWelcomeWindowPlayerPrefsKey, 0);

		if(PlayerPrefs.GetInt (Welcome.mezanixWelcomeWindowPlayerPrefsKey, 0) == 1)
			return;			

		n = -1;

		EditorApplication.update += Update;
	}


	static void Update ()
	{
		if(n == showWelcomeWindowFrame)
			return;

		n++;

		if(n == showWelcomeWindowFrame)
			Welcome.Init ();
	}
}
