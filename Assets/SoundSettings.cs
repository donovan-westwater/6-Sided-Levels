using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundSettings : MonoBehaviour
{

	public AudioMixer audiomixer;

	public void SetVolume (float volume)

	{
		audiomixer.SetFloat("vol", Mathf.Log10(volume) * 20 );
	}

}
