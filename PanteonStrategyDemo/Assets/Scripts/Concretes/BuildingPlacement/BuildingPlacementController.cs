using UnityEngine;
using System.Collections;
using PanteonStrategyDemo.Abstracts.ScriptableObjects;
using PanteonStrategyDemo.Abstracts.InputSystem;
using PanteonStrategyDemo.Concretes.Managers;
using PanteonStrategyDemo.Concretes.GameData;

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

        public void SelectBuilding(ProductionDataSO buildingDataSO)
        {
            if (_currentBuildingPrefab)
                Destroy(_currentBuildingPrefab);

            _currentBuildingPrefab = null;
            _selectedBuildingPrefab = buildingDataSO.ProductionPrefab;
            _currentBuildingPrefab = Instantiate(_selectedBuildingPrefab, Vector3.zero, Quaternion.identity);

            _cellHeight = buildingDataSO.CellHeight;
            _cellWidth = buildingDataSO.CellWidth;

            StartCoroutine(PlaceBuilding(buildingDataSO));
        }

        IEnumerator PlaceBuilding(ProductionDataSO buildingDataSO)
        {
            while (true)
            {
                Vector2 mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(_inputData.MousePosition);

                _currentBuildingPrefab.transform.position = new Vector2(Mathf.Round(mousePosition.x) - (float)buildingDataSO.CellWidth % 2 / 2, Mathf.Round(mousePosition.y) - (float)buildingDataSO.CellHeight % 2 / 2);

                bool isPlaceAvailable = _placementConditionData.IsPlaceAvailable(_currentBuildingPrefab.transform.position.x - (float)buildingDataSO.CellWidth / 2 + 1, _currentBuildingPrefab.transform.position.y - (float)buildingDataSO.CellHeight / 2, _cellWidth, _cellHeight);

                _currentBuildingPrefab.GetComponent<SpriteRenderer>().color = isPlaceAvailable ? Color.white : Color.red;

                if (_inputData.RightClickCheck)
                {
                    Destroy(_currentBuildingPrefab);
                    break;
                }

                if (_inputData.LeftClickCheck)
                {
                    if (isPlaceAvailable)
                    {
                        _placementConditionData.UpdateTiles(_currentBuildingPrefab.transform.position.x - (float)buildingDataSO.CellWidth / 2 + 1, _currentBuildingPrefab.transform.position.y - (float)buildingDataSO.CellHeight / 2, _cellWidth, _cellHeight);
                        _buildingData = _currentBuildingPrefab.AddComponent<BuildingData>();
                        _buildingData.IsBuildingPlaced = true;
                        _currentBuildingPrefab.AddComponent<BoxCollider2D>();
                        _buildingData.BuildingDataSO = buildingDataSO;
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