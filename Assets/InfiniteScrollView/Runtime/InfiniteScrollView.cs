using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

namespace HowTungTung
{
    [RequireComponent(typeof(ScrollRect))]
    public abstract class InfiniteScrollView : UIBehaviour
    {
        public int cellPoolSize = 20;
        public float spacing = 0f;
        public Vector2 padding;
        public float extendVisibleRange;

        public InfiniteCell cellPrefab;
        public ScrollRect scrollRect;
        public List<InfiniteCellData> dataList = new List<InfiniteCellData>();
        public List<InfiniteCell> cellList = new List<InfiniteCell>();
        protected Queue<InfiniteCell> cellPool = new Queue<InfiniteCell>();
        protected YieldInstruction waitEndOfFrame = new WaitForEndOfFrame();
        private Coroutine snappingProcesser;
        public event Action onRectTransformUpdate;
        public event Action<InfiniteCell> onCellSelected;
        public Action onRefresh;

        public bool IsInitialized
        {
            get;
            private set;
        }

        public virtual void Initialize()
        {
            if (IsInitialized)
                return;
            scrollRect = GetComponent<ScrollRect>();
            scrollRect.onValueChanged.AddListener(OnValueChanged);
            for (int i = 0; i < cellPoolSize; i++)
            {
                var newCell = Instantiate(cellPrefab, scrollRect.content);
                newCell.gameObject.SetActive(false);
                cellPool.Enqueue(newCell);
            }
            IsInitialized = true;
        }

        protected abstract void OnValueChanged(Vector2 normalizedPosition);

        public abstract void Refresh();

        public virtual void Add(InfiniteCellData data)
        {
            if (!IsInitialized)
            {
                Initialize();
            }
            data.index = dataList.Count;
            dataList.Add(data);
            cellList.Add(null);
        }

        public virtual void Remove(int index)
        {
            if (!IsInitialized)
            {
                Initialize();
            }
            if (dataList.Count == 0)
                return;
            dataList.RemoveAt(index);
            Refresh();
        }

        public abstract void Snap(int index, float duration);

        public void SnapLast(float duration)
        {
            Snap(dataList.Count - 1, duration);
        }

        protected void DoSnapping(Vector2 target, float duration)
        {
            if (!gameObject.activeInHierarchy)
                return;
            StopSnapping();
            snappingProcesser = StartCoroutine(ProcessSnapping(target, duration));
        }

        public void StopSnapping()
        {
            if (snappingProcesser != null)
            {
                StopCoroutine(snappingProcesser);
                snappingProcesser = null;
            }
        }

        private IEnumerator ProcessSnapping(Vector2 target, float duration)
        {
            scrollRect.velocity = Vector2.zero;
            Vector2 startPos = scrollRect.content.anchoredPosition;
            float t = 0;
            while (t < 1f)
            {
                if (duration <= 0)
                    t = 1;
                else
                    t += Time.deltaTime / duration;
                scrollRect.content.anchoredPosition = Vector2.Lerp(startPos, target, t);
                var normalizedPos = scrollRect.normalizedPosition;
                if (normalizedPos.y < 0 || normalizedPos.x > 1)
                {
                    break;
                }
                yield return null;
            }
            if (duration <= 0)
                OnValueChanged(scrollRect.normalizedPosition);
            snappingProcesser = null;
        }

        protected void SetupCell(int index, Vector2 pos)
        {
            if (cellList[index] == null)
            {
                var cell = cellPool.Dequeue();
                cell.gameObject.SetActive(true);
                cell.CellData = dataList[index];
                cell.RectTransform.anchoredPosition = pos;
                cellList[index] = cell;
                cell.onSelected += OnCellSelected;
            }
        }

        protected void RecycleCell(int index)
        {
            if (cellList[index] != null)
            {
                var cell = cellList[index];
                cellList[index] = null;
                cellPool.Enqueue(cell);
                cell.gameObject.SetActive(false);
                cell.onSelected -= OnCellSelected;
            }
        }

        private void OnCellSelected(InfiniteCell selectedCell)
        {
            onCellSelected?.Invoke(selectedCell);
        }

        public virtual void Clear()
        {
            if (IsInitialized == false)
                Initialize();
            scrollRect.velocity = Vector2.zero;
            scrollRect.content.anchoredPosition = Vector2.zero;
            dataList.Clear();
            for (int i = 0; i < cellList.Count; i++)
            {
                RecycleCell(i);
            }
            cellList.Clear();
            Refresh();
        }

        protected override void OnRectTransformDimensionsChange()
        {
            base.OnRectTransformDimensionsChange();
            if (scrollRect)
            {
                onRectTransformUpdate?.Invoke();
            }
        }
    }
}