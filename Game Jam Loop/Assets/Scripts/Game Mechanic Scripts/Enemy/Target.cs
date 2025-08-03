using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Profiling;
using System.Collections;
using UnityEngine.AI;

public class Target : MonoBehaviour
{
    public float Health = 100;
    public GameObject Enemy;
    public Transform SpawnPoint;
    public TimeRewind TimeRewind;
    public EnemyAI EAI;
    [SerializeField] private float TimeStopCount;
    [SerializeField] private float TimeSinceDeath;
    public bool Dead;
    public GameObject Deagle;
    public GameObject Shotgun;
    public GameObject SMG;
    List<float> health;
    public GameObject Player;
    private GameObject DroppedGun;
    bool dropped;
    bool once = true;
    

    void Awake()
    {
        Player = GameObject.Find("FirstPersonController");
        TimeRewind timeRewind = Player.GetComponent<TimeRewind>();
        EnemyAI EAI = GetComponent<EnemyAI>(); 
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
        Enemy.GetComponent<NavMeshAgent>().enabled = false;
        Dead = true;
        dropped = true;
        EAI.Damage = 0;
    }

    void Update()
    {
        if (dropped)
        {
            if (once == true)
            {
                switch (EAI.EquippedGun)
                {
                    case 1:
                        DroppedGun = Instantiate(Deagle, SpawnPoint.position, SpawnPoint.rotation);
                        break;
                    case 2:
                        DroppedGun = Instantiate(Shotgun, SpawnPoint.position, SpawnPoint.rotation);
                        break;
                    case 3:
                        DroppedGun = Instantiate(SMG, SpawnPoint.position, SpawnPoint.rotation);
                        break;
                }
                once = false;
            }
        }   
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
        Destroy(DroppedGun);
        EAI.SetDamage();
    }

    void Record()
    {
        if (health.Count > Mathf.Round(5f / Time.fixedDeltaTime))
        {
            health.RemoveAt(health.Count - 1);
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

