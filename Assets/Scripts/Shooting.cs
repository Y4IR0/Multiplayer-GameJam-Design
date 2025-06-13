using UnityEngine;
using UnityEngine.InputSystem;
using SmoothShakeFree;
using System.Collections;

public class Shooting : MonoBehaviour
{
    public enum Gun
    {
        Pistol,
        Machinegun,
        Sniper
    }
    
    public GameObject bullet;
    public Transform muzzle; // Point from which bullet will be fired
    
    private float lastShootTime = -Mathf.Infinity;

    private PlayerInput playerInput;
    private PlayerController playerController;

    public Gun currentGun = Gun.Pistol;

    [SerializeField] private SpriteRenderer pistolRenderer;
    [SerializeField] private SpriteRenderer machinegunRenderer;
    [SerializeField] private SpriteRenderer sniperRenderer;
    
    private SmoothShake smoothShake;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerController = GetComponent<PlayerController>();
        smoothShake = transform.GetChild(4).GetComponent<SmoothShake>();
    }

    void Update()
    {
        if (playerController.gun.gameObject.activeSelf)
        {
            switch (currentGun)
            {
                case Gun.Pistol:
                    if (playerInput.actions["Attack"].WasPressedThisFrame() && Time.time >= lastShootTime + .5f)
                        StartCoroutine(Shoot(1, 20, 1));
                    break;
                case Gun.Machinegun:
                    if (playerInput.actions["Attack"].WasPressedThisFrame() && Time.time >= lastShootTime + .3f)
                        StartCoroutine(Shoot(1, 20, 3));
                    break;
                case Gun.Sniper:
                    if (playerInput.actions["Attack"].WasPressedThisFrame() && Time.time >= lastShootTime + .8f)
                        StartCoroutine(Shoot(2, 80, 1));
                    break;
            }
        }
        
        // Visual
        switch (currentGun)
        {
            case Gun.Pistol:
                pistolRenderer.enabled = true;
                machinegunRenderer.enabled = false;
                sniperRenderer.enabled = false;
                break;
            case Gun.Machinegun:
                pistolRenderer.enabled = false;
                machinegunRenderer.enabled = true;
                sniperRenderer.enabled = false;
                break;
            case Gun.Sniper:
                pistolRenderer.enabled = false;
                machinegunRenderer.enabled = false;
                sniperRenderer.enabled = true;
                break;
        }

        if (playerController.gun.GetComponent<SpriteHider>().hideChildrenSprites == true)
        {
            pistolRenderer.enabled = false;
            machinegunRenderer.enabled = false;
            sniperRenderer.enabled = false;
        }
    }

    private IEnumerator Shoot(int damage = 1, float speed = 20, float burstShots = 1)
    {
        for (int i = 0; i < burstShots; i++)
        {
            GameObject spawnedBullet = Instantiate(bullet, muzzle.position, muzzle.rotation);
            Bullet bulletComponent = spawnedBullet.GetComponent<Bullet>();
            bulletComponent.shooter = gameObject;
            bulletComponent.damage = damage;
            bulletComponent.speed = speed;

            smoothShake.StartShake();
            yield return new WaitForSeconds(.03f);
        }
    }
}
