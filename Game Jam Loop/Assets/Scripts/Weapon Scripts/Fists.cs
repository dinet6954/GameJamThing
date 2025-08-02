using UnityEngine;

public class Fists : MonoBehaviour
{
    public Camera FPS;
    [SerializeField] GameObject AmmoCounter;

    public float Damage = 100f;
    public float Range = 2f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }

        AmmoCounter.GetComponent<TMPro.TMP_Text>().text = "Fists/100";
    }

    void Shoot()
    {

        RaycastHit hit;

        if (Physics.Raycast(FPS.transform.position, FPS.transform.forward, out hit, Range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(Damage);
            }
        }

    }
}
