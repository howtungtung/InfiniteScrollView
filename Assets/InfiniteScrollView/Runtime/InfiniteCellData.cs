using UnityEngine;
namespace HowTungTung
{
    public class InfiniteCellData
    {
        public int index;
        public Vector2 cellSize;
        public object data;

        public InfiniteCellData()
        {

        }

        public InfiniteCellData(Vector2 cellSize)
        {
            this.cellSize = cellSize;
        }

        public InfiniteCellData(Vector2 cellSize, object data)
        {
            this.cellSize = cellSize;
            this.data = data;
        }
    }
}