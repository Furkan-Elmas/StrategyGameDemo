using UnityEngine;

namespace PanteonStrategyDemo.Abstracts.ScriptableObjects
{
    public class PowerPlantSO : ProductionDataSO
    {
        [SerializeField] int _cellHeight = 3;
        [SerializeField] int _cellWidth = 2;

        public override int CellHeight { get => _cellHeight; set => _cellHeight = value; }
        public override int CellWidth { get => _cellWidth; set => _cellWidth = value; }
    }
}