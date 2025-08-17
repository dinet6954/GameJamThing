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
    List<int> CountDown;


    void Start()
    {
        Rwd = GetComponent<TimeRewind>();
        health = new List<float>();
        CountDown = new List<int>();
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
    }

    void Record()
    {
        if (health.Count > Mathf.Round(5f / Time.fixedDeltaTime))
        {
            health.RemoveAt(health.Count - 1);
        }

        health.Insert(0, HP);

        if (CountDown.Count > Mathf.Round(5f / Time.fixedDeltaTime))
        {
            CountDown.RemoveAt(CountDown.Count - 1);
        }
    }

    void Rewind()
    {
        if (health.Count > 0)
        {
            HP = health[0];
            health.RemoveAt(0);
        }

        countdown = CountDown[0];
        CountDown.RemoveAt(0);
    }

    void Die()
    { 
        SceneManager.LoadScene(CurrentScene);
    }
}
