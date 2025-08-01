using System.Collections;
using UnityEngine;

public class ThrownGuns : MonoBehaviour
{
    bool IsAWeapon = true;
    public Collider Hitbox;

    void OnCollisionEnter(Collision collision)
    {
        if (IsAWeapon == true)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                GameObject Enemy = collision.gameObject;
                Target target = Enemy.GetComponent<Target>();
                if (target != null)
                {
                    target.TakeDamage(100f);
                }
            }
        }
    }

    void Start()
    {
        StartCoroutine(StopBeingDangerous());
    }

    IEnumerator StopBeingDangerous()
    {
        yield return new WaitForSeconds(10f);
        IsAWeapon = false;
    }
}
