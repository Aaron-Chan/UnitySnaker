using OT.Foundation;

namespace Snaker.Service.Core.Example
{
	public class ModuleB : BusinessModule
	{
		public ModuleEvent onModuleEventB { get { return Event("onModuleEventB"); } }

		public override void Create(object args = null)
		{
			base.Create(args);
			onModuleEventB.Invoke("aaaa");
		}

		protected void MessageFromA_2(string args1, int args2)
		{
			this.Log("MessageFromA_2() args:{0},{1}", args1, args2);
		}

		protected override void OnModuleMessage(string msg, object[] args)
		{
			base.OnModuleMessage(msg, args);
			this.Log("OnModuleMessage() msg:{0}, args:{1},{2},{3}", msg, args[0], args[1], args[2]);
		}
	}

}