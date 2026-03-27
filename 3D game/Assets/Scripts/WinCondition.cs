using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    [Header("UI Settings")]
    public GameObject winUI;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            Win();
        }
    }

    void Win()
    {
        if (winUI != null) winUI.SetActive(true); 

        Time.timeScale = 0f; 

       
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}