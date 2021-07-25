using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HowTungTung
{
    public class VerticalInfiniteScrollView : InfiniteScrollView
    {
        public bool isAtTop = true;
        public bool isAtBottom = true;

        public override void Initialize()
        {
            base.Initialize();
            isAtTop = true;
            isAtBottom = true;
        }

        protected override void OnValueChanged(Vector2 normalizedPosition)
        {
            if (dataList.Count == 0)
                return;
            float viewportInterval = scrollRect.viewport.rect.height;
            float minViewport = scrollRect.content.anchoredPosition.y;
            Vector2 viewportRange = new Vector2(minViewport - extendVisibleRange, minViewport + viewportInterval + extendVisibleRange);
            float contentHeight = padding.x;
            for (int i = 0; i < dataList.Count; i++)
            {
                var visibleRange = new Vector2(contentHeight, contentHeight + dataList[i].cellSize.y);
                if (visibleRange.y < viewportRange.x || visibleRange.x > viewportRange.y)
                {
                    RecycleCell(i);
                }
                contentHeight += dataList[i].cellSize.y + spacing;
            }
            contentHeight = padding.x;
            for (int i = 0; i < dataList.Count; i++)
            {
                var visibleRange = new Vector2(contentHeight, contentHeight + dataList[i].cellSize.y);
                if (visibleRange.y >= viewportRange.x && visibleRange.x <= viewportRange.y)
                {
                    SetupCell(i, new Vector2(0, -contentHeight));
                    if (visibleRange.y >= viewportRange.x)
                        cellList[i].transform.SetAsLastSibling();
                    else
                        cellList[i].transform.SetAsFirstSibling();
                }
                contentHeight += dataList[i].cellSize.y + spacing;
            }
            if (scrollRect.content.sizeDelta.y > viewportInterval)
            {
                isAtTop = viewportRange.x + extendVisibleRange <= 0.001f;
                isAtBottom = scrollRect.content.sizeDelta.y - viewportRange.y + extendVisibleRange <= 0.001f;
            }
            else
            {
                isAtTop = true;
                isAtBottom = true;
            }
        }

        public sealed override void Refresh()
        {
            if (!IsInitialized)
            {
                Initialize();
            }
            if (scrollRect.viewport.rect.height == 0)
                StartCoroutine(DelayToRefresh());
            else
                DoRefresh();
        }

        private void DoRefresh()
        {
            float height = padding.x;
            for (int i = 0; i < dataList.Count; i++)
            {
                height += dataList[i].cellSize.y + spacing;
            }
            for (int i = 0; i < cellList.Count; i++)
            {
                RecycleCell(i);
            }
            height += padding.y;
            scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, height);
            OnValueChanged(scrollRect.normalizedPosition);
            onRefresh?.Invoke();
        }

        private IEnumerator DelayToRefresh()
        {
            yield return waitEndOfFrame;
            DoRefresh();
        }

        public override void Snap(int index, float duration)
        {
            if (!IsInitialized)
                return;
            if (index >= dataList.Count)
                return;
            if (scrollRect.content.rect.height < scrollRect.viewport.rect.height)
                return;
            float height = padding.x;
            for (int i = 0; i < index; i++)
            {
                height += dataList[i].cellSize.y + spacing;
            }
            height = Mathf.Min(scrollRect.content.rect.height - scrollRect.viewport.rect.height, height);
            if (scrollRect.content.anchoredPosition.y != height)
            {
                DoSnapping(new Vector2(0, height), duration);
            }
        }

        public override void Remove(int index)
        {
            var removeCell = dataList[index];
            base.Remove(index);
            scrollRect.content.anchoredPosition -= new Vector2(0, removeCell.cellSize.y + spacing);
        }
    }
}