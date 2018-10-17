using System.Collections.Generic;
using System.Xml;
using Script.Model.Data;
using UnityEngine;

namespace Script.Model
{
    public class ModelManager : MonoBehaviour
    {
        public delegate void Callback();

        public Callback LoadCompleteCallBack;

        public void AddLoadCompleteCallBack(Callback callback)
        {
            LoadCompleteCallBack += callback;
        }

        private List<ListData> _listDatas;
        private static ModelManager _instance;

        public static ModelManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    var obj = new GameObject("ModelManager");
                    _instance = obj.AddComponent<ModelManager>();
                }

                return _instance;
            }
        }

        public void Load()
        {
            var xmlData = new TextAsset();
            xmlData = (TextAsset) Resources.Load("data", typeof(TextAsset));
            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlData.text);
            XmlNodeList lists = xmlDoc.GetElementsByTagName("lists");
            XmlNode node = lists[0];
            XmlNodeList nodeList = node.ChildNodes;
            _listDatas = new List<ListData>();
            var n = nodeList.Count;
            for (var i = 0; i < n; i++)
            {
                node = nodeList[i];
                var listData = new ListData();
                listData.SetNode(node);
                //
                _listDatas.Add(listData);
            }

            LoadCompleteCallBack();
        }

        public List<ListData> ListDatas
        {
            get { return _listDatas; }
        }
    }
}