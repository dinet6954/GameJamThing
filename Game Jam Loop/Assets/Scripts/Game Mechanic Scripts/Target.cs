using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Profiling;
using System.Collections;

public class Target : MonoBehaviour
{
    public float Health = 100;
    public GameObject Enemy;
    public TimeRewind TimeRewind;
    [SerializeField] private float TimeStopCount;
    [SerializeField] private float TimeSinceDeath;
    bool Dead;
    List<float> health;

    void Start()
    {
        TimeRewind timeRewind = GetComponent<TimeRewind>();
        health = new List<float>();
    }

    public void TakeDamage(float amount)
    {
        Health -= amount;
        if (Health <= 0f)
        {
            TempDie();
        }
    }

    void TempDie()
    {
        Enemy.GetComponent<Renderer>().enabled = false;
        //Disable AI too, otherwise this will make an invisible unkillable threat
        Dead = true;
    }

    void FixedUpdate()
    {

        if (TimeRewind.IsRewinding)
        {
            Rewind();
        }
        else
        {
            Record();
        }

        if (Dead)
        {
            if (TimeRewind.IsRewinding)
            {
                StartCoroutine(WaitForSpawn());
            }
            else
            {
                TimeSinceDeath += Time.deltaTime;

                if (TimeSinceDeath > 5)
                {
                    Destroy(Enemy);
                }
            }
        }
    }

    IEnumerator WaitForSpawn()
    {
        yield return new WaitForSeconds(TimeSinceDeath);
        Enemy.GetComponent<Renderer>().enabled = true;
        //Re-Enable AI
        Dead = false;
        TimeSinceDeath = 0;
    }

    void Record()
    {
        if (health.Count > Mathf.Round(5f / Time.fixedDeltaTime))
        {

        }

        health.Insert(0, Health);
    }

    void Rewind()
    {
        if (health.Count > 0)
        {
            Health = health[0];
            health.RemoveAt(0);
        }

    }
}

