using UnityEngine;
using PanteonStrategyDemo.Abstracts.ProductionTypes;
using PanteonStrategyDemo.Abstracts.ScriptableObjects;

namespace PanteonStrategyDemo.Concretes.GameData
{
    public class UnitData : MonoBehaviour
    {
        public ProductionDataSO UnitDataSO { get; set; }
        public UnitType UnitType { get; set; }
        public bool IsUnitPlaced { get; set; }
    }
}