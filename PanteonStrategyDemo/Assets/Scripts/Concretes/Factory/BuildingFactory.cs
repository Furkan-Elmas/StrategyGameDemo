using UnityEngine;
using PanteonStrategyDemo.Abstracts.ScriptableObjects;
using PanteonStrategyDemo.Concretes.GameData;

namespace PanteonStrategyDemo.Concretes.Factory
{
    public class BuildingFactory
    {
        GameObject BuildingClone { get; set; }


        public BuildingFactory(GameObject buildingPrefab, ProductionDataSO buildingDataSO, out BuildingData buildingData)
        {
            BuildingClone = MonoBehaviour.Instantiate(buildingPrefab, Vector3.zero, Quaternion.identity);
            BuildingClone.AddComponent<BoxCollider2D>();
            buildingData = BuildingClone.AddComponent<BuildingData>();
            buildingData.BuildingDataSO = buildingDataSO;
        }

        public GameObject GetBuilding()
        {
            return BuildingClone;
        }
    }
}