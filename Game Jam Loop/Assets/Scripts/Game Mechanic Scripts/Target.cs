using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float Health = 100;
    public GameObject Enemy;
    public TimeRewind TimeRewind;
    [SerializeField] private float TimeStopTime = 0;
    [SerializeField] private float DeathTime = 0;
    private bool CountTime = false;

    void Start()
    {
        TimeRewind timeRewind = GetComponent<TimeRewind>();
    }

    public void TakeDamage(float amount)
    {
        Health -= amount;
        if (Health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Enemy.SetActive(false);
        CountTime = true;
    }

    void FixedUpdate()
    {
        if (TimeRewind.IsRewinding == true)
        {
            TimeStopTime = TimeStopTime + Time.deltaTime;

            if (TimeRewind.IsRewinding)
            {
                StartCoroutine(WaitToSpawn());
            }
        }

        if (CountTime == true)
        {
            DeathTime = DeathTime + Time.deltaTime;

            if (DeathTime > 5)
            {
                Destroy(Enemy);
            }
        }
    }

    IEnumerator WaitToSpawn()
    {
        yield return new WaitForSeconds(DeathTime);

        Enemy.SetActive(true);
        CountTime = false;
    }
}
