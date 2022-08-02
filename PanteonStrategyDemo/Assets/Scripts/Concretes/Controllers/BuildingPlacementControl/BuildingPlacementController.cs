using UnityEngine;
using System.Collections;
using PanteonStrategyDemo.Abstracts.ScriptableObjects;
using PanteonStrategyDemo.Abstracts.InputSystem;
using PanteonStrategyDemo.Concretes.Managers;
using PanteonStrategyDemo.Concretes.GameData;
using PanteonStrategyDemo.Concretes.Factory;

namespace PanteonStrategyDemo.Concretes.BuildingPlacement
{
    public class BuildingPlacementController : MonoBehaviour
    {
        PlacementConditionData _placementConditionData;
        InputData _inputData;
        BuildingData _buildingData;

        GameObject _currentBuildingPrefab;
        GameObject _selectedBuildingPrefab;

        int _cellHeight;
        int _cellWidth;


        void Awake()
        {
            _placementConditionData = new PlacementConditionData();
            _inputData = new InputData();
        }

        // Button click event on production menu
        public void SelectBuilding(ProductionDataSO buildingDataSO)
        {
            if (_currentBuildingPrefab) // Destroy if any selected building exists
                Destroy(_currentBuildingPrefab);

            _currentBuildingPrefab = null;
            _selectedBuildingPrefab = buildingDataSO.ProductionPrefab; // Taking prefab gameobject from building's scriptable object

            _cellHeight = buildingDataSO.CellHeight;
            _cellWidth = buildingDataSO.CellWidth;

            StartCoroutine(PlaceBuilding(buildingDataSO));
        }

        // Place building coroutine
        IEnumerator PlaceBuilding(ProductionDataSO buildingDataSO)
        {
            BuildingFactory unitFactory = new BuildingFactory(_selectedBuildingPrefab, buildingDataSO, out BuildingData buildingData); // Uses factory for creating instance gameobject
            _currentBuildingPrefab = unitFactory.GetBuilding();

            while (true)
            {
                Vector2 mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(_inputData.MousePosition);

                // Building prefab follows mouse position
                _currentBuildingPrefab.transform.position = new Vector2(Mathf.Round(mousePosition.x) - (float)buildingDataSO.CellWidth % 2 / 2, Mathf.Round(mousePosition.y) - (float)buildingDataSO.CellHeight % 2 / 2);

                // Returns true if tiles under building prefab is available
                bool isPlaceAvailable = _placementConditionData.IsPlaceAvailable(_currentBuildingPrefab.transform.position.x - (float)buildingDataSO.CellWidth / 2 + 1, _currentBuildingPrefab.transform.position.y - (float)buildingDataSO.CellHeight / 2, _cellWidth, _cellHeight);

                // Makes building color red if place is not available, otherwise makes it white
                _currentBuildingPrefab.GetComponent<SpriteRenderer>().color = isPlaceAvailable ? Color.white : Color.red;

                // Destroy building when right-click
                if (_inputData.RightClickCheck)
                {
                    Destroy(_currentBuildingPrefab);
                    break;
                }

                if (_inputData.LeftClickCheck)
                {
                    if (isPlaceAvailable)
                    {   
                        // Turns Tiles' IsPlaceAvailable boolean fields false
                        _placementConditionData.UpdateTiles(_currentBuildingPrefab.transform.position.x - (float)buildingDataSO.CellWidth / 2 + 1, _currentBuildingPrefab.transform.position.y - (float)buildingDataSO.CellHeight / 2, _cellWidth, _cellHeight);
                        buildingData.IsBuildingPlaced = true;
                        BuildingManager.Instance.BuildingListOnGameBoard.Add(_currentBuildingPrefab);
                        _currentBuildingPrefab = null;

                        break;
                    }
                }

                yield return new WaitForEndOfFrame();
            }
        }
    }
}