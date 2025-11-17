using UnityEngine;
using UnityEngine.SceneManagement;

public class ColliderSceneTransition : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] private string sceneToLoad = "NextScene";
    [SerializeField] private float transitionDelay = 0.5f;
    
    [Header("Optional Transition Effects")]
    [SerializeField] private bool fadeTransition = true;
    [SerializeField] private float fadeTime = 1f;
    
    [Header("Trigger Settings")]
    [SerializeField] private string handTag = "Hand";
    [SerializeField] private bool triggerOnce = true;
    
    private bool hasTriggered = false;
    
    void Start()
    {
        Collider col = GetComponent<Collider>();
        if (col == null)
        {
            Debug.LogError("ColliderSceneTransition: No Collider found on " + gameObject.name);
            return;
        }
        
        if (!col.isTrigger)
        {
            Debug.LogWarning("ColliderSceneTransition: Collider is not set as trigger. Setting it now.");
            col.isTrigger = true;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered trigger: " + other.name + " (Tag: " + other.tag + ")");
        
        if (triggerOnce && hasTriggered)
            return;
        
        if (other.CompareTag(handTag) || 
            other.name.Contains("Hand") || 
            other.name.Contains("Controller"))
        {
            hasTriggered = true;
            Debug.Log("HAND DETECTED! Transitioning to scene: " + sceneToLoad);
            
            if (string.IsNullOrEmpty(sceneToLoad))
            {
                Debug.LogError("Scene name is empty! Please set the scene name in the inspector.");
                return;
            }
            
            if (transitionDelay > 0)
            {
                Invoke(nameof(LoadScene), transitionDelay);
            }
            else
            {
                LoadScene();
            }
        }
        else
        {
            Debug.Log("Not recognized as hand - skipping");
        }
    }
    
    private void LoadScene()
    {
        var grabAndLocates = FindObjectsOfType<Meta.XR.MRUtilityKit.BuildingBlocks.GrabAndLocate>();
        foreach (var component in grabAndLocates)
        {
            if (component != null)
            {
                component.enabled = false;
            }
        }
        
        if (fadeTransition)
        {
            StartCoroutine(FadeAndLoadScene());
        }
        else
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
    
    private System.Collections.IEnumerator FadeAndLoadScene()
    {
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(sceneToLoad);
    }
    
    private void OnDrawGizmos()
    {
        BoxCollider boxCol = GetComponent<BoxCollider>();
        if (boxCol != null)
        {
            Gizmos.color = Color.green;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(boxCol.center, boxCol.size);
        }
    }
}