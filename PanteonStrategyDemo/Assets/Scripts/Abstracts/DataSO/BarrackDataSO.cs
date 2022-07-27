using UnityEngine;
using System.Collections.Generic;
using PanteonStrategyDemo.Abstracts.ProductionTypes;

namespace PanteonStrategyDemo.Abstracts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Barrack", menuName = "ProductionSO/Barrack")]
    public class BarrackDataSO : ProductionDataSO
    {
        [SerializeField] List<UnitDataSO> _unitDataList;
        [SerializeField] BuildingType _buildingType;
        [SerializeField] GameObject _barrackPrefab;
        [SerializeField] string _name;
        [SerializeField] int _cost;
        [SerializeField] int _cellHeight = 4;
        [SerializeField] int _cellWidth = 4;

        public List<UnitDataSO> UnitDataList { get => _unitDataList; set => _unitDataList = value; }
        public BuildingType BuildType { get => _buildingType; set => _buildingType = value; }
        public override GameObject ProductionPrefab { get => _barrackPrefab; set => _barrackPrefab = value; }
        public override string Name { get => _name; set => _name = value; }
        public override int Cost { get => _cost; set => _cost = value; }
        public override int CellHeight { get => _cellHeight; set => _cellHeight = value; }
        public override int CellWidth { get => _cellWidth; set => _cellWidth = value; }

    }
}