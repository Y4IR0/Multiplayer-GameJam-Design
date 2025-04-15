using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform muzzle; // Point from which bullet will be fired

    public float shootCooldown = 0.5f;
    private float lastShootTime = -Mathf.Infinity;

    private PlayerInput playerInput;
    private PlayerController playerController;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (playerController.gun.gameObject.activeSelf)
        {
            if (playerInput.actions["Attack"].WasPressedThisFrame() && Time.time >= lastShootTime + shootCooldown)
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        GameObject spawnedBullet = Instantiate(bullet, muzzle.position, muzzle.rotation);
        spawnedBullet.GetComponent<Bullet>().shooter = gameObject;
    }
}
