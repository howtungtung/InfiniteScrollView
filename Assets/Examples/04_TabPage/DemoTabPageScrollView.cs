using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using HowTungTung;
public class DemoTabPageScrollView : HorizontalInfiniteScrollView<DemoTabPageData>, IBeginDragHandler, IEndDragHandler
{
    public string[] pageContents;
    public Toggle[] toggles;
    public ToggleGroup toggleGroup;
    public Vector2 eachContentSize = new Vector2(600, 400);
    public float snapThreshold = 100;
    private bool isEndDragging;

    private void Start()
    {
        foreach (var data in pageContents)
            Add(new DemoTabPageData(eachContentSize, data));
    }

    public void OnToggleChange(int index)
    {
        if (toggles[index].isOn)
        {
            Snap(index, 0.1f);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        StopSnapping();
        isEndDragging = false;
        toggleGroup.SetAllTogglesOff();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isEndDragging = true;
    }

    private void Update()
    {
        if (isEndDragging)
        {
            if (Mathf.Abs(scrollRect.velocity.x) <= snapThreshold)
            {
                isEndDragging = false;
                var clampX = Mathf.Min(0, scrollRect.content.anchoredPosition.x);
                int closingIndex = Mathf.Abs(Mathf.RoundToInt(clampX / eachContentSize.x));
                toggles[closingIndex].isOn = true;
            }
        }
    }
}
