using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class Inventory : MonoBehaviour
{
    public int Slot1 = 0;

    public GameObject Fists;
    public GameObject Deagle;
    public GameObject Shotgun;
    public GameObject SMG;
    public GameObject Rifle;
    List<int> Inv;
    public TimeRewind TimeRewind;

    void Start()
    {
        TimeRewind timeRewind = GetComponent<TimeRewind>();
        Inv = new List<int>();
    }

    void Update()
    {
        Weapons();
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

    void Weapons()
    {
        if (Slot1 == 0)
        {
            Fists.gameObject.SetActive(true);
            Deagle.gameObject.SetActive(false);
            Shotgun.gameObject.SetActive(false);
            SMG.gameObject.SetActive(false);
            Rifle.gameObject.SetActive(false);
        }

        if (Slot1 == 1)
        {
            Fists.gameObject.SetActive(false);
            Deagle.gameObject.SetActive(true);
            Shotgun.gameObject.SetActive(false);
            SMG.gameObject.SetActive(false);
            Rifle.gameObject.SetActive(false);
        }

        if (Slot1 == 2)
        {
            Fists.gameObject.SetActive(false);
            Deagle.gameObject.SetActive(false);
            Shotgun.gameObject.SetActive(true);
            SMG.gameObject.SetActive(false);
            Rifle.gameObject.SetActive(false);
        }

        if (Slot1 == 3)
        {
            Fists.gameObject.SetActive(false);
            Deagle.gameObject.SetActive(false);
            Shotgun.gameObject.SetActive(false);
            SMG.gameObject.SetActive(true);
            Rifle.gameObject.SetActive(false);
        }

        if (Slot1 == 4)
        {
            Fists.gameObject.SetActive(false);
            Deagle.gameObject.SetActive(false);
            Shotgun.gameObject.SetActive(false);
            SMG.gameObject.SetActive(false);
            Rifle.gameObject.SetActive(true);
        }
    }

    void Record()
    {
        if (Inv.Count > Mathf.Round(5f / Time.fixedDeltaTime))
        {

        }

        Inv.Insert(0, Slot1);
    }
    
    void Rewind()
    {
        if (Inv.Count > 0)
        {
            Slot1 = Inv[0];
            Inv.RemoveAt(0);
        }

    }
}
