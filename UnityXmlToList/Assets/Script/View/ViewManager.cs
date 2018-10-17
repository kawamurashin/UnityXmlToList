using Script.Model;
using Script.View.List;
using UnityEngine;

namespace Script.View
{
   
    public class ViewManager : MonoBehaviour
    {
        private Canvas _canvas;

        private float _preMouseY = 0;
        private ListManager _listManager;
        private void Awake()
        {
            var obj = new GameObject("Canvas");
            _canvas = obj.AddComponent<Canvas>();
            obj.transform.SetParent(transform);
            var rectTransfrom = _canvas.GetComponent<RectTransform>();
            rectTransfrom.sizeDelta = new Vector2(1080,1920);
            
            obj = new GameObject("LineContainers");
            _listManager = obj.AddComponent<ListManager>();
            obj.transform.SetParent(_canvas.transform);

        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _listManager.mouseButtonDown();
                _preMouseY = Input.mousePosition.y;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _listManager.mouseButtonUp();
            }
            //
            if (Input.GetMouseButton(0)) {
                
                var vy = Input.mousePosition.y - _preMouseY;
                _listManager.Vy = vy;
                _preMouseY = Input.mousePosition.y;
            }
        }

        public void LoadComplete()
        {
            _listManager.LoadComplete();

            /*
            string str = "";
            n = 20;
            for (int i = 0; i < n; i++)
            {
                var nam = pluszero(i);
                str += "<list id=\"" + i + "\" name=\"" + nam + "\" />\n";
            }
            Debug.Log(str);
            */
        }

        private string pluszero(int num)
        {
            string str = (1000 + num).ToString().Substring(1);

            return str;
        }
    }
}