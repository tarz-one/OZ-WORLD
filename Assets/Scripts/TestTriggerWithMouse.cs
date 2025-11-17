using UnityEngine;
using UnityEngine.SceneManagement;

public class TestDoorknobTrigger : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "hallway";
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("=== SPACEBAR PRESSED ===");
            Debug.Log("Attempting to load scene: " + sceneToLoad);
            
            // Try to load the scene directly
            if (!string.IsNullOrEmpty(sceneToLoad))
            {
                SceneManager.LoadScene(sceneToLoad);
            }
            else
            {
                Debug.LogError("Scene name is empty!");
            }
        }
    }
}
