namespace Snaker.Service.Core
{
	public class GlobalEvent
	{
		public static ModuleEvent<bool> onLogin= new ModuleEvent<bool>();
		public static ModuleEvent<bool> onPay= new ModuleEvent<bool>();
		
	}
}