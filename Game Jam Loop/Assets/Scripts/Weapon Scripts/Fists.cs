using UnityEngine;

public class Fists : MonoBehaviour
{
    public Camera FPS;
    [SerializeField] GameObject AmmoCounter;
    [SerializeField] GameObject AmmoWarn;

    public float Damage = 100f;
    public float Range = 2f;
    public AudioSource Fist;
    public AudioClip Swing;
    public AudioClip Hit;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }

        AmmoCounter.GetComponent<TMPro.TMP_Text>().text = "Fists/100";
        AmmoWarn.SetActive(false);
    }

    void Shoot()
    {

        RaycastHit hit;
        Fist.PlayOneShot(Swing);


        if (Physics.Raycast(FPS.transform.position, FPS.transform.forward, out hit, Range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(Damage);
                Fist.PlayOneShot(Hit);
            }
        }

    }
}
