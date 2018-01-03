using SGF.Module.Framework;
using SGF.UI.Framework;
using Snaker.Service.User;
using Snaker.Service.UserManager.Data;

namespace Snaker.Module
{
	public class LoginModule : BusinessModule
	{
		protected override void Show(object arg)
		{
			UIManager.Instance.OpenPage(UIDef.UILoginPage);
		}


		public void Login(uint id, string name, string pwd)
		{
			UserData ud = new UserData();
			ud.id = (uint) id;
			ud.name = name;
			ud.defaultSnakeId = 1;
			OnLoginSuccess(ud);
		}

		private void OnLoginSuccess(UserData ud)
		{
			//更新用户信息
			UserManager.Instance.UpdateMainUserData(ud);

			//保存
			AppConfig.Value.mainUserData = ud;
			AppConfig.Save();

			GlobalEvent.onLogin.Invoke(true);

			//进入主页面
			UIManager.Instance.EnterMainPage();
		}
	}
}