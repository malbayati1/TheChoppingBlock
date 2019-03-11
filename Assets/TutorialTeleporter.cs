using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialTeleporter : MonoBehaviour
{
    public Light dirLight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(teleportPlayer(10));
    }

    private IEnumerator teleportPlayer(float delay)
    {
        float cursor = delay;
        while(cursor > 0)
        {
            cursor -= Time.deltaTime;
        }
        SceneManager.LoadScene("PlayTest", LoadSceneMode.Single);
        yield return null;
    }
}
