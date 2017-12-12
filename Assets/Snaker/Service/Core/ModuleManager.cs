using System.Collections.Generic;

namespace Snaker.Service.Core
{
	public class ModuleManager : ServiceModule<ModuleManager>
	{

		class MessageObject
		{
			public string target;
			public string msg;
			public object[] args;
		}

		private Dictionary<string, BusinessModule> m_mapModules;
		private Dictionary<string, ModuleEvent> m_mapPreListenEvents;
		private Dictionary<string, List<MessageObject>> m_mapCacheMessage;


	}
}