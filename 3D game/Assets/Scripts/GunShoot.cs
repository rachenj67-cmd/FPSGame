using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GunShoot : MonoBehaviour
{
    [Header("Basic Settings")]
    public Camera cam;
    public float range = 100f;
    public int damage = 5;

    [Header("Ammo Settings")]
    public int magSize = 30;
    public int currentAmmo;
    public int reserveAmmo = 90;      
    public int maxReserveAmmo = 180;  
    public float reloadTime = 2f;
    private bool isReloading = false;

    [Header("UI References")]
    public TextMeshProUGUI ammoText;
    public Slider reloadSlider;

    void Start()
    {
        currentAmmo = magSize;
        UpdateUI();
        if (reloadSlider != null) reloadSlider.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isReloading) return;

       
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < magSize && reserveAmmo > 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (currentAmmo > 0)
            {
                Shoot();
            }
            else if (reserveAmmo > 0)
            {
                StartCoroutine(Reload());
            }
        }
    }

    void Shoot()
    {
        currentAmmo--;
        UpdateUI();

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Debug.Log("Hit: " + hit.transform.name);
            ZombieHealth zombie = hit.transform.GetComponent<ZombieHealth>();
            if (zombie == null) zombie = hit.transform.GetComponentInParent<ZombieHealth>();

            if (zombie != null)
            {
                zombie.TakeDamage(damage);
            }
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;

        if (reloadSlider != null)
        {
            reloadSlider.gameObject.SetActive(true);
            float timer = 0;
            while (timer < reloadTime)
            {
                timer += Time.deltaTime;
                reloadSlider.value = timer / reloadTime;
                yield return null;
            }
            reloadSlider.gameObject.SetActive(false);
        }
        else
        {
            yield return new WaitForSeconds(reloadTime);
        }

      
        int ammoNeeded = magSize - currentAmmo;
        int ammoToFill = Mathf.Min(reserveAmmo, ammoNeeded);

        reserveAmmo -= ammoToFill;
        currentAmmo += ammoToFill;

        UpdateUI();
        isReloading = false;
    }

   
    public void UpdateUI()
    {
        if (ammoText != null)
        {
          
            ammoText.text = "Ammo: " + currentAmmo + " / " + reserveAmmo;
        }
    }

  
    public void AddAmmo(int amount)
    {
        reserveAmmo += amount;
        if (reserveAmmo > maxReserveAmmo) reserveAmmo = maxReserveAmmo;

        UpdateUI(); 
        Debug.Log("Ammo Added! Current Reserve: " + reserveAmmo);
    }
}