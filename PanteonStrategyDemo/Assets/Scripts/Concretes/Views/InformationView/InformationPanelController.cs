using UnityEngine;
using System.Collections.Generic;
using PanteonStrategyDemo.Abstracts.InputSystem;
using PanteonStrategyDemo.Abstracts.ScriptableObjects;
using PanteonStrategyDemo.Concretes.Managers;
using PanteonStrategyDemo.Concretes.GameData;

namespace PanteonStrategyDemo.Concretes.UI.InformationView
{
    public class InformationPanelController : MonoBehaviour
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

        void ProductClickControl()
        {
            if (_inputData.LeftClickCheck)
            {
                foreach (GameObject building in BuildingManager.Instance.BuildingListOnGameBoard)
                {
                    building.GetComponent<SpriteRenderer>().color = Color.white;
                }

                foreach(GameObject unit in UnitManager.Instance.UnitListOnGameBoard)
                {
                    unit.GetComponent<SpriteRenderer>().color = Color.white;
                }

                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(_inputData.MousePosition);
                RaycastHit2D rayHit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity);

                if (rayHit.collider == null)
                    return;

                if (rayHit.transform.TryGetComponent<BuildingData>(out BuildingData buildingData))
                    BuildingDataRead(buildingData, rayHit);

                if (rayHit.transform.TryGetComponent<UnitData>(out UnitData unitData))
                    UnitDataRead(unitData, rayHit);
            }
        }

        void ShowUnitInfo(BarrackDataSO barrackData)
        {
            _panelData.UnitImage.sprite = barrackData.UnitData.ProductionPrefab.GetComponent<SpriteRenderer>().sprite;
            _panelData.UnitText.text = barrackData.UnitData.Name;

            _panelData.UnitImage.gameObject.SetActive(true);
        }

        void BuildingDataRead(BuildingData buildingData, RaycastHit2D rayHit)
        {
            BuildingManager.Instance.SelectedBuildingData = buildingData;

            if (buildingData.BuildingDataSO is BarrackDataSO)
                ShowUnitInfo((BarrackDataSO)buildingData.BuildingDataSO);
            else
                _panelData.UnitImage.gameObject.SetActive(false);

            if (buildingData.IsBuildingPlaced)
            {
                _panelData.ProductImage.sprite = rayHit.transform.GetComponent<SpriteRenderer>().sprite;
                _panelData.ProductText.text = _panelData.ProductImage.sprite.name;
                buildingData.transform.GetComponent<SpriteRenderer>().color = Color.red;

                _panelData.ProductImage.gameObject.SetActive(true);
            }
        }

        void UnitDataRead(UnitData unitData, RaycastHit2D rayHit)
        {
            UnitManager.Instance.SelectedUnitData = unitData;

            _panelData.UnitImage.gameObject.SetActive(false);

            _panelData.ProductImage.sprite = rayHit.transform.GetComponent<SpriteRenderer>().sprite;
            _panelData.ProductText.text = _panelData.ProductImage.sprite.name;
            unitData.transform.GetComponent<SpriteRenderer>().color = Color.red;

            _panelData.ProductImage.gameObject.SetActive(true);
        }
    }
}