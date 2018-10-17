using System;
using System.Collections.Generic;
using Script.Model;
using Script.Model.Data;
using UnityEngine;

namespace Script.View.List
{
    public class ListManager : MonoBehaviour
    {
        public static string MouseDown = "mouseDownCallBack";
        public static string MouseUp = "mouseUpCallBack";
        //
        public static float ListWidth = 600;
        public static float ListHeight = 180;
        //
        //private ListData _selectedListData;
        private bool _isMouseDown = false;
        private Vector2 _mouseDownPosition;
        private bool _isMove = false;
        private float _vy = 0;
        private GameObject _container ;
        private List<ListContaiener> _listContaieners = new List<ListContaiener>();

        public bool IsMouseDown
        {
            get { return _isMouseDown; }
            set { _isMouseDown = value; }
        }

        public float Vy
        {
            get { return _vy; }
            set { _vy = value; }
        }

        private void Awake()
        {
            _container = new GameObject("ListManager");
            _container.transform.SetParent(transform);
        }

        private void Update()
        {
            if (_isMouseDown == false)
            {
                _vy *= 0.9f;
                if (Mathf.Abs(_vy) < 0.01)
                {
                    _vy = 0;
                }
            }
            else
            {
                if (_isMove == false)
                {
                    var d = Mathf.Sqrt(Mathf.Pow(_mouseDownPosition.x - Input.mousePosition.x, 2) +
                                       Mathf.Pow(_mouseDownPosition.y - Input.mousePosition.y, 2));
                    if (d > 5)
                    {
                        _isMove = true;
                        var n = _listContaieners.Count;
                        for (var i = 0; i < n; i++)
                        {
                            var listContainer = _listContaieners[i];
                            listContainer.MouseUpHandler();
                        }
                    }
                }
            }

            var vecter = transform.localPosition;
            vecter.y += _vy;
            if (vecter.y > 0)
            {
                _vy *= -0.5f;
                vecter.y = 0;
            }
            else if (vecter.y < -1 * ListHeight * _listContaieners.Count)
            {
                _vy *= -0.5f;
                vecter.y = -1 * ListHeight * _listContaieners.Count;
            }
            transform.localPosition = vecter;
            
        }

        public void LoadComplete()
        {
            //_listContaieners = 
            //
            var modelManager = ModelManager.Instance;
            var listDatas = modelManager.ListDatas;
            //
            
            var n = listDatas.Count;
            for (var i = 0; i < n; i++)
            {
                var listData = listDatas[i];

                var obj = new GameObject("list" + i);
                var listContaiener = obj.AddComponent<ListContaiener>();
                listContaiener.SetListData(listData);
                listContaiener.transform.SetParent(_container.transform);
                listContaiener.transform.localPosition = new Vector3(0, i * ListHeight);
                listContaiener.AddListCallBack(ListCallBackHandler);
                //
                _listContaieners.Add(listContaiener);
            }
        }

        private void ListCallBackHandler(string massage, ListData listData)
        {
            if (massage == MouseDown)
            {
                ListMouseDownHandler(listData);
            }
            else if (massage == MouseUp)
            {
                ListMouseUpHandler(listData);
            }
        }

        private void ListMouseDownHandler(ListData listData)
        {
            //_selectedListData = listData;
            var n = _listContaieners.Count;
            for (var i = 0; i < n; i++)
            {
                var listContainer = _listContaieners[i];
                if (listContainer.ListData.Id == listData.Id)
                {
                    listContainer.MouseDownHandler();
                    break;
                }
            }
        }

        private void ListMouseUpHandler(ListData listData)
        {
            //_selectedListData = null;
            if (_isMove == false)
            {
                Debug.Log("セレクト"　+　listData.Id);
            }
            var n = _listContaieners.Count;
            for (var i = 0; i < n; i++)
            {
                var listContainer = _listContaieners[i];
                if (listContainer.ListData.Id == listData.Id)
                {
                    listContainer.MouseUpHandler();
                    break;
                }
            }
        }

        public void mouseButtonDown()
        {
            _isMouseDown = true;
            _isMove = false;
            _mouseDownPosition = new Vector2(Input.mousePosition.x ,Input.mousePosition.y);
        }

        public void mouseButtonUp()
        {
            _isMouseDown = false;
        }
    }
}