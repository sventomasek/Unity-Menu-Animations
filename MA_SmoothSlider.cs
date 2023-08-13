using UnityEngine;
using UnityEngine.UI;

public class MA_SmoothSlider : MonoBehaviour
{
    [SerializeField] float fillSpeed = 1;

    private Slider slider;
    private RectTransform fillRect;
    private float targetValue = 0;
    private float currentValue = 0;

    void Awake () 
    {
        slider = GetComponent<Slider>();
        if (slider == null) return;

        // Adds a listener to the main slider and invokes a method when the value changes
        slider.onValueChanged.AddListener (delegate {ValueChange ();});

        fillRect = slider.fillRect;
        targetValue = currentValue = slider.value;
    }

    void Update () 
    {
        if (slider == null) return;
        currentValue = Mathf.MoveTowards(currentValue, targetValue, Time.unscaledDeltaTime * fillSpeed);
    
        Vector2 fillAnchor = fillRect.anchorMax;
        fillAnchor.x = Mathf.Clamp01(currentValue / slider.maxValue);
        fillRect.anchorMax = fillAnchor;
    }

    // Runs when the value of the slider changes
    public void ValueChange()
    {
        if (slider == null) return;
        targetValue = slider.value;
    }
}
