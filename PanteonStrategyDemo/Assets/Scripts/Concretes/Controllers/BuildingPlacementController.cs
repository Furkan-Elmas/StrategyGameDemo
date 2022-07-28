using UnityEngine;
using System.Collections;
using PanteonStrategyDemo.Abstracts.ScriptableObjects;
using PanteonStrategyDemo.Abstracts.InputSystem;
using PanteonStrategyDemo.Concretes.GameBoardData;

namespace PanteonStrategyDemo.Concretes.Controllers
{
    public class BuildingPlacementController : MonoBehaviour
    {
        InputData _inputData;
        PlacementContidions _placementConditions;

        GameObject _currentBuildingPrefab;
        GameObject _selectedBuildingPrefab;

        int _cellHeight;
        int _cellWidth;


        void Awake()
        {
            _inputData = new InputData();
            _placementConditions = new PlacementContidions();
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
                Vector2 mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(_inputData.MousePosition);

                _currentBuildingPrefab.transform.position = new Vector2(Mathf.Round(mousePosition.x), Mathf.Round(mousePosition.y));

                bool isPlaceAvailable = _placementConditions.IsPlaceAvailable(_currentBuildingPrefab.transform.position.x, _currentBuildingPrefab.transform.position.y, _cellHeight, _cellWidth);

                _currentBuildingPrefab.GetComponent<SpriteRenderer>().color = isPlaceAvailable ? Color.white : Color.red;

                if (_inputData.LeftClickCheck)
                {
                    if (isPlaceAvailable)
                    {
                        _placementConditions.UpdateTiles(_currentBuildingPrefab.transform.position.x, _currentBuildingPrefab.transform.position.y, _cellHeight, _cellWidth);
                        _currentBuildingPrefab = null;
                        break;
                    }
                }

                yield return new WaitForEndOfFrame();
            }
        }
    }
}