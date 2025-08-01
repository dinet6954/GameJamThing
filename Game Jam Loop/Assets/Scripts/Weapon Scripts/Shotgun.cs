using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Profiling;

public class Shotgun : MonoBehaviour
{
        public Camera FPS;

    public float Damage = 20f;
    public float Range = 50f;
    public float FireRate = 3f;
    private float NextFireTime = 0f;

    public int Pellets = 8;

    public int MagSize = 2; 
    public int Rounds = 2;

    public bool CanShoot = true;

    public TimeRewind TimeRewind;

    List<int> rounds;

    void Start()
    {
        TimeRewind timeRewind = GetComponent<TimeRewind>();
        rounds = new List<int>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= NextFireTime)
        {

            NextFireTime = Time.time + FireRate;
            Debug.Log("Bang!");
            for (int i = 0; i < Pellets; i++)
            {
                Shoot();
            }
            
            Rounds = Rounds - 1;
        }

        if (Input.GetKeyDown(KeyCode.R))
            {
                Reload();
            }
    }

    void FixedUpdate()
    {
        if (TimeRewind.IsRewinding == true)
        {
            Rewind();
        }
        else
        {
            Record();
        }
    }

    void Shoot()
    {
        if (Rounds > 0)
        {

            Vector3 direction = FPS.transform.forward; // your initial aim.
            Vector3 spread = Vector3.zero;
            spread += FPS.transform.up * Random.Range(-1f, 1f); // add random up or down (because random can get negative too)
            spread += FPS.transform.right * Random.Range(-1f, 1f); // add random left or right

            direction += spread * Random.Range(0f, 0.2f);
            RaycastHit hit;

            if (Physics.Raycast(FPS.transform.position, direction, out hit, Range))
            {
                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    target.TakeDamage(Damage);
                }
            }

            //Quite honestly don't understand a letter of this code, we should not touch this under any circumstances.
        }
        else
        {
            //Play a click noise or smth
            Debug.Log("Out of ammo!");
        }
    }

    void Reload()
    {
        if (Rounds != 2)
        {
            CanShoot = false;
            // Play reload animation
            CanShoot = true;
            Debug.Log("Reloaded!");
            Rounds = 2;
        }

    }

    void Record()
    {
        if (rounds.Count > Mathf.Round(5f / Time.fixedDeltaTime))
        {

        }

        rounds.Insert(0, Rounds);
    }

    void Rewind()
    {
        if (rounds.Count > 0)
        {
            Rounds = rounds[0];
            rounds.RemoveAt(0);
        }

    }
}
