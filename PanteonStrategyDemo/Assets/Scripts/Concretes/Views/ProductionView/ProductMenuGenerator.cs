using UnityEngine;
using UnityEngine.UI;
using PanteonStrategyDemo.Abstracts.ScriptableObjects;
using PanteonStrategyDemo.Concretes.BuildingPlacement;
using PanteonStrategyDemo.Concretes.Managers;
using TMPro;

namespace PanteonStrategyDemo.Concretes.UI.ProductionView
{
    public class ProductMenuGenerator : MonoBehaviour
    {
        ScrollRect _scrollRect;


        void Awake()
        {
            _scrollRect = GetComponent<ScrollRect>();
        }

        void OnEnable()
        {
            GameManager.Instance.OnGameBeginning += CreateContentItem;
        }

        void CreateContentItem()
        {
            foreach (ProductionDataSO building in BuildingManager.Instance.Buildings)
            {
                GameObject item = new GameObject();
                item.transform.SetParent(_scrollRect.content);
                item.transform.localPosition = new Vector3(0, item.transform.localPosition.y, 0);

                item.name = building.Name;
                item.AddComponent<CanvasRenderer>();

                Image itemImage = item.AddComponent<Image>();
                itemImage.sprite = building.ProductionPrefab.GetComponent<SpriteRenderer>().sprite;

                Button itemButton = item.AddComponent<Button>();
                itemButton.onClick.AddListener(() => GetComponentInChildren<BuildingPlacementController>().SelectBuilding(building));

                CreateContentItemText(item, building);
            }
        }

        void CreateContentItemText(GameObject item, ProductionDataSO building)
        {
            GameObject itemTextObj = new GameObject();
            itemTextObj.transform.SetParent(item.transform);
            itemTextObj.AddComponent<CanvasRenderer>();

            TextMeshProUGUI itemText = itemTextObj.AddComponent<TextMeshProUGUI>();
            itemText.alignment = TextAlignmentOptions.Center;
            itemText.fontSize = 24.0f;
            itemText.rectTransform.sizeDelta = new Vector2(100, 50);
            itemText.rectTransform.localPosition = new Vector3(0, -75, 0);
            itemText.text = building.Name;
        }

        void OnDisable()
        {
            GameManager.Instance.OnGameBeginning -= CreateContentItem;
        }
    }
}