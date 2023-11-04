using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class InjectController
    {
        private List<InjectItem> _uiItem = new List<InjectItem>();

        public void AddUIItem(InjectItem item)
        {
            _uiItem.Add(item);
        }

        public InjectItem GetUIItemById(string id)
        {
            var item = _uiItem.Find(x => x.Id.Equals(id));
            if (item == null) return null;
            return item;
        }
    }

    public class InjectItem
    {
        public string Id;
        public Button Btn;
        public Transform Tr;
        public int Num;
        public string Des;

        public InjectItem(string id, Button btn)
        {
            Id = id;
            Btn = btn;
        }

        public InjectItem(string id, Button btn, int num)
        {
            Id = id;
            Btn = btn;
            Num = num;
        }

        public InjectItem(string id, Button btn, string des)
        {
            Id = id;
            Btn = btn;
            Des = des;
        }

        public InjectItem(string id, Transform tr)
        {
            Id = id;
            Tr = tr;
        }
    }
}