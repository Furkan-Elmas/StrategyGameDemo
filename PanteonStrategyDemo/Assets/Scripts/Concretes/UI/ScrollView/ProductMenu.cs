using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PanteonStrategyDemo.Abstracts.ScriptableObjects;
using PanteonStrategyDemo.Concretes.Managers;
using TMPro;

namespace PanteonStrategyDemo.Concretes.Controllers
{
    public class ProductMenu : MonoBehaviour
    {
        List<ProductionDataSO> _products;

        ScrollRect _scrollRect;


        void Awake()
        {
            _scrollRect = GetComponent<ScrollRect>();
            _products = new List<ProductionDataSO>();
        }

        void OnEnable()
        {
            GameManager.Instance.OnGameBeginning += CreateContentItem;
        }

        void GetBuildingsData()
        {
            _products.AddRange(Resources.LoadAll<ProductionDataSO>("ScriptableObjects/Barracks"));
            _products.AddRange(Resources.LoadAll<ProductionDataSO>("ScriptableObjects/PowerPlants"));
        }

        void CreateContentItem()
        {
            GetBuildingsData();

            foreach (ProductionDataSO product in _products)
            {
                GameObject item = new GameObject();
                item.transform.SetParent(_scrollRect.content);
                item.transform.localPosition = new Vector3(0, item.transform.localPosition.y, 0);

                item.name = product.Name;
                item.AddComponent<CanvasRenderer>();

                Image itemImage = item.AddComponent<Image>();
                itemImage.sprite = product.ProductionPrefab.GetComponent<SpriteRenderer>().sprite;

                Button itemButton = item.AddComponent<Button>();
                itemButton.onClick.AddListener(() => GetComponentInChildren<BuildingPlacementController>().SelectBuilding(product));

                CreateContentItemText(item, product);
            }
        }

        void CreateContentItemText(GameObject item, ProductionDataSO product)
        {
            GameObject itemTextObj = new GameObject();
            itemTextObj.transform.SetParent(item.transform);
            itemTextObj.AddComponent<CanvasRenderer>();

            TextMeshProUGUI itemText = itemTextObj.AddComponent<TextMeshProUGUI>();
            itemText.alignment = TextAlignmentOptions.Center;
            itemText.fontSize = 24.0f;
            itemText.rectTransform.sizeDelta = new Vector2(100, 50);
            itemText.rectTransform.localPosition = new Vector3(0, -75, 0);
            itemText.text = product.Name;
        }

        void OnDisable()
        {
            GameManager.Instance.OnGameBeginning -= CreateContentItem;
        }
    }
}