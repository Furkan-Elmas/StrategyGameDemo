using UnityEngine;
using System.Collections.Generic;
using PanteonStrategyDemo.Abstracts.InputSystem;
using PanteonStrategyDemo.Abstracts.Interfaces;
using PanteonStrategyDemo.Abstracts.ScriptableObjects;
using PanteonStrategyDemo.Concretes.Managers;
using PanteonStrategyDemo.Concretes.GameData;

namespace PanteonStrategyDemo.Concretes.UI.InformationView
{
    public class InformationPanelController : MonoBehaviour, IProductClickController
    {
        InformationPanelData _panelData;
        InputData _inputData;

        List<GameObject> _buildingListOnGameBoard;


        void Awake()
        {
            _inputData = new InputData();

            _panelData = FindObjectOfType<InformationPanelData>();
        }

        void Update()
        {
            ProductClickControl();
        }

        public void ProductClickControl()
        {
            if (_inputData.LeftClickCheck)
            {
                _panelData.BuildingImage.gameObject.SetActive(false);
                _panelData.UnitImage.gameObject.SetActive(false);

                _buildingListOnGameBoard = BuildingManager.Instance.BuildingListOnGameBoard;
                foreach (GameObject building in _buildingListOnGameBoard)
                {
                    building.GetComponent<SpriteRenderer>().color = Color.white;
                }

                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(_inputData.MousePosition);
                RaycastHit2D rayHit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity);

                if (rayHit.collider == null || rayHit.transform.TryGetComponent<BuildingData>(out BuildingData buildingData) == false || rayHit.transform.TryGetComponent<UnitData>(out UnitData unitData))
                    return;

                if (buildingData.BuildingDataSO is BarrackDataSO)
                    ShowUnitInfo((BarrackDataSO)buildingData.BuildingDataSO);

                if (buildingData.IsBuildingPlaced)
                {
                    _panelData.BuildingImage.sprite = rayHit.transform.GetComponent<SpriteRenderer>().sprite;
                    _panelData.BuildingText.text = _panelData.BuildingImage.sprite.name;
                    buildingData.transform.GetComponent<SpriteRenderer>().color = Color.red;

                    _panelData.BuildingImage.gameObject.SetActive(true);
                }
            }
        }

        void ShowUnitInfo(BarrackDataSO barrackData)
        {
            _panelData.UnitImage.sprite = barrackData.UnitData.ProductionPrefab.GetComponent<SpriteRenderer>().sprite;
            _panelData.UnitText.text = barrackData.UnitData.Name;

            _panelData.UnitImage.gameObject.SetActive(true);
        }
    }
}