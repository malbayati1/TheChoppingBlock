using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerDeathManager : MonoBehaviour
{

    private Health playerHealth;
    [SerializeField] private GameObject player; //  Reference to the player
    [SerializeField] private GameObject deathMenu;  //  Reference to the Canvas Death Menu
    [SerializeField] private SeasonManager levelmanager; 
    [SerializeField] private TextMeshProUGUI score;

    // Start is called before the first frame update
    private void Awake()
    {
        Time.timeScale = 1;
    }

    void Start()
    {
        playerHealth = player.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealth.health <= 0)
        {
            deathMenu.SetActive(true);
            score.text = string.Format("SEASONS SURVIVED: {0}", levelmanager.getSeasonCount());
            Time.timeScale = 0;
        }
    }
}
