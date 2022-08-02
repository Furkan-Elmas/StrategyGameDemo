using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PanteonStrategyDemo.Concretes.UI.InformationView
{
    public class InformationPanelData : MonoBehaviour
    {
        Image _productImage;
        Image _unitImage;
        TextMeshProUGUI _productText;
        TextMeshProUGUI _unitText;

        public Image ProductImage { get => _productImage; set => _productImage = value; }
        public Image UnitImage { get => _unitImage; set => _unitImage = value; }
        public TextMeshProUGUI ProductText { get => _productText; set => _productText = value; }
        public TextMeshProUGUI UnitText { get => _unitText; set => _unitText = value; }

        void Awake()
        {
            ProductImage = transform.GetChild(0).GetComponent<Image>();
            UnitImage = transform.GetChild(1).GetComponent<Image>();
            ProductText = transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
            UnitText = transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
        }

        void Start()
        {
            ProductImage.gameObject.SetActive(false);
            UnitImage.gameObject.SetActive(false);
        }
    }
}