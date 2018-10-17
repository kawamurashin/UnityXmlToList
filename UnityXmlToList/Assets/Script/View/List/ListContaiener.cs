using Script.Model.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Script.View.List
{
    public class ListContaiener : MonoBehaviour
    {
        public delegate void CallBack(string massage , ListData listData);

        private CallBack ListCallBack;

        public void AddListCallBack(CallBack callBack)
        {
            ListCallBack += callBack;
        }

        private Image _backgroundImage;
        private ListData _listData;
        //
        public void SetListData(ListData listData)
        {
            _listData = listData;
            //
            var obj = new GameObject("BackgroundImage");
            obj.transform.SetParent(transform);
            _backgroundImage= obj.AddComponent<Image>();
            var rectTransFrom = _backgroundImage.GetComponent<RectTransform>();
            rectTransFrom.sizeDelta = new Vector2(ListManager.ListWidth,ListManager.ListHeight);
            
            //Material material = new Material(Shader.Find("Shader/ListShader"));
            Color color;
            if (listData.Id % 2 == 0)
            {
                color = new Color( 0.0f, 0.1f, 0.0f ,1.0f );
            }
            else
            {
                color = new Color( 0.1f, 0.0f, 0.0f,1.0f );
            }
            //material.color = color;
            _backgroundImage.color = color;


            //image.material = material;
            Font font = Resources.Load<Font>("Font/KozGoPro-ExtraLight");
            obj = new GameObject("List Text");
            obj.transform.SetParent(transform);
            var text = obj.AddComponent<Text>();
            text.font = font;
            text.text = listData.Name;
            text.fontSize = 48;
            rectTransFrom = text.GetComponent<RectTransform>();
            rectTransFrom.sizeDelta = new Vector2(ListManager.ListWidth, ListManager.ListHeight);

        }

        public void MouseUpHandler()
        {
            Color color;
            if (_listData.Id % 2 == 0)
            {
                color = new Color( 0.0f, 0.1f, 0.0f ,1.0f );
            }
            else
            {
                color = new Color( 0.1f, 0.0f, 0.0f,1.0f );
            }
            //material.color = color;
            _backgroundImage.color = color;
            
        }

        public void MouseDownHandler()
        {
            _backgroundImage.color = Color.black;
        }
        
        
        
        private void OnMouseDown()
        {
            ListCallBack(ListManager.MouseDown, _listData);
        }

        private void OnMouseUp()
        {
            ListCallBack(ListManager.MouseUp, _listData);
        }

        private void Awake()
        {

            var collider2D = gameObject.AddComponent<BoxCollider2D>();
            collider2D.size = new Vector2(ListManager.ListWidth,ListManager.ListHeight);
        }

        public ListData ListData
        {
            get { return _listData; }
        }



    }
}