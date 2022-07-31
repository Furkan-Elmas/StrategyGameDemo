using System.Collections.Generic;
using UnityEngine;
using PanteonStrategyDemo.Abstracts.ScriptableObjects;

namespace PanteonStrategyDemo.Concretes.Managers
{
    public class BuildingManager : MonoBehaviour
    {
        List<GameObject> _buildingListOnGameBoard = new List<GameObject>();
        List<ProductionDataSO> _buildings = new List<ProductionDataSO>();

        public List<GameObject> BuildingListOnGameBoard { get => _buildingListOnGameBoard; set => _buildingListOnGameBoard = value; }
        public List<ProductionDataSO> Buildings { get => _buildings; set => _buildings = value; }

        public static BuildingManager Instance { get; set; }


        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
                return;
            }
        }

        void Start()
        {
            GetAllBuildingData();
        }

        void GetAllBuildingData()
        {
            Buildings.AddRange(Resources.LoadAll<ProductionDataSO>("ScriptableObjects/Barracks"));
            Buildings.AddRange(Resources.LoadAll<ProductionDataSO>("ScriptableObjects/PowerPlants"));
        }
    }
}