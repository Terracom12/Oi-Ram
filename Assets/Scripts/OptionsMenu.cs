using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
	[SerializeField]
	private AudioMixer mixer;

	[SerializeField]
	private Slider slider;

	private AudioSource tone;

	private float timeSinceVolumeSet;
	private bool tonePlayed = true;

	void Start()
	{
		float currentVolume;
		tone = GetComponent<AudioSource>();
		mixer.GetFloat("VolumeParam", out currentVolume);
		slider.SetValueWithoutNotify(currentVolume);
	}

	void Update()
	{
		timeSinceVolumeSet += Time.deltaTime;

		if (!tonePlayed)
		{
			if (timeSinceVolumeSet > 0.25)
			{
				tone.Play();
				tonePlayed = true;
			}
		}
	}

	public void OnVolume(float value)
	{
		timeSinceVolumeSet = 0;
		mixer.SetFloat("VolumeParam", value);
		tonePlayed = false;
	}
}
