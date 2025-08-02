using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Profiling;
using System.Collections;

public class PickUp : MonoBehaviour
{

    public Collider Hitbox;
    public TextMeshProUGUI InteractText;
    public bool InRange = false;
    public Inventory Inventory;
    [SerializeField] private int PickedUpItem;
    private GameObject DroppedItem;
    public TimeRewind TimeRewind;
    [SerializeField] private float TimeStopTime;
    [SerializeField] private float PickUpTime = 0;
    public bool Counting = false;
    int i;


    void Start()
    {
        Inventory inventory = GetComponent<Inventory>();
        TimeRewind timeRewind = GetComponent<TimeRewind>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DroppedDeagle"))
        {
            InRange = true;
            InteractText.text = "Press F to pick up Deagle";
            PickedUpItem = 1;
            DroppedItem = other.gameObject;
        }

        if (other.CompareTag("DroppedShotgun"))
        {
            InRange = true;
            InteractText.text = "Press F to pick up Shotgun";
            PickedUpItem = 2;
            DroppedItem = other.gameObject;
        }

        if (other.CompareTag("DroppedSMG"))
        {
            InRange = true;
            InteractText.text = "Press F to pick up SMG";
            PickedUpItem = 3;
            DroppedItem = other.gameObject;
        }

        if (other.CompareTag("DroppedRifle"))
        {
            InRange = true;
            InteractText.text = "Press F to pick up Rifle";
            PickedUpItem = 4;
            DroppedItem = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DroppedDeagle"))
        {
            InRange = false;
            InteractText.text = "";
        }

        if (other.CompareTag("DroppedShotgun"))
        {
            InRange = false;
            InteractText.text = "";
        }

        if (other.CompareTag("DroppedSMG"))
        {
            InRange = false;
            InteractText.text = "";
        }

        if (other.CompareTag("DroppedRifle"))
        {
            InRange = false;
            InteractText.text = "";
        }
    }

    void Update()
    {
        if (InRange == true)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                switch (PickedUpItem)
                {
                    case 1:
                        Inventory.Slot1 = 1;
                        break;
                    case 2:
                        Inventory.Slot1 = 2;
                        break;
                    case 3:
                        Inventory.Slot1 = 3;
                        break;
                    case 4:
                        Inventory.Slot1 = 4;
                        break;
                }

                DroppedItem.SetActive(false);
                Counting = true;
                InRange = false;
                InteractText.text = "";
                i = 1;

            }
        }
        else
        {
            InteractText.text = "";
        }
        
        if (Input.GetKeyUp(KeyCode.T))
        {
            PickUpTime = 0;
        }
    }

    void FixedUpdate()
    {
        if (i == 1)
        {
            if (TimeRewind.IsRewinding == true)
            {
                TimeStopTime = TimeStopTime + Time.deltaTime;
                StartCoroutine(WaitToSpawn());
            }
            else
            {
                TimeStopTime = 0;
            }
        }

        if (Counting == true)
            {
                PickUpTime = PickUpTime + Time.deltaTime;

                if (TimeRewind.IsRewinding == true)
                {
                    Counting = false;
                }
                else
                {
                    if (PickUpTime > 5)
                    {
                        Destroy(DroppedItem);
                        PickUpTime = 0;
                        Counting = false;
                    }
                }
            }
    }

    IEnumerator WaitToSpawn()
    {
        yield return new WaitForSeconds(PickUpTime);

        if (DroppedItem != null)
        {
            DroppedItem.SetActive(true);
            Counting = false;
        }
    }
}
