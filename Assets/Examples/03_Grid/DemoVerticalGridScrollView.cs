using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HowTungTung;
public class DemoVerticalGridScrollView : VerticalGridInfiniteScrollView<DemoVerticalGridData>
{
    protected override void OnCellSelected(InfiniteCell<DemoVerticalGridData> selectedCell)
    {
        selectedCell.GetComponent<Button>().Select();
        Debug.Log("On Cell Selected " + selectedCell.CellData.index);
    }
}
