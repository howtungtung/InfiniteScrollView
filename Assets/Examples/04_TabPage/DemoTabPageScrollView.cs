using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using HowTungTung;
public class DemoTabPageScrollView : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    public InfiniteScrollView scrollView;
    public string[] pageContents;
    public Toggle[] toggles;
    public ToggleGroup toggleGroup;
    public Vector2 eachContentSize = new Vector2(600, 400);
    public float snapThreshold = 100;
    private bool isEndDragging;

    private void Start()
    {
        foreach (var data in pageContents)
        {
            scrollView.Add(new InfiniteCellData(eachContentSize, new DemoTabPageData { content = data }));
        }
        scrollView.Refresh();
    }

    public void OnToggleChange(int index)
    {
        if (toggles[index].isOn)
        {
            scrollView.Snap(index, 0.1f);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        scrollView.StopSnapping();
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
            if (Mathf.Abs(scrollView.scrollRect.velocity.x) <= snapThreshold)
            {
                isEndDragging = false;
                var clampX = Mathf.Min(0, scrollView.scrollRect.content.anchoredPosition.x);
                int closingIndex = Mathf.Abs(Mathf.RoundToInt(clampX / eachContentSize.x));
                toggles[closingIndex].isOn = true;
            }
        }
    }
}
