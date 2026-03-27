using UnityEngine;
using TMPro;

public class AmmoPickup : MonoBehaviour
{
    public int ammoAmount = 30;
    public GameObject uiPrompt;

    private bool canPickup = false;
    private GunShoot playerGun;

    void Start()
    {
        if (uiPrompt != null) uiPrompt.SetActive(false);
    }

    void Update()
    {
        
        if (canPickup && Input.GetKeyDown(KeyCode.E))
        {
            if (playerGun != null)
            {
                playerGun.AddAmmo(ammoAmount); 
                playerGun.UpdateUI();         

                if (uiPrompt != null) uiPrompt.SetActive(false);
                Destroy(gameObject); 
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = true;
            playerGun = other.GetComponentInChildren<GunShoot>();
            if (uiPrompt != null) uiPrompt.SetActive(true); 
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = false;
            if (uiPrompt != null) uiPrompt.SetActive(false); 
        }
    }
}