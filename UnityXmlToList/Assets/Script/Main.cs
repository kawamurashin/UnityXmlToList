using Script.Controller;
using UnityEngine;

namespace Script
{
	public class Main : MonoBehaviour {

		// Use this for initialization
		private void Start ()
		{
			var controllerManager = ControllerManager.Instance;
			controllerManager.gameObject.transform.parent = transform;

		}
	}
}
