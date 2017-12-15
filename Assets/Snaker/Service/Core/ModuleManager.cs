using System;
using System.Collections.Generic;
using OT.Foundation;

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
		private Dictionary<string, EventTable> m_mapPreListenEvents;
		private Dictionary<string, List<MessageObject>> m_mapCacheMessage;

		private string m_domain;


		public ModuleManager()
		{
			m_mapModules = new Dictionary<string, BusinessModule>();
			m_mapPreListenEvents = new Dictionary<string, EventTable>();
			m_mapCacheMessage = new Dictionary<string, List<MessageObject>>();
		}

		public void Init(string domain = "Snaker.Module")
		{
			CheckSingleton();
			m_domain = domain;
		}

		public T CreateModule<T>(object[] args) where T : BusinessModule
		{
			return (T) CreateModule(typeof(T).Name, args);
		}

		public BusinessModule CreateModule(string name,params object[] args)
		{
			if (m_mapModules.ContainsKey(name))
			{
				this.LogError("this module has exist");
				return null;
			}
			BusinessModule module = null;

			Type type = Type.GetType(m_domain + "." + name);

			if (type != null)
			{
				module = Activator.CreateInstance(type) as BusinessModule;
			}
			else
			{
				module = new LuaModule(name);
			}
			m_mapModules.Add(name, module);


			module = m_mapModules[name];

			//预监听事件
			if (m_mapPreListenEvents.ContainsKey(name))
			{
				var tblEvent = m_mapPreListenEvents[name];
				m_mapPreListenEvents.Remove(name);
				module.SetEventTable(tblEvent);
			}
			module.Create(args);
			//处理消息
			if (m_mapCacheMessage.ContainsKey(name))
			{
				List<MessageObject> list = m_mapCacheMessage[name];
				for (int i = 0; i < list.Count; i++)
				{
					MessageObject msgobj = list[i];
					module.HandleMessage(msgobj.msg, msgobj.args);
				}
				m_mapCacheMessage.Remove(name);
			}

			return module;
		}
		//===========================================


		public void ReleaseModule(BusinessModule module)
		{
			if (m_mapModules.ContainsKey(module.Name))
			{
				m_mapModules.Remove(module.Name);

				module.Release();
			}
		}

		public void RealseAll()
		{
			foreach (var @event in m_mapPreListenEvents)
			{
				@event.Value.Clear();
			}
			m_mapPreListenEvents.Clear();
			m_mapCacheMessage.Clear();
			foreach (var module in m_mapModules)
			{
				ReleaseModule(module.Value);
			}
			m_mapModules.Clear();
		}

		//===========================================
		/// <summary>
		/// 获取模块
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public T GetModule<T>() where T : BusinessModule
		{
			return (T) GetModule(typeof(T).Name);
		}

		private BusinessModule GetModule(string name)
		{
			if (m_mapModules.ContainsKey(name))
			{
				return m_mapModules[name];
			}
			return null;
		}

		//===========================================

		public void SendMessage(string target, string msg, params object[] args)
		{
			BusinessModule module = GetModule(target);
			if (module != null)
			{
				module.HandleMessage(msg, args);
			}
			else
			{
				var messageObjects = GetCacheMessageList(target);
				messageObjects.Add(new MessageObject() {args = args, msg = msg, target = target});
			}
		}

		/// <summary>
		/// 获取缓存消息
		/// </summary>
		/// <param name="target"></param>
		/// <returns></returns>
		private List<MessageObject> GetCacheMessageList(string target)
		{
			if (!m_mapCacheMessage.ContainsKey(target))
			{
				m_mapCacheMessage.Add(target, new List<MessageObject>());
			}
			var messageObjects = m_mapCacheMessage[target];
			return messageObjects;
		}


		//===========================================
		public ModuleEvent Event(string target, string type)
		{
			ModuleEvent evt = null;
			if (m_mapModules.ContainsKey(target))
			{
				var module = m_mapModules[target];
				evt = module.Event(type);
			}
			else
			{
				//预创建事件
				EventTable table = GetPreEventTable(target);
				evt = table.GetEvent(type);
				this.LogWarning("Event() target不存在！将预监听事件! target:{0}, event:{1}", target, type);
			}
			return evt;

		}

		private EventTable GetPreEventTable(string target)
		{
			EventTable table = null;
			if (!m_mapPreListenEvents.ContainsKey(target))
			{
				table = new EventTable();
				m_mapPreListenEvents.Add(target, table);
			}
			else
			{
				table = m_mapPreListenEvents[target];
			}
			return table;
		}

	}
}