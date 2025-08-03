using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowGun : MonoBehaviour
{

    public Transform FPS;
    public GameObject Deagle;
    public GameObject Shotgun;
    public GameObject SMG;
    public GameObject Rifle;
    public float ThrowForce = 30f;
    public Inventory Inventory;
    public TimeRewind TimeRewind;
    public Collider Hitbox;
    public PickUp PickUp;
    private bool Throwcount = false;
    GameObject thrownObject;
    [SerializeField] GameObject Ammowarn;
    Rigidbody rb;
    public Deagle deag;
    public Shotgun shot;
    public SMG smg;

    void Start()
    {
        Inventory inventory = GetComponent<Inventory>();
        TimeRewind timeRewind = GetComponent<TimeRewind>();
        PickUp pickUp = GetComponent<PickUp>();
        Deagle deag = GetComponent<Deagle>();
        Shotgun shot = GetComponent<Shotgun>();
        SMG smg = GetComponent<SMG>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (PickUp.Counting != true)
            {
                Throw();
            }
        }

        if (deag.Rounds == 0 || shot.Rounds == 0 || smg.Rounds == 0)
        {
            Ammowarn.SetActive(true);
        }
        else
        {
            Ammowarn.SetActive(false);
        }
    }

    void Throw()
    {
        switch (Inventory.Slot1)
        {
            case 0:
                Debug.Log("Can't throw your hands");
                break;
            case 1:
                thrownObject = Instantiate(Deagle, FPS.position, FPS.rotation);
                rb = thrownObject.GetComponent<Rigidbody>();
                deag.Rounds = 7;

                if (rb != null)
                {
                    rb.AddForce(FPS.forward * ThrowForce, ForceMode.Impulse);
                    Inventory.Slot1 = 0;
                }
                else
                {
                    Debug.LogWarning("Thrown object does not have a Rigidbody component!");
                }
                break;
            case 2:
                thrownObject = Instantiate(Shotgun, FPS.position, FPS.rotation);
                rb = thrownObject.GetComponent<Rigidbody>();
                shot.Rounds = 2;

                if (rb != null)
                {
                    rb.AddForce(FPS.forward * ThrowForce, ForceMode.Impulse);
                    Inventory.Slot1 = 0;
                }
                else
                {
                    Debug.LogWarning("Thrown object does not have a Rigidbody component!");
                }
                break;
            case 3:
                thrownObject = Instantiate(SMG, FPS.position, FPS.rotation);
                rb = thrownObject.GetComponent<Rigidbody>();
                smg.Rounds = 30;

                if (rb != null)
                {
                    rb.AddForce(FPS.forward * ThrowForce, ForceMode.Impulse);
                    Inventory.Slot1 = 0;
                }
                else
                {
                    Debug.LogWarning("Thrown object does not have a Rigidbody component!");
                }
                break;
            case 4:
                thrownObject = Instantiate(Rifle, FPS.position, FPS.rotation);
                rb = thrownObject.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.AddForce(FPS.forward * ThrowForce, ForceMode.Impulse);
                    Inventory.Slot1 = 0;
                }
                else
                {
                    Debug.LogWarning("Thrown object does not have a Rigidbody component!");
                }
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (TimeRewind.IsRewinding)
        {
            if (other.gameObject == thrownObject)
            {
                if (other.gameObject != null)
                {
                    Destroy(other.gameObject);
                    PickUp.InRange = false;
                }
                
            }
        }
    }
}
