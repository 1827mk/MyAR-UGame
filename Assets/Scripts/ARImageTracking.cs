using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARImageTracking : MonoBehaviour
{
    [SerializeField]
    public ARTrackedImageManager m_TrackedImageManager;
    
    [SerializeField]
    public List<GameObject> prefabOptions;
    public GameObject selectedPrefab;
    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();

    void OnEnable()
    {
        if (m_TrackedImageManager == null)
        {
            Debug.LogError("ARTrackedImageManager is not assigned in ARImageTracking!");
            return;
        }
       m_TrackedImageManager.trackablesChanged.AddListener(OnChanged);
    }
    void OnDisable() => m_TrackedImageManager.trackablesChanged.RemoveListener(OnChanged);

    void OnChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            Debug.Log("Tracked image added: " + trackedImage.name);
            SpawnPrefab(trackedImage);
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            UpdatePrefabPosition(trackedImage);
        }
    }

    private void SpawnPrefab(ARTrackedImage trackedImage)
    {
        if (!spawnedPrefabs.ContainsKey(trackedImage.referenceImage.name))
        {
            var prefab = Instantiate(selectedPrefab, trackedImage.transform.position, Quaternion.identity);
            spawnedPrefabs[trackedImage.referenceImage.name] = prefab;
        }
    }

    private void UpdatePrefabPosition(ARTrackedImage trackedImage)
    {
        if (spawnedPrefabs.TryGetValue(trackedImage.referenceImage.name, out var prefab))
        {
            prefab.transform.position = trackedImage.transform.position;
        }
    }
}
