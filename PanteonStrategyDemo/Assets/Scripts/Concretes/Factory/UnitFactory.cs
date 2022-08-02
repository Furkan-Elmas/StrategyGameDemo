using UnityEngine;
using PanteonStrategyDemo.Abstracts.ScriptableObjects;
using PanteonStrategyDemo.Concretes.GameData;
using PanteonStrategyDemo.Concretes.UnitControl;

namespace PanteonStrategyDemo.Concretes.Factory
{
    public class UnitFactory
    {
        GameObject UnitClone { get; set; }


        public UnitFactory(GameObject unitPrefab, ProductionDataSO unitDataSO, out UnitData unitData)
        {
            UnitClone = MonoBehaviour.Instantiate(unitPrefab, Vector3.zero, Quaternion.identity);
            UnitClone.AddComponent<BoxCollider2D>();
            UnitClone.AddComponent<UnitPathFinding>();
            unitData = UnitClone.AddComponent<UnitData>();
            unitData.UnitDataSO = unitDataSO;
        }

        public GameObject GetUnit()
        {
            return UnitClone;
        }
    }
}