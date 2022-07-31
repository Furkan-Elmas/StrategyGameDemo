using System.Collections.Generic;
using UnityEngine;
using PanteonStrategyDemo.Abstracts.ScriptableObjects;

namespace PanteonStrategyDemo.Concretes.Managers
{
    public class UnitManager : MonoBehaviour
    {
        List<GameObject> _unitListOnGameBoard = new List<GameObject>();
        List<ProductionDataSO> _units = new List<ProductionDataSO>();

        public List<GameObject> UnitListOnGameBoard { get => _unitListOnGameBoard; set => _unitListOnGameBoard = value; }
        public List<ProductionDataSO> Units { get => _units; set => _units = value; }

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