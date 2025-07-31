using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{

    public Collider Hitbox;
    public TextMeshProUGUI InteractText;
    private bool InRange = false;
    public Inventory Inventory;
    [SerializeField] private int PickedUpItem;

    void Start()
    {
        Inventory inventory = GetComponent<Inventory>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DroppedDeagle"))
        {
            InRange = true;
            InteractText.text = "Press F to pick up Deagle";
            PickedUpItem = 1;
        }

        if (other.CompareTag("DroppedShotgun"))
        {
            InRange = true;
            InteractText.text = "Press F to pick up Shotgun";
            PickedUpItem = 2;
        }

        if (other.CompareTag("DroppedSMG"))
        {
            InRange = true;
            InteractText.text = "Press F to pick up SMG";
            PickedUpItem = 3;
        }

        if (other.CompareTag("DroppedRifle"))
        {
            InRange = true;
            InteractText.text = "Press F to pick up Rifle";
            PickedUpItem = 4;
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
            }
        }
    }
}
