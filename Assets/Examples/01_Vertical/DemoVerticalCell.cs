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
