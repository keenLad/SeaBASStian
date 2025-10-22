using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ListItemView : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;

    public string text;
    public float Height
    {
        get => ((RectTransform)transform).rect.height;
        set => ((RectTransform)transform).sizeDelta = new Vector2(((RectTransform)transform).sizeDelta.x, value);
    }

    public float Width
    {
        get => ((RectTransform)transform).rect.width;
        set => ((RectTransform)transform).sizeDelta = new Vector2(value, ((RectTransform)transform).sizeDelta.y);
    }

    private void Start()
    {
        if(null == _label)
        {
            _label = GetComponent<TMP_Text>();
        }

        _label.text = text;
    }
}
