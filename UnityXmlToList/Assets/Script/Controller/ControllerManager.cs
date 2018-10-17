using Script.Model;
using Script.View;
using UnityEngine;

namespace Script.Controller
{
    public class ControllerManager:MonoBehaviour
    {
        private static ControllerManager _instance;
        //
        private ModelManager _modelManager;
        private ViewManager _viewManager;

        public static ControllerManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    var obj = new GameObject("ControllerManager");
                    _instance = obj.AddComponent<ControllerManager>();
                }
                return _instance;
            }
        }

        private void Start()
        {
            _modelManager = ModelManager.Instance;
            _modelManager.gameObject.transform.parent = transform;
            _modelManager.AddLoadCompleteCallBack(ModelLoadCompleteHandler);
            
            var obj = new GameObject("ViewManager");
            _viewManager = obj.AddComponent<ViewManager>();
            obj.transform.parent = transform;

            _modelManager.Load();

        }

        private void ModelLoadCompleteHandler()
        {
            _viewManager.LoadComplete();

        }
    }
}