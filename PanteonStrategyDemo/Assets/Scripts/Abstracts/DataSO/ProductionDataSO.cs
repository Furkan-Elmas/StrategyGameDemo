using UnityEngine;

namespace PanteonStrategyDemo.Abstracts.ScriptableObjects
{
    public abstract class ProductionDataSO : ScriptableObject
    {
        public abstract GameObject ProductionPrefab { get; set; }
        public abstract string Name { get; set; }
        public abstract int Cost { get; set; }
        public abstract int CellHeight { get; set; }
        public abstract int CellWidth { get; set; }
    }
}