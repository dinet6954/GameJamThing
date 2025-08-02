using UnityEngine;

public class CheckAirborne : MonoBehaviour
{

    public bool Airborne = false;
    public bool Running = false;

    void Update()
    {
        CheckForGround();
        CheckForRun();
    }

    void CheckForGround()
    {
        Vector3 origin = new Vector3(transform.position.x, transform.position.y - (transform.localScale.y * .5f), transform.position.z);
        Vector3 direction = transform.TransformDirection(Vector3.down);
        float distance = .75f;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, distance))
        {
            Airborne = false;
        }
        else
        {
            Airborne = true;
        }
    }

    void CheckForRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Running = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Running = false;
        }
    }
}
