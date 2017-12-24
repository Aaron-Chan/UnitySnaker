using System.Collections;
using System.Collections.Generic;
using SGF.UI.Framework;
using UnityEngine;
using OT.Foundation;
namespace SGF.UI.Framework.Example
{
	public class UIPage1 : UIPage
	{
		protected override void OnOpen(object arg = null)
		{
			base.OnOpen(arg);

		}

		public void OnPage2BtnClick()
		{
			UIManager.Instance.OpenPage("UIPage2");
		}
	}
}