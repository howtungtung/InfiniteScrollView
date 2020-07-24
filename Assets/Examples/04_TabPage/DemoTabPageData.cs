using System;
using System.Collections.Generic;
using UnityEngine;
using HowTungTung;
[Serializable]
public class DemoTabPageData : InfiniteCellData
{
    public override float Height => height;
    public override float Width => width;
    public string content;

    private float height;
    private float width;

    public DemoTabPageData(Vector2 size, string content)
    {
        width = size.x;
        height = size.y;
        this.content = content;
    }
}
