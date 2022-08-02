using System.Collections;
using UnityEngine;
using PanteonStrategyDemo.Abstracts.InputSystem;
using PanteonStrategyDemo.Abstracts.ScriptableObjects;
using PanteonStrategyDemo.Concretes.GameData;
using PanteonStrategyDemo.Concretes.Managers;

namespace PanteonStrategyDemo.Concretes.UnitControl
{
    public class UnitPathFinding : MonoBehaviour
    {
        PlacementConditionData _placementConditionData;
        UnitData _selfUnitData;
        UnitDataSO _unitDataSO;
        InputData _inputData;

        Vector2 _currentPosition, _lastPosition;
        int _unitCellHeight;
        int _unitCellWidth;

        bool _isMoving;


        void Awake()
        {
            _placementConditionData = new PlacementConditionData();
            _inputData = new InputData();
        }

        void Start()
        {
            _selfUnitData = GetComponent<UnitData>();
            _unitDataSO = (UnitDataSO)_selfUnitData.UnitDataSO;
            _currentPosition = transform.position;

            StartCoroutine(ClickCheck());
        }

        IEnumerator ClickCheck()
        {
            while (true)
            {
                if (_inputData.RightClickCheck && UnitManager.Instance.SelectedUnitData == _selfUnitData && _isMoving == false)
                {
                    _isMoving = true;
                    Move();
                }
                yield return new WaitForSeconds(0.1f);
            }
        }

        void Move()
        {
            _unitCellHeight = _unitDataSO.CellHeight;
            _unitCellWidth = _unitDataSO.CellWidth;

            Vector2 mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(_inputData.MousePosition);
            if (!BoardManager.Instance.BoardTiles[(int)mousePosition.y, (int)mousePosition.x].IsAvailable)
            {
                _isMoving = false;
                return;
            }
            mousePosition = new Vector2(Mathf.Round(mousePosition.x) - 0.5f, Mathf.Round(mousePosition.y) + 0.5f);

            StartCoroutine(MoveCoroutine(mousePosition));
        }

        IEnumerator MoveCoroutine(Vector2 mousePosition)
        {
            while ((Vector2)transform.position != mousePosition)
            {
                _placementConditionData.GetTileOverProduct(_currentPosition.x - (float)_unitCellWidth / 2, _currentPosition.y - (float)_unitCellHeight / 2, out int row, out int column);
                Vector2 nextPosition1 = new Vector2(BoardManager.Instance.BoardTiles[row, column + 2].XPosition - 0.5f, BoardManager.Instance.BoardTiles[row, column + 2].YPosition + 0.5f);
                Vector2 nextPosition2 = new Vector2(BoardManager.Instance.BoardTiles[row, column].XPosition - 0.5f, BoardManager.Instance.BoardTiles[row, column].YPosition + 0.5f);
                Vector2 nextPosition3 = new Vector2(BoardManager.Instance.BoardTiles[row + 1, column + 1].XPosition - 0.5f, BoardManager.Instance.BoardTiles[row + 1, column + 1].YPosition + 0.5f);
                Vector2 nextPosition4 = new Vector2(BoardManager.Instance.BoardTiles[row - 1, column + 1].XPosition - 0.5f, BoardManager.Instance.BoardTiles[row - 1, column + 1].YPosition + 0.5f);

                float distance1 = Vector2.Distance(mousePosition, nextPosition1);
                float distance2 = Vector2.Distance(mousePosition, nextPosition2);
                float distance3 = Vector2.Distance(mousePosition, nextPosition3);
                float distance4 = Vector2.Distance(mousePosition, nextPosition4);

                float minDistance = Mathf.Min(distance1, distance2, distance3, distance4);

                if (minDistance == distance1)
                {
                    while ((Vector2)transform.position != nextPosition1)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, nextPosition1, Time.deltaTime * _unitDataSO.Speed);
                        yield return new WaitForEndOfFrame();
                    }
                    _lastPosition = _currentPosition;
                    _currentPosition = nextPosition1;
                    continue;
                }
                if (minDistance == distance2)
                {
                    while ((Vector2)transform.position != nextPosition2)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, nextPosition2, Time.deltaTime * _unitDataSO.Speed);
                        yield return new WaitForEndOfFrame();
                    }
                    _currentPosition = nextPosition2;
                    continue;
                }
                if (minDistance == distance3)
                {
                    while ((Vector2)transform.position != nextPosition3)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, nextPosition3, Time.deltaTime * _unitDataSO.Speed);
                        yield return new WaitForEndOfFrame();
                    }
                    _currentPosition = nextPosition3;
                    continue;
                }
                if (minDistance == distance4)
                {
                    while ((Vector2)transform.position != nextPosition4)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, nextPosition4, Time.deltaTime * _unitDataSO.Speed);
                        yield return new WaitForEndOfFrame();
                    }
                    _currentPosition = nextPosition4;
                    continue;
                }
                yield return new WaitForEndOfFrame();
            }
            _isMoving = false;
        }
    }
}