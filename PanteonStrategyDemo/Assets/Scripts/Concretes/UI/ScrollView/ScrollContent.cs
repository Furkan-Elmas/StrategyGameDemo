using UnityEngine;

namespace PanteonStrategyDemo.Concretes.UI
{
    public class ScrollContent : MonoBehaviour
    {
        [SerializeField] float _itemSpacing;
        [SerializeField] float _horizontalMargin, _verticalMargin;
        [SerializeField] bool _horizontal, _vertical;
        RectTransform _rectTransform;
        RectTransform[] _rtChildren;
        float _width, _height;
        float _childWidth, _childHeight;

        public float ItemSpacing => _itemSpacing;
        public float HorizontalMargin => _horizontalMargin;
        public float VerticalMargin => _verticalMargin;
        public bool Horizontal => _horizontal;
        public bool Vertical => _vertical;
        public float Width => _width;
        public float Height => _height;
        public float ChildWidth => _childWidth;
        public float ChildHeight => _childHeight;


        void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _rtChildren = new RectTransform[_rectTransform.childCount];

            for (int i = 0; i < _rectTransform.childCount; i++)
            {
                _rtChildren[i] = _rectTransform.GetChild(i) as RectTransform;
            }

            // Subtract the margin from both sides.
            _width = _rectTransform.rect.width - (2 * _horizontalMargin);

            // Subtract the margin from the top and bottom.
            _height = _rectTransform.rect.height - (2 * _verticalMargin);

            _childWidth = _rtChildren[0].rect.width;
            _childHeight = _rtChildren[0].rect.height;

            _horizontal = !_vertical;
            if (_vertical)
                InitializeContentVertical();
            else
                InitializeContentHorizontal();
        }

        void InitializeContentHorizontal()
        {
            float originX = 0 - (_width * 0.5f);
            float posOffset = _childWidth * 0.5f;
            for (int i = 0; i < _rtChildren.Length; i++)
            {
                Vector2 childPos = _rtChildren[i].localPosition;
                childPos.x = originX + posOffset + i * (_childWidth + _itemSpacing);
                _rtChildren[i].localPosition = childPos;
            }
        }

        void InitializeContentVertical()
        {
            float originY = 0 - (_height * 0.5f);
            float posOffset = _childHeight * 0.5f;
            for (int i = 0; i < _rtChildren.Length; i++)
            {
                Vector2 childPos = _rtChildren[i].localPosition;
                childPos.y = originY + posOffset + i * (_childHeight + _itemSpacing);
                _rtChildren[i].localPosition = childPos;
            }
        }
    }
}