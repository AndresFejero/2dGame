using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    public Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = gameObject.GetComponentInChildren<Slider>();
        transform.localPosition = new Vector3(0, 0, 0);
        healthBar.transform.localPosition = new Vector3(0, 7, 0);

        if (this.transform.parent.tag == "Player")
        {
            healthBar.maxValue = this.transform.parent.GetComponent<PlayerAttributes>().maxHealthPoints;
        }
        else
        {
            healthBar.maxValue = this.transform.parent.GetComponent<EnemyAttributes>().maxHealthPoints;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.parent.tag == "Player")
        {
            healthBar.value = this.transform.parent.GetComponent<PlayerAttributes>().healthPoints;
        }
        else
        {
            healthBar.value = this.transform.parent.GetComponent<EnemyAttributes>().healthPoints;
        }

    }
}