using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int baseDamage;
    public float range;
    Transform playerCamera;

    private void Awake()
    {
        playerCamera = transform.parent;
    }

    public void Attack()
    {
        RaycastHit raycastHit;
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out raycastHit, range))
        {
            Enemy enemy = raycastHit.transform.GetComponent<Enemy>();
            if (enemy != null)
                enemy.GetHealthSystem().Damage(baseDamage);
        }
    }
}
