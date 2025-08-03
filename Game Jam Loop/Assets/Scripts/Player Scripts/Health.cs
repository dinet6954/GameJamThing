using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Health : MonoBehaviour
{
    public TimeRewind Rwd;
    public float HP = 100;
    [SerializeField] float countdown = 5;
    [SerializeField] GameObject HealthCounter;
    public string CurrentScene;
    List<float> health;


    void Start()
    {
        Rwd = GetComponent<TimeRewind>();
        health = new List<float>();
    }

    void Update()
    {
        if (HP < 100)
        {
            countdown = countdown - Time.deltaTime;

            if (countdown <= 0)
            {
                HP = 100;
                countdown = 5;
            }
        }

        if (HP == 100)
        {
            countdown = 100;
        }

        HealthCounter.GetComponent<TMPro.TMP_Text>().text = HP.ToString();

        if (HP <= 0)
        {
            Die();
        }

    }

    void FixedUpdate()
    {
        if (Rwd.IsRewinding == true)
        {
            Rewind();
        }
        else
        {
            Record();
        }
    }

    public void TakeDamage(float amount)
    {
        countdown = 5;
        HP -= amount;
        if (HP <= 0f)
        {

        }
    }

    void Record()
    {
        if (health.Count > Mathf.Round(5f / Time.fixedDeltaTime))
        {

        }

        health.Insert(0, HP);
    }

    void Rewind()
    {
        if (health.Count > 0)
        {
            HP = health[0];
            health.RemoveAt(0);
        }
    }

    void Die()
    { 
        SceneManager.LoadScene("Game");
    }
}
