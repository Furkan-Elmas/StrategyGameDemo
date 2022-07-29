using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PanteonStrategyDemo.Abstracts.InputSystem;

namespace PanteonStrategyDemo.Concretes.Controllers
{
    public class InformationPanelController : MonoBehaviour
    {
        InputData _inputData;

        Collider2D _objectCollider;
        SpriteRenderer _objectSprite;
        GameObject _informationPanel;
        RectTransform _panelTransform;
        Image _panelImage;
        TextMeshProUGUI _panelText;


        void Awake()
        {
            _inputData = new InputData();

            _objectCollider = GetComponent<Collider2D>();
            _objectSprite = GetComponent<SpriteRenderer>();
            _informationPanel = GameObject.FindGameObjectWithTag("Information Panel");
            _panelTransform = _informationPanel.GetComponent<RectTransform>();
            _panelImage = _informationPanel.transform.GetChild(0).GetComponent<Image>();
            _panelText = _informationPanel.transform.GetComponentInChildren<TextMeshProUGUI>();
        }

        void Update()
        {
            BuildingClickControl();
        }

        void BuildingClickControl()
        {
            if (_inputData.LeftClickCheck)
            {
                _objectSprite.color = Color.white;
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(_inputData.MousePosition);
                RaycastHit2D rayHit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity);

                if (rayHit.collider == null)
                    return;

                if (rayHit.collider != _objectCollider)
                    return;

                if (_informationPanel != null)
                    _panelTransform.anchoredPosition = new Vector2(0, _panelTransform.anchoredPosition.y);

                _panelImage.sprite = rayHit.transform.GetComponent<SpriteRenderer>().sprite;
                _panelText.text = _panelImage.sprite.name;
                _objectSprite.color = Color.red;
            }
        }
    }
}