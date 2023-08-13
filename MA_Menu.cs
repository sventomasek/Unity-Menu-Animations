using UnityEngine;

[RequireComponent(typeof(CanvasGroup))] // This will automatically add a CanvasGroup to the GameObject
public class MA_Menu : MonoBehaviour
{
    [SerializeField] [Range(0, 30)] float fadeSpeed = 5;
    [SerializeField] [Range(0, 30)] float scaleXSpeed = 5;
    [SerializeField] [Range(0, 30)] float scaleYSpeed = 5;
    [SerializeField] [Range(0, 30)] float spinSpeed = 5;

    [System.Flags] // This line allows multiple options to be selected
    [SerializeField] enum AnimationType
    {
        None = 0,
        AlphaFade = 1, // Fades in/out the menu by changing the Alpha of the CanvasGroup
        ScaleX = 1 << 1, // Scales the menu on the X axis
        ScaleY = 1 << 2, // Scales the menu on the Y axis
        SpinClockwise = 1 << 3, // Spins the menu clockwise
        SpinCounterclockwise = 1 << 4 // Spins the menu counterclockwise
    }
    [SerializeField] AnimationType animationType = AnimationType.AlphaFade;

    public bool showMenu;
    CanvasGroup canvasGroup;

    Vector3 defaultScale;
    Quaternion defaultRotation;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        defaultScale = transform.localScale;
        defaultRotation = transform.localRotation;
    }

    void Update()
    {
        if (showMenu) // Show the Menu
        {
            // These are used to make sure the Player can't interact with the Menu when it's hidden
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;

            /// Animations
            // Increase the alpha value of the CanvasGroup
            if (animationType.HasFlag(AnimationType.AlphaFade)) {
                canvasGroup.alpha += fadeSpeed * Time.unscaledDeltaTime;
            }
            
            // Linearly interpolates the current X scale to the defaultScale
            if (animationType.HasFlag(AnimationType.ScaleX)) {
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(defaultScale.x, transform.localScale.y, transform.localScale.z), scaleXSpeed * Time.unscaledDeltaTime);
            }

            // Linearly interpolates the current Y scale to the defaultScale
            if (animationType.HasFlag(AnimationType.ScaleY)) {
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(transform.localScale.x, defaultScale.y, transform.localScale.z), scaleYSpeed * Time.unscaledDeltaTime);
            }

            // Linearly interpolates the current Z rotation to 0
            if (animationType.HasFlag(AnimationType.SpinClockwise) || animationType.HasFlag(AnimationType.SpinCounterclockwise)) {
                transform.localRotation = Quaternion.Lerp(transform.localRotation, new Quaternion(defaultRotation.x, defaultRotation.y, 0, defaultRotation.w), spinSpeed * Time.unscaledDeltaTime);
            }
        }
        else // Hide the Menu
        {
            // These are used to make sure the Player can't interact with the Menu when it's hidden
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            /// Animations
            // Decrease the alpha value of the CanvasGroup
            if (animationType.HasFlag(AnimationType.AlphaFade)) {
                canvasGroup.alpha -= fadeSpeed * Time.unscaledDeltaTime;
            }
            
            // Linearly interpolates the current X scale to 0
            if (animationType.HasFlag(AnimationType.ScaleX)) {
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0, transform.localScale.y, transform.localScale.z), scaleXSpeed * Time.unscaledDeltaTime);
            }

            // Linearly interpolates the current Y scale to 0
            if (animationType.HasFlag(AnimationType.ScaleY)) {
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(transform.localScale.x, 0, transform.localScale.z), scaleYSpeed * Time.unscaledDeltaTime);
            }

            // Linearly interpolates the current Z rotation to 360, which will make the rotation Clockwise
            if (animationType.HasFlag(AnimationType.SpinClockwise)) {
                transform.localRotation = Quaternion.Lerp(transform.localRotation, new Quaternion(defaultRotation.x, defaultRotation.y, 360, defaultRotation.w), spinSpeed / 500 * Time.unscaledDeltaTime);
            }

            // Linearly interpolates the current Z rotation to -360, which will make the rotation Counterclockwise
            if (animationType.HasFlag(AnimationType.SpinCounterclockwise)) {
                transform.localRotation = Quaternion.Lerp(transform.localRotation, new Quaternion(defaultRotation.x, defaultRotation.y, -360, defaultRotation.w), spinSpeed / 500 * Time.unscaledDeltaTime);
            }
        }
    }
}
