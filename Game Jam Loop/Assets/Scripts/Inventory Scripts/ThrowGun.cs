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
    Rigidbody rb;

    void Start()
    {
        Inventory inventory = GetComponent<Inventory>();
        TimeRewind timeRewind = GetComponent<TimeRewind>();
        PickUp pickUp = GetComponent<PickUp>();
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
                Destroy(other.gameObject);
            }
        }
    }
}
