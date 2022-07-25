using UnityEngine;

namespace PanteonStrategyDemo.Abstracts.ScriptableObjects
{

    public abstract class BarrackDataSO : ProductionDataSO
    {
        [SerializeField] int _cellHeight = 4;
        [SerializeField] int _cellWidth = 4;

        public override int CellHeight { get => _cellHeight; set => _cellHeight = value; }
        public override int CellWidth { get => _cellWidth; set => _cellWidth = value; }
    }
}