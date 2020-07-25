using System;
using System.Collections.Generic;
using UnityEngine;
using HowTungTung;
[Serializable]
public class DemoTabPageData : InfiniteCellData
{
    public string content;

    public DemoTabPageData(Vector2 size, string content)
    {
        cellSize = size;
        this.content = content;
    }
}
