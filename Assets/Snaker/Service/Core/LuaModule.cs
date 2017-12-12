using OT.Foundation;

namespace Snaker.Service.Core
{
	public class LuaModule:BusinessModule
	{
		private object m_args = null;
		public LuaModule(string name) : base(name)
		{
		}

		/// <summary>
		/// 这里应该去加载Lua脚本
		/// 并且将EventManager映射到Lua脚本中
		/// </summary>
		/// <param name="args"></param>
		public override void Create(object args = null)
		{
			base.Create(args);
			this.Log("Create() Lua = " + Name);
			m_args = args;

			EventTable mgrEvent = GetEventTable();
			//TODO 需要映射到Lua脚本中
		}

		/// <summary>
		/// 调用它以卸载Lua脚本
		/// </summary>
		public override void Release()
		{
			base.Release();
			this.Log("Release() Lua = " + Name);
		}
	}
}