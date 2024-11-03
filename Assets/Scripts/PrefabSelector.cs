using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PrefabSelector : MonoBehaviour
{
    [SerializeField]
    public ARImageTracking arImageTracking;

    [SerializeField]
    public TMP_Dropdown prefabDropdown; // Use TMP_Dropdown for TextMeshPro

    void Start()
    {
        prefabDropdown.onValueChanged.AddListener(OnPrefabSelected);
        PopulateDropdown();
    }

    void PopulateDropdown()
    {
        prefabDropdown.options.Clear();
        foreach (var prefab in arImageTracking.prefabOptions)
        {
            prefabDropdown.options.Add(new TMP_Dropdown.OptionData(prefab.name));
        }
    }

    void OnPrefabSelected(int index)
    {
        Debug.Log("Selected prefab: " + arImageTracking.prefabOptions[index].name);
        arImageTracking.selectedPrefab = arImageTracking.prefabOptions[index];
    }
}

