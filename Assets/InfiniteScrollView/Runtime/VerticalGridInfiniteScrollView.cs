using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HowTungTung
{
    public abstract class VerticalGridInfiniteScrollView<T> : InfiniteScrollView<T> where T : InfiniteCellData
    {
        public int columeCount;

        protected override void OnValueChanged(Vector2 normalizedPosition)
        {
            float viewportInterval = scrollRect.viewport.rect.height;
            float minViewport = scrollRect.content.anchoredPosition.y;
            Vector2 viewportRange = new Vector2(minViewport, minViewport + viewportInterval);
            float contentHeight = 0;
            for (int i = 0; i < dataList.Count; i += columeCount)
            {
                for (int j = 0; j < columeCount; j++)
                {
                    int index = i + j;
                    if (index >= dataList.Count)
                        break;
                    var visibleRange = new Vector2(contentHeight, contentHeight + dataList[index].cellSize.y);
                    if (visibleRange.y < viewportRange.x || visibleRange.x > viewportRange.y)
                    {
                        RecycleCell(index);
                    }
                }
                contentHeight += dataList[i].cellSize.y + spacing;
            }
            contentHeight = 0;
            for (int i = 0; i < dataList.Count; i += columeCount)
            {
                for (int j = 0; j < columeCount; j++)
                {
                    int index = i + j;
                    if (index >= dataList.Count)
                        break;
                    var visibleRange = new Vector2(contentHeight, contentHeight + dataList[index].cellSize.y);
                    if (visibleRange.y >= viewportRange.x && visibleRange.x <= viewportRange.y)
                    {
                        SetupCell(index, new Vector2((dataList[index].cellSize.x + spacing) * j, -contentHeight));
                    }
                }
                contentHeight += dataList[i].cellSize.y + spacing;
            }
        }

        public override void Refresh()
        {
            if (!IsInitialized)
                return;
            float height = 0;
            for (int i = 0; i < dataList.Count; i += columeCount)
            {
                height += dataList[i].cellSize.y + spacing;
            }
            for (int i = 0; i < cellList.Count; i++)
            {
                RecycleCell(i);
            }
            scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, height);
            OnValueChanged(scrollRect.normalizedPosition);
        }

        public override void Snap(int index, float duration)
        {
            if (!IsInitialized)
                return;
            if (index >= dataList.Count)
                return;
            var rowNumber = index / columeCount;
            var height = 0f;
            for (int i = 0; i < rowNumber; i ++)
            {
                height += dataList[i * columeCount].cellSize.y + spacing;
            }
            if (scrollRect.content.anchoredPosition.y != height)
            {
                DoSnapping(new Vector2(0, height), duration);
            }
        }
    }
}