using OT.Foundation;

namespace Snaker.Service.Core.Example
{
	public class ModuleA : BusinessModule
	{
		public override void Create(object args = null)
		{
			base.Create(args);

			//业务层模块之间，通过Message进行通讯
			ModuleManager.Instance.SendMessage("ModuleB", "MessageFromA_1", 1, 2, 3);
			ModuleManager.Instance.SendMessage("ModuleB", "MessageFromA_2", "abc", 123);

			//业务层模块之间，通过Event进行通讯 
			ModuleManager.Instance.Event("ModuleB", "onModuleEventB").AddListener(OnModuleEventB);

			//业务层调用服务层，通过事件监听回调
			ModuleC.Instance.onEvent.AddListener(OnModuleEventC);
			ModuleC.Instance.DoSomething();

			//全局事件
			GlobalEvent.onLogin.AddListener(OnLogin);
		}

		private void OnModuleEventC(object args)
		{
			this.Log("OnModuleEventC() args:{0}", args);
		}

		private void OnModuleEventB(object args)
		{
			this.Log("OnModuleEventB() args:{0}", args);
		}

		private void OnLogin(bool args)
		{
			this.Log("OnLogin() args:{0}", args);
		}
	}

}