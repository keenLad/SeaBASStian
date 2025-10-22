using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class ListView : InitialisableBase
{
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private ListItemView _itemPrefab;

    [SerializeField] float _spacing = 3;
    [SerializeField] float _itemHeight = 30;

    [SerializeField] int _itemsCount = 1000;
    [SerializeField] int _blockSize = 100;
    [SerializeField] int _boundaryItemsCount = 5;

    private List<GameObject> _activeItems = new List<GameObject>();

    private int visibleItemCount;
    private int firstVisibleIndex = -1;
    private int lastVisibleIndex = -1;

    private void Awake()
    {
        if(null == _scrollRect)
        {
            _scrollRect = GetComponent<ScrollRect>();
        }

        _scrollRect.onValueChanged.AddListener(OnScrollHandler);
    }

    override public async UniTask Init(CancellationToken token)
    {
        _scrollRect.content.ClearChilds();

        var totalSize= new Vector2(_scrollRect.content.sizeDelta.x, 0);

        for (int i = 0; i < _itemsCount; i++)
        {
            token.ThrowIfCancellationRequested();

            var item = Instantiate(_itemPrefab, _scrollRect.content, false);
            item.text = i.ToString();

            SetItem(item, i);

            totalSize.y += item.Height;
            if(_itemsCount > 1 && i != _itemsCount-1)
            {
                totalSize.y += _spacing;
            }
            _scrollRect.content.sizeDelta = totalSize;


            if (i > 0 && i % _blockSize == 0)
            {
                UpdateActive();
                await UniTask.Yield();
            }
        }

        UpdateActive();
    }

    private void SetItem(ListItemView item, int index)
    {
        RectTransform cachedTransform = item.transform as RectTransform;

        if(null == cachedTransform)
        {
            return;
        }

        item.gameObject.SetActive(false);

        float yPos = -index * (_itemHeight + _spacing);
        cachedTransform.anchoredPosition = new Vector2(0, yPos);
    }

    private void OnScrollHandler(Vector2 pos)
    {
        UpdateActive();
    }

    private void UpdateActive()
    {
        visibleItemCount = Mathf.CeilToInt(_scrollRect.viewport.rect.height / (_itemHeight + _spacing)) + _boundaryItemsCount * 2;

        float scrollPosition = _scrollRect.content.anchoredPosition.y;
        int newFirstVisible = Mathf.Max(0, Mathf.FloorToInt(scrollPosition / (_itemHeight + _spacing)) - _boundaryItemsCount);
        int newLastVisible = Mathf.Min(_itemsCount - 1, newFirstVisible + visibleItemCount);

        if (newFirstVisible == firstVisibleIndex && newLastVisible == lastVisibleIndex)
            return;

        firstVisibleIndex = newFirstVisible;
        lastVisibleIndex = newLastVisible;

        foreach(var item in _activeItems)
        {
            item.SetActive(false);
        }

        _activeItems.Clear();

        for (int i = firstVisibleIndex; i <= lastVisibleIndex; i++)
        {
            var item = _scrollRect.content.GetChild(i).gameObject;
            item.SetActive(true);
            _activeItems.Add(item);
        }
    }

    private void OnDestroy()
    {
        if (_scrollRect != null)
        {
            _scrollRect.onValueChanged.RemoveListener(OnScrollHandler);
        }
    }

}
