namespace Snaker.Service.Core.Example
{
	public class ModuleC : ServiceModule<ModuleC>
	{
		public ModuleEvent onEvent = new ModuleEvent();
		public void Init()
		{

		}

		public void DoSomething()
		{
			onEvent.Invoke(null);
		}
	}
}