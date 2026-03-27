using UnityEngine;
using UnityEngine.UI; // ต้องมีอันนี้เพื่อใช้ Slider
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("UI Settings")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI healthText; // สำหรับตัวเลข (ถ้าไม่ใช้ก็ปล่อยว่าง)
    public Slider healthSlider;        // สำหรับหลอดเลือด (เอา Slider มาใส่ช่องนี้!)

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;

        // ตั้งค่าหลอดเลือดตอนเริ่มเกม
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = maxHealth;
        }

        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        UpdateUI();
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        UpdateUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateUI()
    {
        // อัปเดตตัวเลข
        if (healthText != null)
        {
            healthText.text = "HP: " + currentHealth.ToString("0");
        }

        // อัปเดตหลอดเลือด (Slider)
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}