using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Text))]
public class TutorialDialogue : MonoBehaviour
{
    public float textCrawlSpeedInSeconds;
    private Text box;
    private List<string> dialouge = new List<string>();
    private List<float> delay = new List<float>();

    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<Text>();
        dialouge.Add("YO.           \nNEW HIRE.");
        delay.Add(4);
        dialouge.Add("Make yourself useful\nand get a pie going!");
        delay.Add(10);

        StartCoroutine(dialogueAdvancer());

    }

    private IEnumerator dialogueAdvancer()
    {
        StartCoroutine(textCrawl(dialouge[0]));
        yield return new WaitForSeconds(delay[0]);
        StartCoroutine(textCrawl(dialouge[1]));
        yield return new WaitForSeconds(delay[1]);
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
}
