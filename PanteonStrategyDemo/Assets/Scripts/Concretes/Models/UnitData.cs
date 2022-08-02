using UnityEngine;
using PanteonStrategyDemo.Abstracts.Enums;
using PanteonStrategyDemo.Abstracts.ScriptableObjects;

namespace PanteonStrategyDemo.Concretes.GameData
{
    public class UnitData : MonoBehaviour
    {
        public ProductionDataSO UnitDataSO { get; set; }
        public UnitType UnitType { get; set; }
    }
}