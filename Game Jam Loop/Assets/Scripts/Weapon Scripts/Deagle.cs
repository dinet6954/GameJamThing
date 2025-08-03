using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class Deagle : MonoBehaviour
{
    public Camera FPS;

    public float Damage = 70f;
    public float Range = 1000f;
    public float FireRate = 1f;
    private float NextFireTime = 0f;

    public int MagSize = 7;
    public int Rounds = 7;
    public Transform Deag;
    public GameObject MFlash;
    public GameObject BulletHole;
    public AudioSource Gunshot;
    public AudioClip Shot;
    public bool CanShoot = true;
    public TimeRewind TimeRewind;
    [SerializeField] GameObject AmmoCounter;

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
            Shoot();
            Debug.Log("Bang!");
        }

        AmmoCounter.GetComponent<TMPro.TMP_Text>().text = Rounds + "/7";
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

                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    target.TakeDamage(Damage);
                }

                Instantiate(BulletHole, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
            }

            Instantiate(MFlash, Deag.position, Quaternion.identity);
            Gunshot.PlayOneShot(Shot);
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
