using System;

namespace Snaker.Service.Core
{
	public abstract class ServiceModule<T>:Module where T: ServiceModule<T>,new()
	{
		public static T ms_instance = default(T);

		public static T Instance
		{
			get
			{
				if (ms_instance == null)
				{
					ms_instance = new T();
				}
				return ms_instance;
			}
		}

		protected void CheckSingleton()
		{
			if (ms_instance == null)
			{
				var exp = new Exception("ServiceModule<"+typeof(T).Name+">"+"无法直接实例化，因为是单例");
				throw exp;
			}

		}
	}
}