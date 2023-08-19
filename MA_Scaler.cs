using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MA_Scaler : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Tooltip("NOTE: If you have this on a button with text, make sure the text is not a raycast target, otherwise it won't work!")]
    [SerializeField] bool selectOnMouseOver = true;
    [Space]
    [SerializeField] float scaleSpeed = 5f;
    [SerializeField] float scaleAmount = 1.075f;

    bool selected;
    bool mouseOver;
    Vector3 startScale;

    void Start()
    {
        startScale = transform.localScale;
    }

    void Update()
    {
        Vector3 currentScale;
        currentScale = selected || mouseOver ? startScale * scaleAmount : startScale;
        transform.localScale = Vector3.Lerp(transform.localScale, currentScale, scaleSpeed * Time.unscaledDeltaTime);
    }

    public void OnSelect(BaseEventData eventData)
    {
        selected = true;
    }

    public void OnDeselect(BaseEventData data)
    {
        selected = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
        if (selectOnMouseOver) EventSystem.current.SetSelectedGameObject(eventData.pointerCurrentRaycast.gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
        if (selectOnMouseOver) EventSystem.current.SetSelectedGameObject(null);
    }
}
