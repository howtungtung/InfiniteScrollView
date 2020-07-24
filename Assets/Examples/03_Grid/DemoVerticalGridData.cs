using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HowTungTung;
public class DemoVerticalGridData : InfiniteCellData
{
    public override float Width => width;
    public override float Height => height;
    private float width;
    private float height;

    public DemoVerticalGridData(Vector2 size)
    {
        width = size.x;
        height = size.y;
    }
}
