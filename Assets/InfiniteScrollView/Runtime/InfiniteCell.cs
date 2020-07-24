using UnityEngine;
using System;
namespace HowTungTung
{
    public abstract class InfiniteCell<T> : MonoBehaviour
    {
        public event Action<InfiniteCell<T>> onSelected;

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

        private T cellData;
        public T CellData
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

        public abstract void OnUpdate();

        public void InvokeSelected()
        {
            if(onSelected != null)
                onSelected.Invoke(this);
        }
    }
}

