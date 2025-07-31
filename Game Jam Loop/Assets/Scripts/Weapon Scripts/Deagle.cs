using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class Deagle : MonoBehaviour
{
    public Camera FPS;

    public float Damage = 70f;
    public float Range = 1000f;

    public int MagSize = 7;
    public int Rounds = 7;

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
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
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
            Rounds = Rounds - 1;
            RaycastHit hit;

            if (Physics.Raycast(FPS.transform.position, FPS.transform.forward, out hit, Range))
            {
                Debug.Log(hit.transform.name);
            }
        }
        else
        {
            //Play a click noise or smth
            Debug.Log("Out of ammo!");
        }
    }

    void Reload()
    {
        if (Rounds != 7)
        {
            CanShoot = false;
            // Play reload animation
            CanShoot = true;
            Debug.Log("Reloaded!");
            Rounds = 7;
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
