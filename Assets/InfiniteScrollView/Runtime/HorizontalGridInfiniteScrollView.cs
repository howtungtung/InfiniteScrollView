using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HowTungTung
{
    public class HorizontalGridInfiniteScrollView : InfiniteScrollView
    {
        public int rowCount = 1;
        public bool isAtLeft = true;
        public bool isAtRight = true;
        protected override void OnValueChanged(Vector2 normalizedPosition)
        {
            if (rowCount <= 0)
            {
                rowCount = 1;
            }
            float viewportInterval = scrollRect.viewport.rect.width;
            float minViewport = -scrollRect.content.anchoredPosition.x;
            Vector2 viewportRange = new Vector2(minViewport - extendVisibleRange, minViewport + viewportInterval + extendVisibleRange);
            float contentWidth = padding.x;
            for (int i = 0; i < dataList.Count; i += rowCount)
            {
                for (int j = 0; j < rowCount; j++)
                {
                    int index = i + j;
                    if (index >= dataList.Count)
                        break;
                    var visibleRange = new Vector2(contentWidth, contentWidth + dataList[index].cellSize.x);
                    if (visibleRange.y < viewportRange.x || visibleRange.x > viewportRange.y)
                    {
                        RecycleCell(index);
                    }
                }
                contentWidth += dataList[i].cellSize.x + spacing;
            }
            contentWidth = padding.x;
            for (int i = 0; i < dataList.Count; i += rowCount)
            {
                for (int j = 0; j < rowCount; j++)
                {
                    int index = i + j;
                    if (index >= dataList.Count)
                        break;
                    var visibleRange = new Vector2(contentWidth, contentWidth + dataList[index].cellSize.x);
                    if (visibleRange.y >= viewportRange.x && visibleRange.x <= viewportRange.y)
                    {
                        SetupCell(index, new Vector2(contentWidth, (dataList[index].cellSize.y + spacing) * -j));
                        if(visibleRange.y >= viewportRange.x)
                            cellList[index].transform.SetAsLastSibling();
                        else
                            cellList[index].transform.SetAsFirstSibling();
                    }
                }
                contentWidth += dataList[i].cellSize.x + spacing;
            }
            if (scrollRect.content.sizeDelta.x > viewportInterval)
            {
                isAtLeft = viewportRange.x + extendVisibleRange <= dataList[0].cellSize.x;
                isAtRight = scrollRect.content.sizeDelta.x - viewportRange.y + extendVisibleRange <= dataList[dataList.Count - 1].cellSize.x;
            }
            else
            {
                isAtLeft = true;
                isAtRight = true;
            }
        }

        public sealed override void Refresh()
        {
            if (!IsInitialized)
            {
                Initialize();
            }
            if (scrollRect.viewport.rect.width == 0)
                StartCoroutine(DelayToRefresh());
            else
                DoRefresh();
        }

        private void DoRefresh()
        {
            float width = padding.x;
            for (int i = 0; i < dataList.Count; i += rowCount)
            {
                width += dataList[i].cellSize.x + spacing;
            }
            for (int i = 0; i < cellList.Count; i++)
            {
                RecycleCell(i);
            }
            width += padding.y;
            scrollRect.content.sizeDelta = new Vector2(width, scrollRect.content.sizeDelta.y);
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
            var columeNumber = index / rowCount;
            var width = padding.x;
            for (int i = 0; i < columeNumber; i++)
            {
                width += dataList[i * rowCount].cellSize.x + spacing;
            }
            width = Mathf.Min(scrollRect.content.rect.width - scrollRect.viewport.rect.width, width);
            if (scrollRect.content.anchoredPosition.x != width)
            {
                DoSnapping(new Vector2(-width, 0), duration);
            }
        }
    }
}

