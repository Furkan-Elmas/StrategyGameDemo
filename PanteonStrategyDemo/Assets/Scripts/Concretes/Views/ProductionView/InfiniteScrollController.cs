using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace PanteonStrategyDemo.Concretes.UI.ProductionView
{
    public class InfiniteScrollController : MonoBehaviour, IBeginDragHandler, IDragHandler, IScrollHandler
    {
        [SerializeField] float _itemSpacing;
        [SerializeField] float _horizontalMargin, _verticalMargin;
        [SerializeField] bool _horizontal, _vertical;
        [SerializeField] float _outOfBoundsThreshold;
        ScrollContentData _scrollContentData;
        ScrollRect _scrollRect;
        Vector2 _lastDragPosition;
        bool _positiveDrag;

        public ScrollRect ScrollRect => _scrollRect;
        public float ItemSpacing => _itemSpacing;
        public float HorizontalMargin => _horizontalMargin;
        public float VerticalMargin => _verticalMargin;
        public bool Horizontal => _horizontal;
        public bool Vertical => _vertical;


        void Awake()
        {
            _scrollContentData = new ScrollContentData(this);
        }

        void Start()
        {
            _horizontal = !_vertical;

            _scrollRect = GetComponent<ScrollRect>();
            _scrollRect.vertical = Vertical;
            _scrollRect.horizontal = Horizontal;
            _scrollRect.movementType = ScrollRect.MovementType.Unrestricted;

            ContentDataRead();
        }

        public void ContentDataRead()
        {
            _scrollContentData.InitializeContentDataRead();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _lastDragPosition = eventData.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (Vertical)
            {
                _positiveDrag = eventData.position.y > _lastDragPosition.y;
            }
            else if (Horizontal)
            {
                _positiveDrag = eventData.position.x > _lastDragPosition.x;
            }

            _lastDragPosition = eventData.position;
        }

        public void OnScroll(PointerEventData eventData)
        {
            if (Vertical)
            {
                _positiveDrag = eventData.scrollDelta.y < 0;
            }
            else
            {
                // Scrolling up on the mouse wheel is considered a negative scroll, but I defined
                // scrolling downwards (scrolls right in a horizontal view) as the positive direciton,
                // so I check if the if scrollDelta.y is less than zero to check for a positive drag.
                _positiveDrag = eventData.scrollDelta.y > 0;
            }
        }

        public void OnViewScroll()
        {
            if (Vertical)
            {
                HandleVerticalScroll();
            }
            else
            {
                HandleHorizontalScroll();
            }
        }

        void HandleVerticalScroll()
        {
            int currItemIndex = _positiveDrag ? _scrollRect.content.childCount - 1 : 0;
            var currItem = _scrollRect.content.GetChild(currItemIndex);

            if (!ReachedThreshold(currItem))
            {
                return;
            }

            int endItemIndex = _positiveDrag ? 0 : _scrollRect.content.childCount - 1;
            Transform endItem = _scrollRect.content.GetChild(endItemIndex);
            Vector2 newPos = endItem.position;

            if (_positiveDrag)
            {
                newPos.y = endItem.position.y - _scrollContentData.ChildHeight * 1.5f + ItemSpacing;
            }
            else
            {
                newPos.y = endItem.position.y + _scrollContentData.ChildHeight * 1.5f - ItemSpacing;
            }

            currItem.position = newPos;
            currItem.SetSiblingIndex(endItemIndex);
        }

        void HandleHorizontalScroll()
        {
            int currItemIndex = _positiveDrag ? _scrollRect.content.childCount - 1 : 0;
            var currItem = _scrollRect.content.GetChild(currItemIndex);
            if (!ReachedThreshold(currItem))
            {
                return;
            }

            int endItemIndex = _positiveDrag ? 0 : _scrollRect.content.childCount - 1;
            Transform endItem = _scrollRect.content.GetChild(endItemIndex);
            Vector2 newPos = endItem.position;

            if (_positiveDrag)
            {
                newPos.x = endItem.position.x - _scrollContentData.ChildWidth * 1.5f + ItemSpacing;
            }
            else
            {
                newPos.x = endItem.position.x + _scrollContentData.ChildWidth * 1.5f - ItemSpacing;
            }

            currItem.position = newPos;
            currItem.SetSiblingIndex(endItemIndex);
        }

        private bool ReachedThreshold(Transform item)
        {
            if (Vertical)
            {
                float posYThreshold = transform.position.y + _scrollContentData.Height * 0.5f + _outOfBoundsThreshold;
                float negYThreshold = transform.position.y - _scrollContentData.Height * 0.5f - _outOfBoundsThreshold;
                return _positiveDrag ? item.position.y - _scrollContentData.ChildWidth * 0.5f > posYThreshold :
                    item.position.y + _scrollContentData.ChildWidth * 0.5f < negYThreshold;
            }
            else
            {
                float posXThreshold = transform.position.x + _scrollContentData.Width * 0.5f + _outOfBoundsThreshold;
                float negXThreshold = transform.position.x - _scrollContentData.Width * 0.5f - _outOfBoundsThreshold;
                return _positiveDrag ? item.position.x - _scrollContentData.ChildWidth * 0.5f > posXThreshold :
                    item.position.x + _scrollContentData.ChildWidth * 0.5f < negXThreshold;
            }
        }
    }
}