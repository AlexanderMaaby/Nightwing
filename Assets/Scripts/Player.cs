using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Player variables
    public int maxHealth = 100;
    public int currentHealth;

    public int mintleaves;

    //UI elements that belong to the player
    public Text spellamount;
    public Image spellborder;
    public GameObject inventory;

    //Health bar and mana bar
    public ValueBar healthBar;

    //Spell components and related variables
    public GameObject m_Projectile;
    public GameObject z_Projectile;
    public GameObject vfxProjectile;
    public Transform m_SpawnTransform;
    public float projectileSpeed = 30;

    //Raycast attack variables
    public Camera cam;
    private Vector3 destination;
    public float arcRange = 1;

    //healing aura variables
    public GameObject healingAuraVFX;



    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        mintleaves = 10;
        spellamount.text = "" + mintleaves;
        healthBar.setMaxHealth(currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ShootProjectile();
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            TakeDamage(10);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (mintleaves > 0)
            {
                CastHealingWave();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CastSunlightCurse();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CastHealingAura();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
        //Just a spell to remove health for testing purposes! REMOVE this later.
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            currentHealth -= 20;
        }
        healthBar.SetHealth(currentHealth);
    }

    //A reworked mouse 1 spell attack. 
    void ShootProjectile()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            destination = hit.point;
        else
            destination = ray.GetPoint(1000);

        InstantiateProjectile(m_SpawnTransform);
    }
    
    void CastHealingAura()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 100f))
        {
            Vector3 hitpoint = hit.point;
            hitpoint.y += 0.3f;
            var impact = Instantiate(healingAuraVFX, hitpoint, Quaternion.identity) as GameObject;
            Destroy(impact, 10);
        }
        //actually healing!
        TakeDamage(-20);
        
    }

    //Healing wave spell, only casts if the player has picked up enough mintleaves.
    void CastHealingWave()
    {
        Instantiate(m_Projectile, m_SpawnTransform.position, m_SpawnTransform.rotation);
        mintleaves -= 1;
        spellamount.text = "" + mintleaves;
        if (mintleaves == 0)
        {
            spellborder.GetComponent<Image>().color = new Color32(211, 211, 211, 100);
        }
    }

    void InstantiateProjectile(Transform firePoint)
    {
        var projectileObj = Instantiate(vfxProjectile, firePoint.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * projectileSpeed;
        iTween.PunchPosition(projectileObj, new Vector3(Random.Range(arcRange, arcRange), Random.Range(arcRange, arcRange), 0), Random.Range(0.5f, 2));
    }

    
    void CastSunlightCurse()
    {
        Instantiate(z_Projectile, m_SpawnTransform.position, m_SpawnTransform.rotation);
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    //Toggles weather the Inventory gameobject is visible or not.
    void ToggleInventory()
    {
        if (inventory.gameObject.activeInHierarchy == true)
        {
            inventory.gameObject.SetActive(false);
        }
        else
            inventory.gameObject.SetActive(true);
    }
}
