using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class AnimatedContentView : InitialisableBase
{
    [SerializeField] int _objectsCount = 100;
    [SerializeField] GameObject _itemPrefab;
    [SerializeField] int _blockCount = 20;

    private Rect _currentRect;

    private void Awake()
    {
        _currentRect = ((RectTransform)transform).rect;
    }

    override public async UniTask Init(CancellationToken token)
    {

        for (int i = 0; i < _objectsCount; i++)
        {
            token.ThrowIfCancellationRequested();

            var item = Instantiate(_itemPrefab, transform, false);

            RectTransform cachedTransform = ((RectTransform)item.transform);
            cachedTransform.anchoredPosition = GetRandomPosition(cachedTransform);
            var image = item.GetComponent<Image>();

            image.color = GetRandomColor();
            image.raycastTarget = false;

            RotateToRandomAngle(cachedTransform);

            item.SetActive(true);

            if (i != 0 && i % _blockCount == 0)
            {
                await UniTask.Yield();
            }
        }
    }


    public Vector2 GetRandomPosition(RectTransform item)
    {
        float itemX = item.rect.width / 2;
        float itemY = item.rect.height / 2;

        float randomX = Random.Range(_currentRect.xMin + itemX, _currentRect.xMax - itemX);
        float randomY = Random.Range(_currentRect.yMin + itemY, _currentRect.yMax - itemY);

        return new Vector2(randomX, randomY);
    }

    public Color GetRandomColor()
    {
        return new Color(Random.value, Random.value, Random.value);
    }

    public void RotateToRandomAngle(RectTransform item)
    {
        float randomAngle = Random.Range(0f, 360f);
        item.rotation = Quaternion.Euler(0, 0, randomAngle);
    }
}
