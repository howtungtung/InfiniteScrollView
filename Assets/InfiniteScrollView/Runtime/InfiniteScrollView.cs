using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
namespace HowTungTung
{
    [RequireComponent(typeof(ScrollRect))]
    public abstract class InfiniteScrollView<T> : MonoBehaviour where T : InfiniteCellData
    {
        public int cellPoolSize = 20;
        public float spacing = 0f;

        public GameObject cellPrefab;

        protected ScrollRect scrollRect;
        protected InfiniteCell<T> infiniteCell;
        protected List<T> dataList = new List<T>();
        protected List<InfiniteCell<T>> cellList = new List<InfiniteCell<T>>();
        protected Queue<InfiniteCell<T>> cellPool = new Queue<InfiniteCell<T>>();
        private Coroutine snappingProcesser;
        private Vector2 dampingVelocity;
        public event Action<InfiniteCell<T>> onCellSelected;

        public bool IsInitialized 
        {
            get;
            private set;
        }

        protected virtual void Awake()
        {
            Initialize();
        }

        protected IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();
            Refresh();
        }

        public void Initialize()
        {
            if (IsInitialized)
                return;
            infiniteCell = cellPrefab.GetComponent<InfiniteCell<T>>();
            scrollRect = GetComponent<ScrollRect>();
            scrollRect.onValueChanged.AddListener(OnValueChanged);
            for (int i = 0; i < cellPoolSize; i++)
            {
                var newCell = Instantiate(infiniteCell, scrollRect.content);
                newCell.gameObject.SetActive(false);
                cellPool.Enqueue(newCell);
            }
            IsInitialized = true;
        }

        protected abstract void OnValueChanged(Vector2 normalizedPosition);

        public abstract void Refresh();

        public virtual void Add(T data)
        {
            if (!IsInitialized)
                return;
            data.index = dataList.Count;
            dataList.Add(data);
            cellList.Add(null);
            Refresh();
        }

        public virtual void Remove(int index)
        {
            if (!IsInitialized)
                return;
            if (dataList.Count == 0)
                return;
            dataList.RemoveAt(index);
            Refresh();
        }

        public abstract void Snap(int index, float smoothTime);

        protected void DoSnapping(Vector2 target, float smoothTime)
        {
            StopSnapping();
            snappingProcesser = StartCoroutine(ProcessSnapping(target, smoothTime));
        }

        public void StopSnapping()
        {
            if (snappingProcesser != null)
            {
                StopCoroutine(snappingProcesser);
                snappingProcesser = null;
            }
        }

        private IEnumerator ProcessSnapping(Vector2 target, float smoothTime)
        {
            scrollRect.velocity = Vector2.zero;
            while (Vector2.Distance(scrollRect.content.anchoredPosition, target) > 0.1f)
            {
                yield return null;
                scrollRect.content.anchoredPosition = Vector2.SmoothDamp(scrollRect.content.anchoredPosition, target, ref dampingVelocity, smoothTime, float.MaxValue, Time.deltaTime);
                var normalizedPos = scrollRect.normalizedPosition;
                if (normalizedPos.y <= 0 || normalizedPos.x >= 1)
                {
                    normalizedPos.x = Mathf.Clamp01(normalizedPos.x);
                    normalizedPos.y = Mathf.Clamp01(normalizedPos.y);
                    scrollRect.normalizedPosition = normalizedPos;
                    break;
                }
            }
            scrollRect.velocity = Vector2.zero;
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

        private void OnCellSelected(InfiniteCell<T> selectedCell)
        {
            onCellSelected?.Invoke(selectedCell);
        }
    }
}