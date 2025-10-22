using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class AnimatedItem : MonoBehaviour
{
    [SerializeField] private float _mooveSpeed = 1f;
    [SerializeField] private float _rotateSpeed = 1f;
    private RectTransform _cachedTransform;
    private Rect _cachedParentRect;

    private void Start()
    {
        _cachedTransform = ((RectTransform)transform);
        _cachedParentRect = ((RectTransform)transform.parent).rect;

        AnimatePosition(_cachedTransform);
        AnimateRotation(_cachedTransform);

    }

    void AnimatePosition(RectTransform rect)
    {
        float itemX = rect.rect.width / 2;
        float itemY = rect.rect.height / 2;

        Vector2 randomPos = new Vector2(
            Random.Range(_cachedParentRect.xMin + itemX, _cachedParentRect.xMax - itemX),
            Random.Range(_cachedParentRect.yMin + itemY, _cachedParentRect.yMax - itemY)
        );

        rect.DOAnchorPos(randomPos, _mooveSpeed)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() => AnimatePosition(rect));
    }

    void AnimateRotation(RectTransform rect)
    {
        float randomAngle = Random.Range(0f, 360f);

        rect.DOLocalRotate(new Vector3(0, 0, randomAngle), _rotateSpeed, RotateMode.FastBeyond360)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() => AnimateRotation(rect));
    }
}
