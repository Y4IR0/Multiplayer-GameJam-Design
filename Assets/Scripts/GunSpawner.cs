using UnityEngine;

public class GunSpawner : MonoBehaviour
{
    public Shooting.Gun selectedGun;

    [SerializeField] private SpriteRenderer pistolRenderer;
    [SerializeField] private SpriteRenderer machinegunRenderer;
    [SerializeField] private SpriteRenderer sniperRenderer;
    
    private void Start()
    {
        int number = Random.Range(0, 2);

        switch (number)
        {
            case 0:
                selectedGun = Shooting.Gun.Machinegun;
                break;
            case 1:
                selectedGun = Shooting.Gun.Sniper;
                break;
        }
        
        // Visual
        switch (selectedGun)
        {
            case Shooting.Gun.Pistol:
                pistolRenderer.enabled = true;
                machinegunRenderer.enabled = false;
                sniperRenderer.enabled = false;
                break;
            case Shooting.Gun.Machinegun:
                pistolRenderer.enabled = false;
                machinegunRenderer.enabled = true;
                sniperRenderer.enabled = false;
                break;
            case Shooting.Gun.Sniper:
                pistolRenderer.enabled = false;
                machinegunRenderer.enabled = false;
                sniperRenderer.enabled = true;
                break;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            collision.gameObject.GetComponent<Shooting>().currentGun = selectedGun;
            Destroy(gameObject);
        }
    }
}
