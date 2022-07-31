using UnityEngine;

namespace PanteonStrategyDemo.Concretes.UI.ProductView
{
    public class ScrollContentData
    {
        InfiniteScrollController _scrollController;
        RectTransform _rectTransform;
        RectTransform[] _rtChildren;
        float _width, _height;
        float _childWidth, _childHeight;

        public float Width => _width;
        public float Height => _height;
        public float ChildWidth => _childWidth;
        public float ChildHeight => _childHeight;
        

        public ScrollContentData(InfiniteScrollController scrollController)
        {
            _scrollController = scrollController;
        }

        public void InitializeContentDataRead()
        {
            _rectTransform = _scrollController.ScrollRect.content.GetComponent<RectTransform>();
            _rtChildren = new RectTransform[_rectTransform.childCount];

            for (int i = 0; i < _rectTransform.childCount; i++)
            {
                _rtChildren[i] = _rectTransform.GetChild(i) as RectTransform;
            }

            // Subtract the margin from both sides.
            _width = _rectTransform.rect.width - (2 * _scrollController.HorizontalMargin);

            // Subtract the margin from the top and bottom.
            _height = _rectTransform.rect.height - (2 * _scrollController.VerticalMargin);

            _childWidth = _rtChildren[0].rect.width;
            _childHeight = _rtChildren[0].rect.height;
            
            if (_scrollController.Vertical)
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
                childPos.x = originX + posOffset + i * (_childWidth + _scrollController.ItemSpacing);
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
                childPos.y = originY + posOffset + i * (_childHeight + _scrollController.ItemSpacing);
                _rtChildren[i].localPosition = childPos;
            }
        }
    }
}