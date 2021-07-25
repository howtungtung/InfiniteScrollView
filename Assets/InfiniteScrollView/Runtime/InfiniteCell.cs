using UnityEngine;
using System;
namespace HowTungTung
{
    public class InfiniteCell : MonoBehaviour
    {
        public event Action<InfiniteCell> onSelected;

        private RectTransform rectTransform;
        public RectTransform RectTransform
        {
            get
            {
                if (rectTransform == null)
                    rectTransform = GetComponent<RectTransform>();
                return rectTransform;
            }
        }

        private InfiniteCellData cellData;
        public InfiniteCellData CellData
        {
            set
            {
                cellData = value;
                OnUpdate();
            }
            get
            {
                return cellData;
            }
        }

        public virtual void OnUpdate() { }

        public void InvokeSelected()
        {
            if (onSelected != null)
                onSelected.Invoke(this);
        }
    }
}

