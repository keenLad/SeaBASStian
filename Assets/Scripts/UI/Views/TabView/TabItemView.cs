using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TabItemView : MonoBehaviour
{
    [SerializeField] private GameObject _contentView;
    [SerializeField] private Toggle _toggle;
    [SerializeField] private ColorBlock _transitions;
   

    public UnityEvent<bool> OnStateChanged;

    private void Start()
    {
        if (null == _toggle)
        {
            _toggle = GetComponent<Toggle>();
        }

        _toggle.onValueChanged.AddListener(OnStateChangedHandler);

        OnStateChangedHandler(_toggle.isOn);
    }

    private void OnStateChangedHandler(bool isActive)
    {
        Debug.Log($"[TabItemView] {name} OnStateChangedHandler");

        if (isActive)
        {
            _toggle.targetGraphic.color = Color.Lerp(_transitions.selectedColor, _transitions.normalColor, _toggle.colors.fadeDuration);
        }
        else
        {
            _toggle.targetGraphic.color = Color.Lerp(_transitions.normalColor, _transitions.selectedColor, _toggle.colors.fadeDuration);
        }

        if(null != _contentView)
            _contentView.SetActive(isActive);

        OnStateChanged?.Invoke(isActive);
    }


}
