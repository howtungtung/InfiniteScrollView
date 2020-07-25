using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace HowTungTung
{
    public abstract class HorizontalInfiniteScrollView<T> : InfiniteScrollView<T> where T : InfiniteCellData
    {
        protected override void OnValueChanged(Vector2 normalizedPosition)
        {
            float viewportInterval = scrollRect.viewport.rect.width;
            float minViewport = -scrollRect.content.anchoredPosition.x;
            Vector2 viewportRange = new Vector2(minViewport, minViewport + viewportInterval);
            float contentWidth = 0;
            for (int i = 0; i < dataList.Count; i++)
            {
                var visibleRange = new Vector2(contentWidth, contentWidth + dataList[i].cellSize.x);
                if (visibleRange.y < viewportRange.x || visibleRange.x > viewportRange.y)
                {
                    RecycleCell(i);
                }
                contentWidth += dataList[i].cellSize.x + spacing;
            }
            contentWidth = 0;
            for (int i = 0; i < dataList.Count; i++)
            {
                var visibleRange = new Vector2(contentWidth, contentWidth + dataList[i].cellSize.x);
                if (visibleRange.y >= viewportRange.x && visibleRange.x <= viewportRange.y)
                {
                    SetupCell(i, new Vector2(contentWidth, 0));
                }
                contentWidth += dataList[i].cellSize.x + spacing;
            }
        }

        public override void Refresh()
        {
            if (!IsInitialized)
                return;
            float width = 0;
            for (int i = 0; i < dataList.Count; i++)
            {
                width += dataList[i].cellSize.x + spacing;
            }
            for (int i = 0; i < cellList.Count; i++)
            {
                RecycleCell(i);
            }
            scrollRect.content.sizeDelta = new Vector2(width, scrollRect.content.sizeDelta.y);
            OnValueChanged(scrollRect.normalizedPosition);
        }

        public override void Snap(int index, float smoothTime)
        {
            if (!IsInitialized)
                return;
            if (index >= dataList.Count)
                return;
            float width = 0;
            for (int i = 0; i < index; i++)
            {
                width += dataList[i].cellSize.x + spacing;
            }
            if (scrollRect.content.anchoredPosition.x != width)
            {
                DoSnapping(new Vector2(-width, 0), smoothTime);
            }
        }
    }
}