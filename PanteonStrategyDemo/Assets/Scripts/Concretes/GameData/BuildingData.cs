using UnityEngine;
using PanteonStrategyDemo.Abstracts.ProductionTypes;
using PanteonStrategyDemo.Abstracts.ScriptableObjects;

namespace PanteonStrategyDemo.Concretes.GameData
{
    public class BuildingData : MonoBehaviour
    {
        public ProductionDataSO BuildingDataSO { get; set; }
        public BuildingType BuildingType { get; set; }
        public bool IsBuildingPlaced { get; set; }
    }
}