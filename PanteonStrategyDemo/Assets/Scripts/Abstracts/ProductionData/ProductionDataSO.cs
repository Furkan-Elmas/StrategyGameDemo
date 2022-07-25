using UnityEngine;

namespace PanteonStrategyDemo.Abstracts.ScriptableObjects
{
    public abstract class ProductionDataSO : ScriptableObject
    {
        public virtual GameObject ProductionPrefab { get; set; }

        public virtual string Name { get; set; }
        public virtual int Cost { get; set; }
        public virtual int CellHeight { get; set; }
        public virtual int CellWidth { get; set; }
    }
}