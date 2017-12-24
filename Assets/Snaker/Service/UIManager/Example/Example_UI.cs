using System.Collections;
using System.Collections.Generic;
using OT.Foundation;
using SGF.UI.Framework;
using UnityEngine;

public class Example_UI  {

	// Use this for initialization
	public void Init ()
	{
		Debuger.EnableLog = true;
		UIManager.MainPage = "UIPage1";
		UIManager.Instance.Init("ui/Example/");
		UIManager.Instance.EnterMainPage();
	}
	
	
}
