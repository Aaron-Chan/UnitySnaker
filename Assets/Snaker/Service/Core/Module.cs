using OT.Foundation;

namespace Snaker.Service.Core
{
	public abstract class Module
	{
		/// <summary>
		/// 调用它以释放模块
		/// </summary>
		public virtual void Release()
		{
			this.Log("Release");
		}

	}
}