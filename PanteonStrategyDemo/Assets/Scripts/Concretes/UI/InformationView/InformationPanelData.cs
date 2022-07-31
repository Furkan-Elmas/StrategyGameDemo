using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PanteonStrategyDemo.Concretes.UI.InformationView
{
    public class InformationPanelData : MonoBehaviour
    {
        Image _buildingImage;
        Image _unitImage;
        TextMeshProUGUI _buildingText;
        TextMeshProUGUI _unitText;

        public Image BuildingImage { get => _buildingImage; set => _buildingImage = value; }
        public Image UnitImage { get => _unitImage; set => _unitImage = value; }
        public TextMeshProUGUI BuildingText { get => _buildingText; set => _buildingText = value; }
        public TextMeshProUGUI UnitText { get => _unitText; set => _unitText = value; }

        void Awake()
        {
            BuildingImage = transform.GetChild(0).GetComponent<Image>();
            UnitImage = transform.GetChild(1).GetComponent<Image>();
            BuildingText = transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
            UnitText = transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
        }

        void Start()
        {
            BuildingImage.gameObject.SetActive(false);
            UnitImage.gameObject.SetActive(false);
        }
    }
}