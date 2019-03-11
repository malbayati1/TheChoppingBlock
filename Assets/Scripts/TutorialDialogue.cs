using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Text))]
public class TutorialDialogue : MonoBehaviour
{
    public float textCrawlSpeedInSeconds;
	public GameObject door;

    private Text box;
    private List<string> dialouge = new List<string>();
    private List<float> delay = new List<float>();
    private bool finishedMakingPie = false;


    void OnEnable()
    {
        
        RecipeManager.instance.onCook += checkFinishPie;
    }

    void OnDisable()
    {
        RecipeManager.instance.onCook -= checkFinishPie;
    }

    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<Text>();
        dialouge.Add("YO.           \nNEW HIRE.");
        delay.Add(4);
        dialouge.Add("Make yourself useful\nand get a pie going!");
        delay.Add(5);
        dialouge.Add("You can make a pie, right?\n               \nIt's 1 apple and 1 wheat.");
        delay.Add(4);
        dialouge.Add("Throw em both in a pot\nusing [K] and then hold [J] \nto cook em.");
        delay.Add(3);
        dialouge.Add("Well Done.\n        \nI knew you'd do fine.");
        delay.Add(3);
        dialouge.Add("Ok! Now,\nput that pie in the back left\n of the freezer.");
        delay.Add(10);

        StartCoroutine(dialogueAdvancer());

    }

    private IEnumerator dialogueAdvancer()
    {
        StartCoroutine(textCrawl(dialouge[0]));
        yield return new WaitForSeconds(delay[0]);
        StartCoroutine(textCrawl(dialouge[1]));
        yield return new WaitForSeconds(delay[1]);
        StartCoroutine(textCrawl(dialouge[2]));
        yield return new WaitForSeconds(delay[2]);
        StartCoroutine(textCrawl(dialouge[3]));
        yield return new WaitForSeconds(delay[3]);
        while (!finishedMakingPie)
        {
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(textCrawl(dialouge[4]));
        yield return new WaitForSeconds(delay[4]);
        StartCoroutine(textCrawl(dialouge[5]));
        yield return new WaitForSeconds(delay[5]);

        Debug.Log("End of Dialogue.");
        yield return null;
    }

    private IEnumerator textCrawl(string toAssign)
    {
        for (int i = 0; i < toAssign.Length; ++i)
        {
            box.text = toAssign.Substring(0, i + 1);
            yield return new WaitForSeconds(textCrawlSpeedInSeconds);
        }
        yield return null;
    }
    private IEnumerator moveDoor()
    {
        while(door.transform.localPosition.x < 3f)
        {
            door.transform.position += new Vector3(1 * Time.deltaTime, 0, 0);
            yield return new WaitForEndOfFrame();
        }
    }

    public void checkFinishPie(Ingredient toTest)
    {
        if(toTest.ID == 3)
        {
            finishedMakingPie = true;
            StartCoroutine(moveDoor());
        }
    }
}