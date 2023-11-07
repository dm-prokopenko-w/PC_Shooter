using UnityEngine;
using UnityEngine.UI;

namespace Game.Core
{
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
