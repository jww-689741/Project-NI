using UnityEngine;
using System.Collections;

using UnityEditor;

public class Welcome : EditorWindow
{
	public const string mezanixWelcomeWindowPlayerPrefsKey = "mezanixWelcomeWindowPlayerPrefsKey";

	static Welcome welcome;



	string titleWelcome = "Welcome To Mezanix Products";

	string productAbstract = "Mezanix Fx Free 1.1 \n" +
		"More than Fx set, it is also a bunch of utilities, \n" +
		"usefule for this package and also for your projects. \n" +
		"\n";

	string mezanixFxProInvitText = "Check out Mezanix Fx Pro with more of amazing Fx and Tools";

	string mezanixFxProUrl = "https://www.assetstore.unity3d.com/en/#!/content/64488";


	static string mezanixFolderName = "Assets/Mezanix/";

	static string productFolderName = "MezanixFX_Free_1_1/";

	static string mezanixLogoFolderName = "0_0_MezanixLogo/";

	static string mezanixLogoName = "RedBgLogoMezanix.png";

	static string mezanixLogoPath = "";


	static Texture mezanixLogoTexture = null;


	string inviteText = "Want to know more about Mezanix, subscribe to email-list, or contact";

	string buttonWelcomeToMezanixText = "You Are Welcome";

	string mezanixUrl = "http://mezanix.com/";


	public static void Init ()
	{
		//Debug.Log("loaded");

		mezanixLogoPath = 
			mezanixFolderName +
			productFolderName +
			mezanixLogoFolderName +
			mezanixLogoName;

		//Debug.Log(mezanixLogoPath);


		mezanixLogoTexture = AssetDatabase.LoadAssetAtPath
			(mezanixLogoPath, typeof(Texture)) as Texture;

		if(mezanixLogoTexture == null)
		{
			Debug.LogWarning ("Faild to load mezanixLogoTexture");

			return;
		}



		welcome = (Welcome)EditorWindow.GetWindow (typeof(Welcome));

		welcome.position = new Rect(256f, 64f, 512f, 512f);

		welcome.ShowUtility ();		
	}

	void OnGUI ()
	{
		EditorGUI.DrawPreviewTexture (new Rect (0f, 0f, mezanixLogoTexture.width, mezanixLogoTexture.height), mezanixLogoTexture);


		GUILayout.Space (mezanixLogoTexture.height + 20f); 

		GUILayout.Label (titleWelcome, EditorStyles.boldLabel);


		GUILayout.Space (10f);

		GUILayout.Label (productAbstract);




		GUILayout.Space (10f);

		if(GUILayout.Button (mezanixFxProInvitText))
			Application.OpenURL (mezanixFxProUrl);



		GUILayout.Space (10f);

		GUILayout.Label (inviteText, EditorStyles.boldLabel);

		if(GUILayout.Button (buttonWelcomeToMezanixText))
		{
			GoToMezanixWebsite ();

			welcome.Close ();
		}
	}
	
	void GoToMezanixWebsite ()
	{
		Application.OpenURL (mezanixUrl);
		
		PlayerPrefs.SetInt (mezanixWelcomeWindowPlayerPrefsKey, 1);
	}
}
