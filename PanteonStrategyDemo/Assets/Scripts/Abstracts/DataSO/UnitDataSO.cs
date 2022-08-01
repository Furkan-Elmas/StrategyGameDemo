using UnityEngine;
using PanteonStrategyDemo.Abstracts.Enums;

namespace PanteonStrategyDemo.Abstracts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Unit", menuName = "ProductionSO/Unit")]
    public class UnitDataSO : ProductionDataSO
    {
        [SerializeField] UnitType _unitType;
        [SerializeField] GameObject _unitPrefab;
        [SerializeField] string _name;
        [SerializeField] float _speed;
        [SerializeField] int _cost;
        [SerializeField] int _cellHeight = 1;
        [SerializeField] int _cellWidth = 1;

        public UnitType UnitType { get => _unitType; set => _unitType = value; }
        public override GameObject ProductionPrefab { get => _unitPrefab; set => _unitPrefab = value; }
        public override string Name { get => _name; set => _name = value; }
        public float Speed { get => _speed; set => _speed = value; }
        public override int Cost { get => _cost; set => _cost = value; }
        public override int CellHeight { get => _cellHeight; set => _cellHeight = value; }
        public override int CellWidth { get => _cellWidth; set => _cellWidth = value; }
    }
}