using UnityEngine;
using UnityEngine.EventSystems;

public class MA_Keybinds : MonoBehaviour
{
    [System.Serializable]
    public class Keybinds
    {
        public KeyCode key;
        public enum KeyType { Toggle, Hide, Show }
        public KeyType keyType = KeyType.Toggle;

        public MA_Menu menuToToggle;
        public GameObject elementToSelect;
    }

    public Keybinds[] keybinds;

    void Update()
    {
        foreach (Keybinds keybind in keybinds)
        {
            if (Input.GetKeyDown(keybind.key))
            {
                // Shows/Hides the Menu
                if (keybind.menuToToggle != null)
                {
                    if (keybind.keyType == Keybinds.KeyType.Toggle) keybind.menuToToggle.showMenu = !keybind.menuToToggle.showMenu;
                    else if (keybind.keyType == Keybinds.KeyType.Hide) keybind.menuToToggle.showMenu = false;
                    else if (keybind.keyType == Keybinds.KeyType.Show) keybind.menuToToggle.showMenu = true;
                }

                // Selects a UI Element (if you open the pause menu, select the Continue button)
                if (keybind.elementToSelect != null) EventSystem.current.SetSelectedGameObject(keybind.elementToSelect);
            }
        }
    }
}
