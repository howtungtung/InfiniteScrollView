using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HowTungTung
{
    public abstract class VerticalInfiniteScrollView<T> : InfiniteScrollView<T> where T : InfiniteCellData
    {
        protected override void OnValueChanged(Vector2 normalizedPosition)
        {
            float viewportInterval = scrollRect.viewport.rect.height;
            float minViewport = scrollRect.content.anchoredPosition.y;
            Vector2 viewportRange = new Vector2(minViewport, minViewport + viewportInterval);
            float contentHeight = 0;
            for (int i = 0; i < dataList.Count; i++)
            {
                var visibleRange = new Vector2(contentHeight, contentHeight + dataList[i].cellSize.y);
                if (visibleRange.y < viewportRange.x || visibleRange.x > viewportRange.y)
                {
                    RecycleCell(i);
                }
                contentHeight += dataList[i].cellSize.y + spacing;
            }
            contentHeight = 0;
            for (int i = 0; i < dataList.Count; i++)
            {
                var visibleRange = new Vector2(contentHeight, contentHeight + dataList[i].cellSize.y);
                if (visibleRange.y >= viewportRange.x && visibleRange.x <= viewportRange.y)
                {
                    SetupCell(i, new Vector2(0, -contentHeight));
                }
                contentHeight += dataList[i].cellSize.y + spacing;
            }
        }

        public override void Refresh()
        {
            if (!IsInitialized)
                return;
            float height = 0;
            for (int i = 0; i < dataList.Count; i++)
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

        public override void Snap(int index, float smoothTime)
        {
            if (!IsInitialized)
                return;
            if (index >= dataList.Count)
                return;
            if (scrollRect.content.rect.height < scrollRect.viewport.rect.height)
                return;
            float height = 0;
            for (int i = 0; i < index; i++)
            {
                height += dataList[i].cellSize.y + spacing;
            }
            if (scrollRect.content.anchoredPosition.y != height)
            {
                DoSnapping(new Vector2(0, height), smoothTime);
            }
        }
    }
}