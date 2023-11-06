using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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

        public void AddedActionOnClick(string id, UnityAction action)
        {
            var items = _uiItem.FindAll(x => x.Id.Equals(id));
            foreach (var item in items)
            {
                item.Btn.onClick.AddListener(action);
            }
        }
    }

    public class InjectItem
    {
        public string Id;
        public Button Btn;
        public Image Icon;
        public Transform Tr;
        public RectTransform RectTr;
        public int Num;
        public string Des;

        public InjectItem(string id, Button btn)
        {
            Id = id;
            Btn = btn;
        }

        public InjectItem(string id, Image icon)
        {
            Id = id;
            Icon = icon;
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

        public InjectItem(string id, RectTransform rectTr)
        {
            Id = id;
            RectTr = rectTr;
        }
    }
}