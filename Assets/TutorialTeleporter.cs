using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialTeleporter : MonoBehaviour
{
    public GameObject player;
    public GameObject teleportParticleSys;

    private bool startedOnce = false;
    private ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        ps = teleportParticleSys.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!startedOnce && Vector3.Distance(player.transform.position, transform.position) < 4.5f)
        {
            startedOnce = true;
            teleportParticleSys.SetActive(true);
            StartCoroutine(teleportPlayer(5));
            ps.Play();
        }
    }


        


    private IEnumerator teleportPlayer(float delay)
    {
        float cursor = delay;
        while(cursor > 0)
        {
            cursor -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene("PlayTest", LoadSceneMode.Single);
        yield return null;
    }
}
