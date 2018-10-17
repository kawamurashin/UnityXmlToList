using System.Xml;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Model.Data
{
    public class ListData
    {
        private uint _id;
        private string _name;
        public void SetNode(XmlNode xmlNode)
        {
            _name = xmlNode.Attributes["name"].Value;
            _id = uint.Parse(xmlNode.Attributes["id"].Value);

        }

        public uint Id
        {
            get { return _id; }
        }

        public string Name
        {
            get { return _name; }
        }
    }
}