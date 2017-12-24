using OT.Foundation;

namespace SGF.UI.Framework.Example
{

	public class UIPage2:UIPage
	{

		public void OnOpenWindowClick()
		{
			UIManager.Instance.OpenWindow("UIWindow1").onClose+= OnWindows1Close;
		}

		public void OnWindows1Close(object arg=null)
		{
			this.Log("OnWindows1Close()");
		}

		public void OnOpenWidgetClick()
		{
			UIManager.Instance.OpenWidget("UIWidget1");
		}
	}
}