# InfiniteScrollView
InfiniteScrollView is made for Unity extension, that support use as less as possible gameObject count to achieve large infinite scrolling content. 
Developed by native UGUI system, no any magical code inside, so you can easily modify and extend by yourself.

**It's support all platform which Unity supported. Tested on Unity 2019 LTS**
<br>
**You can also get compatible version with older Unity from the link below**
<br>
[2018 LTS](https://github.com/howtungtung/InfiniteScrollView/tree/Unity2018-LTS)
<br>
[2017 LTS](https://github.com/howtungtung/InfiniteScrollView/tree/Unity2017-LTS)


## Features
1. Easy customize by OOP concept
2. Support diffirent cell height or width
3. Full sourcecode and example include

## How to use
1. Download this project and copy Assets/InfiniteScrollView to your own project
2. There are three class you have to inherit and implement by your own context
  <br> `InfiniteCellData`
  <br> `InfiniteCell`
  <br> `VerticalInfiniteScrollView, HorizontalInfiniteScrollView or VerticalGridInfiniteScrollView`
  
3. Create CSharp script named `DemoVerticalDate` and inherit `InfiniteCellData`
```csharp
using HowTungTung;
public class DemoVerticalData : InfiniteCellData
{
    public override float Height => height;

    private float height;

    public DemoVerticalData(float height)
    {
        this.height = height;
    }
}
```
4. Create CSharp script named `DemoVerticalCell` and inherit `InfiniteCell<DemoVeritcalData>`
```csharp
using UnityEngine;
using UnityEngine.UI;
using HowTungTung;
public class DemoVerticalCell : InfiniteCell<DemoVerticalData>
{
    public Text text;

    public override void OnUpdate()
    {
        var size = RectTransform.sizeDelta;
        size.y = CellData.Height;
        RectTransform.sizeDelta = size;
        text.text = CellData.Height.ToString();
    }
}
```
5. Create CSharp script named `DemoVerticalScrollView` and inherit `VerticalInfiniteScrollView<DemoVerticalData>`
```csharp
using HowTungTung;
public class DemoVerticalScrollView : VerticalInfiniteScrollView<DemoVerticalData>
{
    
}
```
6. Create CSharp script named `Tester` only for test purpose
```csharp
using HowTungTung;
public class Tester : MonoBehavior
{
   private void Start()
   {
       var infiniteScrollView = FindObjectOfType<InfiniteScrollView<DemoVerticalData>>();
       for (int i = 0; i < 100; i++)
       {
           var data = new DemoVerticalData(Random.Range(50, 300));
           infiniteScrollView.Add(data);
       }
   }
}
```
7. Right click on Hierarchy and create UI/Scroll View
8. Select `Content` at Scroll View/Viewport/Content on Hierarchy
9. Right click and create UI/Button
10. Click `Anchor Preset` on the Button object, and select left top preset with press shift and Alt key
11. Attach `DemoVerticalCell` on this Button and drag into Project window to make to Prefab
12. Delete the Button from Hierarchy
13. Select Scroll View and attach `DemoVerticalScrollView` then drag the prefab we just created on `Cell Prefab` field of the `DemoVerticalScrollView` component
14. Attach `Tester` to any other gameObject in scene and test it!

For more detail of the example please refer to Assets/Examples

## Example
<img src="https://i.imgur.com/SjkEqnQ.png">
<img src="https://imgur.com/mk39LUO.png">
<img src="https://imgur.com/13rwdCO.png">
<img src="https://imgur.com/nxdvC1e.png">
