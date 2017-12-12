using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Snaker.Service.Core
{
	public class ModuleEvent : UnityEvent<object>
	{

	}

	public class ModuleEvent<T> : UnityEvent<T>
	{

	}

	public class EventTable
	{
		private Dictionary<string, ModuleEvent> m_mapEvent;

		/// <summary>
		/// 获取Type所指定的ModuleEvent（它其实是一个EventTable）
		/// 如果不存在，则实例化一个
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public ModuleEvent GetEvent(string type)
		{
			if (m_mapEvent == null)
			{
				m_mapEvent = new Dictionary<string, ModuleEvent>();
			}
			if (!m_mapEvent.ContainsKey(type))
			{
				m_mapEvent.Add(type,new ModuleEvent());
			}
			return m_mapEvent[type];
		}

		public void Clear()
		{
			if (m_mapEvent != null)
			{
				foreach (var @event in m_mapEvent)
				{
					@event.Value.RemoveAllListeners();
				}
				m_mapEvent.Clear();
			}
			
			
		}

	}

}

