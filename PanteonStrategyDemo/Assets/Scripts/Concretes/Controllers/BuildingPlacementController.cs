using UnityEngine;
using System.Collections;
using PanteonStrategyDemo.Abstracts.ScriptableObjects;
using PanteonStrategyDemo.Abstracts.InputSystem;

namespace PanteonStrategyDemo.Concretes.Controllers
{
    public class BuildingPlacementController : MonoBehaviour
    {
        InputData _inputData;

        GameObject _currentBuildingPrefab;
        GameObject _selectedBuildingPrefab;

        int _cellHeight;
        int _cellWidth;


        void Awake()
        {
            _inputData = new InputData();
        }

        public void SelectBuilding(ProductionDataSO buildingDataSO)
        {
            if (_currentBuildingPrefab)
                Destroy(_currentBuildingPrefab);

            _currentBuildingPrefab = null;
            _selectedBuildingPrefab = buildingDataSO.ProductionPrefab;
            _currentBuildingPrefab = Instantiate(_selectedBuildingPrefab, Vector3.zero, Quaternion.identity);
            _cellHeight = buildingDataSO.CellHeight;
            _cellWidth = buildingDataSO.CellWidth;

            StartCoroutine(PlaceBuilding());
        }

        IEnumerator PlaceBuilding()
        {
            while (true)
            {
                if (_inputData.LeftClickCheck)
                {
                    _currentBuildingPrefab = null;
                    break;
                }

                Vector2 mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(_inputData.MousePosition);

                _currentBuildingPrefab.transform.position = new Vector2(Mathf.Round(mousePosition.x), Mathf.Round(mousePosition.y));

                yield return new WaitForEndOfFrame();

            }

        }
    }
}