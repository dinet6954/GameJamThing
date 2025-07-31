using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int Slot1 = 0;

    public GameObject Fists;
    public GameObject Deagle;
    public GameObject Shotgun;
    public GameObject SMG;
    public GameObject Rifle;

    void Update()
    {
        Weapons();
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

}
