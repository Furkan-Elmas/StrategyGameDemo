using UnityEngine;
using PanteonStrategyDemo.Abstracts.ScriptableObjects;
using PanteonStrategyDemo.Concretes.GameData;
using PanteonStrategyDemo.Concretes.Managers;
using PanteonStrategyDemo.Concretes.Factory;

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
            // Gets unit cell height and cell width from unit generator building scriptable object
            int cellHeight = BuildingManager.Instance.SelectedBuildingData.BuildingDataSO.CellHeight;
            int cellWidth = BuildingManager.Instance.SelectedBuildingData.BuildingDataSO.CellWidth;

            _selectedBuildingData = BuildingManager.Instance.SelectedBuildingData;
            // Gets selected building place's row and column value of BoardTile multiple array
            _placementConditionData.GetTileOverProduct(_selectedBuildingData.transform.position.x - (float)cellWidth / 2, _selectedBuildingData.transform.position.y - (float)cellHeight / 2, out int row, out int column);

            // Available tile search iteration
            for (int i = 0; i < cellHeight + 2; i++)
            {
                for (int j = 0; j < cellWidth + 2; j++)
                {
                    // Creates unit and makes tile state false if place is available
                    if (BoardManager.Instance.BoardTiles[i + row - 1, j + column].IsAvailable)
                    {
                        BarrackDataSO barrackData = (BarrackDataSO)_selectedBuildingData.BuildingDataSO;
                        BoardManager.Instance.BoardTiles[i + row - 1, j + column].IsAvailable = false;
                        Vector2 unitPosition = new Vector2(BoardManager.Instance.BoardTiles[i + row, j + column].XPosition - 0.5f, BoardManager.Instance.BoardTiles[i + row, j + column].YPosition - 0.5f);
                        UnitFactory unitFactory = new UnitFactory(barrackData.UnitData.ProductionPrefab, barrackData.UnitData, out UnitData unitData);
                        GameObject unitClone = unitFactory.GetUnit();
                        unitClone.transform.position = unitPosition;
                        UnitManager.Instance.UnitListOnGameBoard.Add(unitClone);
                        return;
                    }
                }
            }
        }
    }
}