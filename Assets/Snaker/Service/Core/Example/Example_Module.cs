using OT.Foundation;
using UnityEngine;

namespace Snaker.Service.Core.Example
{
	public class Example_Module : MonoBehaviour
	{
		void Start()
		{
			Debuger.EnableLog = true;

			ModuleC.Instance.Init();
			ModuleManager.Instance.Init("Snaker.Service.Core.Example");

			ModuleManager.Instance.CreateModule("ModuleA");
			ModuleManager.Instance.CreateModule("ModuleB");

		}


	}

}