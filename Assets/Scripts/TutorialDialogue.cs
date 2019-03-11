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
        delay.Add(10);
        dialouge.Add("You do know how to make a pie, right?\nYou just need one apple and one thing of wheat.\nThrow em in the pot and hold [K] to cook em.");
        delay.Add(5);
        dialouge.Add("Not half bad...\nPut that in the back left\n of the freezer.");
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
        while (!finishedMakingPie)
        {
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(textCrawl(dialouge[3]));
        yield return new WaitForSeconds(delay[3]);
        
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
        Debug.Log("asrdthgsfgd");
        //GameObject door = GameObject.Find("Cube (6)");
        //GameObject handle = GameObject.Find("Cube(9)");
        while(door.transform.localPosition.x < 3f)
        {
            door.transform.position += new Vector3(1 * Time.deltaTime, 0, 0);
            //handle.transform.position += new Vector3(1 * Time.deltaTime, 0, 0);
            yield return new WaitForEndOfFrame();
        }
    }

    public void checkFinishPie(Ingredient toTest)
    {
        Debug.Log(toTest.ID);
        if(toTest.ID == 3)
        {
            finishedMakingPie = true;
			delay[0] = delay[1] = delay[2] = 0;
            StartCoroutine(moveDoor());
        }
    }
}