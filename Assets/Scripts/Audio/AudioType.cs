using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Audio", menuName = "ScriptableAssets/AudioType", order = 2)]
public class AudioType : ScriptableObject
{
    [HideInInspector] public int currentIndex;
	public List<AudioClip> audioClips;

    public AudioClip GetClip()
	{
		if (currentIndex >= audioClips.Count)
		{
			currentIndex = 0;
		}

		return audioClips[currentIndex++];
	}
}