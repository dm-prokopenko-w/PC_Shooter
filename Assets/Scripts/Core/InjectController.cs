using System.Collections.Generic;
using UnityEngine.Events;

namespace Game.Core
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
}