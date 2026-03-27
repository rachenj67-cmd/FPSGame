using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenu : MonoBehaviour
{
   
    public void PlayGame()
    {
       
       
        SceneManager.LoadScene("SampleScene");
    }

    // ฟังก์ชันสำหรับปุ่ม Exit
    public void QuitGame()
    {
        Debug.Log("Game Exited"); 
        Application.Quit(); 
    }
}