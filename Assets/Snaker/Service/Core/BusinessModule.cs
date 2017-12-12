using System.Reflection;
using OT.Foundation;

namespace Snaker.Service.Core
{
	public abstract class BusinessModule : Module
	{
		private string m_name = "";

		public string Name
		{
			get
			{
				if (string.IsNullOrEmpty(m_name))
				{
					m_name = this.GetType().Name;
				}
				return m_name;
			}
		}

		public BusinessModule()
		{
		}

		internal BusinessModule(string name)
		{
			m_name = name;
		}

		//============================
		private EventTable m_tblEvent;

		public ModuleEvent Event(string type)
		{
			return GetEventTable().GetEvent(type);
		}

		internal void SetEventTable(EventTable tblEvent)
		{
			m_tblEvent = tblEvent;
		}

		protected EventTable GetEventTable()
		{
			return m_tblEvent ?? (m_tblEvent = new EventTable());
		}

		//================================
		//事件是提供给外部，消息是在内部系统中使用
		internal void HandleMessage(string msg, object[] args)
		{
			this.Log("HandleMessage() msg{0} args{1}", msg, args);
			//通过反射
			MethodInfo methodInfo = this.GetType().GetMethod(msg, BindingFlags.NonPublic);
			if (methodInfo != null)
			{
				methodInfo.Invoke(this, BindingFlags.NonPublic, null, args, null);
			}
			else
			{
				OnModuleMessage(msg, args);
			}
		}

		/// <summary>
		/// 由派生类去实现，用于处理消息
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="args"></param>
		protected virtual void OnModuleMessage(string msg, object[] args)
		{
			this.Log("OnModuleMessage() msg:{0}, args:{1}", msg, args);
		}


		//================================
		public virtual void Create(object args = null)
		{
			this.Log("Create() args={0}", args);
		}

		public override void Release()
		{
			if (m_tblEvent != null)
			{
				m_tblEvent.Clear();
				m_tblEvent = null;
			}
			base.Release();
		}

		public void Show()
		{
			this.Log("Show()");
		}
	}
}