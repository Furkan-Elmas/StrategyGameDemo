using UnityEngine;
using PanteonStrategyDemo.Abstracts.ScriptableObjects;
using PanteonStrategyDemo.Concretes.GameData;
using PanteonStrategyDemo.Concretes.Managers;

namespace PanteonStrategyDemo.Concretes.UnitControl
{
    public class UnitPlacementController : MonoBehaviour
    {
        PlacementConditionData _placementConditionData;

        BuildingData _selectedBuildingData;


        void Awake()
        {
            _placementConditionData = new PlacementConditionData();
        }

        public void GenerateUnit()
        {
            int cellHeight = BuildingManager.Instance.SelectedBuildingData.BuildingDataSO.CellHeight;
            int cellWidth = BuildingManager.Instance.SelectedBuildingData.BuildingDataSO.CellWidth;

            _selectedBuildingData = BuildingManager.Instance.SelectedBuildingData;
            _placementConditionData.GetTileOverProduct(_selectedBuildingData.transform.position.x - (float)cellWidth / 2, _selectedBuildingData.transform.position.y - (float)cellHeight / 2, out int row, out int column);

            for (int i = 0; i < cellHeight + 2; i++)
            {
                for (int j = 0; j < cellWidth + 2; j++)
                {
                    if (BoardManager.Instance.BoardTiles[i + row - 1, j + column].IsAvailable)
                    {
                        BarrackDataSO barrackData = (BarrackDataSO)_selectedBuildingData.BuildingDataSO;
                        BoardManager.Instance.BoardTiles[i + row - 1, j + column].IsAvailable = false;
                        Vector2 unitPosition = new Vector2(BoardManager.Instance.BoardTiles[i + row, j + column].XPosition - 0.5f, BoardManager.Instance.BoardTiles[i + row, j + column].YPosition - 0.5f);
                        GameObject unitClone = Instantiate(barrackData.UnitData.ProductionPrefab, unitPosition, Quaternion.identity);
                        UnitData unitData = unitClone.AddComponent<UnitData>();
                        unitData.UnitDataSO = barrackData.UnitData;
                        unitClone.AddComponent<UnitPathFinding>();
                        UnitManager.Instance.UnitListOnGameBoard.Add(unitClone);
                        return;
                    }
                }
            }
        }
    }
}