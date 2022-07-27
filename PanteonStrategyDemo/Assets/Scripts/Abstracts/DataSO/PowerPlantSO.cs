using UnityEngine;
using PanteonStrategyDemo.Abstracts.ProductionTypes;

namespace PanteonStrategyDemo.Abstracts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PowerPlant", menuName = "ProductionSO/Power Plant")]
    public class PowerPlantSO : ProductionDataSO
    {   
        [SerializeField] BuildingType _buildingType;
        [SerializeField] GameObject _powerPlantPrefab;
        [SerializeField] string _name;
        [SerializeField] float _goldGenerationRate;
        [SerializeField] int _cost;
        [SerializeField] int _cellHeight = 3;
        [SerializeField] int _cellWidth = 2;

        public BuildingType BuildType { get => _buildingType; set => _buildingType = value; }
        public override GameObject ProductionPrefab { get => _powerPlantPrefab; set => _powerPlantPrefab = value; }
        public override string Name { get => _name; set => _name = value; }
        public float GoldGenerationRate { get => _goldGenerationRate; set => _goldGenerationRate = value; }
        public override int Cost { get => _cost; set => _cost = value; }
        public override int CellHeight { get => _cellHeight; set => _cellHeight = value; }
        public override int CellWidth { get => _cellWidth; set => _cellWidth = value; }
    }
}