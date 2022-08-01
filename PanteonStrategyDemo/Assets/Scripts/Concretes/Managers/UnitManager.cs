using System.Collections.Generic;
using UnityEngine;
using PanteonStrategyDemo.Abstracts.ScriptableObjects;
using PanteonStrategyDemo.Concretes.GameData;

namespace PanteonStrategyDemo.Concretes.Managers
{
    public class UnitManager : MonoBehaviour
    {
        List<GameObject> _unitListOnGameBoard = new List<GameObject>();
        List<ProductionDataSO> _units = new List<ProductionDataSO>();
        UnitData _selectedUnitData;

        public List<GameObject> UnitListOnGameBoard { get => _unitListOnGameBoard; set => _unitListOnGameBoard = value; }
        public List<ProductionDataSO> Units { get => _units; set => _units = value; }
        public UnitData SelectedUnitData { get => _selectedUnitData; set => _selectedUnitData = value; }

        public static UnitManager Instance { get; set; }


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
            Units.AddRange(Resources.LoadAll<ProductionDataSO>("ScriptableObjects/Units"));
        }
    }
}