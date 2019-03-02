using System.Collections.Generic;
using UnityEngine;


/*
*
*
*		Universal Audio Managment Script
*
*		- Useful for Selcting and playing SFX and pretty much works for any game.
*
*
*/


public class AudioManager : MonoBehaviour
{

    public GameObject Sound_Prefab;																// Holds the prefab that plays the sound requested
    public List<string> Sound_Names = new List<string>();										// A list to hold the audioclip names
    public List<AudioClip> Sound_Clips = new List<AudioClip>();									// A list to hold the audioclips themselves
    public Dictionary<string, AudioClip> Sound_Lib = new Dictionary<string, AudioClip>();       // Dictionary that holds the audio names and clips

	
    private void Start()
    {
        for (int i = 0; i < Sound_Names.Count; i++)         // For loop that populates the dictionary with all the sound assets in the lists
        {
            Sound_Lib.Add(Sound_Names[i], Sound_Clips[i]);
        }
    }

	
	// Fuction to select and play a sound asset from the start with options using its name
    public void PlayClip(string request, float? Volume, float? Pitch)                   
    {
        if (Sound_Lib.ContainsKey(request))												// If the sound is in the library
        {
            GameObject clip = Instantiate(Sound_Prefab);									// Instantiate a Sound prefab
            clip.GetComponent<AudioSource>().clip = Sound_Lib[request];						// Get the prefab and add the requested clip to it

			clip.GetComponent<AudioSource>().volume = Volume.HasValue ? Volume.Value : 1;	// changes the volume if a it is inputted
			clip.GetComponent<AudioSource>().pitch = Pitch.HasValue ? Pitch.Value : 1;      // changes the pitch if a change is inputted

			clip.GetComponent<AudioSource>().Play();										// play the audio from the prefab
			Destroy(clip, clip.GetComponent<AudioSource>().clip.length);					// Destroy the prefab once the clip has finished playing
        }
    }

    // Fuction to select and play a sound asset from the start with options using its indx number 
    public void PlayClip(int request, float? Volume, float? Pitch)
    {
            GameObject clip = Instantiate(Sound_Prefab);									// Instantiate a Sound prefab
            clip.GetComponent<AudioSource>().clip = Sound_Clips[request];                     // Get the prefab and add the requested clip to it

            clip.GetComponent<AudioSource>().volume = Volume.HasValue ? Volume.Value : 1;   // changes the volume if a it is inputted
            clip.GetComponent<AudioSource>().pitch = Pitch.HasValue ? Pitch.Value : 1;      // changes the pitch if a change is inputted

            clip.GetComponent<AudioSource>().Play();                                        // play the audio from the prefab
            Destroy(clip, clip.GetComponent<AudioSource>().clip.length);					// Destroy the prefab once the clip has finished playing
    }


    // Function to select and play a sound asset from a selected time with options
    public void PlayClipFromTime(string request, float time, float? Volume, float? Pitch)
	{
		if (Sound_Lib.ContainsKey(request))
		{
			GameObject clip = Instantiate(Sound_Prefab);
			clip.GetComponent<AudioSource>().clip = Sound_Lib[request];
			clip.GetComponent<AudioSource>().time = time;

			clip.GetComponent<AudioSource>().volume = Volume.HasValue ? Volume.Value : 1;
			clip.GetComponent<AudioSource>().pitch = Pitch.HasValue ? Pitch.Value : 1;

			clip.GetComponent<AudioSource>().Play();
			Destroy(clip, clip.GetComponent<AudioSource>().clip.length);
		}
	}


	// Function to select and play a sound asset with a delay and options
	public void PlayClipWithDelay(string request, float delay, float? Volume, float? Pitch)
    {
        if (Sound_Lib.ContainsKey(request))
        {
            GameObject clip = Instantiate(Sound_Prefab);
            clip.GetComponent<AudioSource>().clip = Sound_Lib[request];

			clip.GetComponent<AudioSource>().volume = Volume.HasValue ? Volume.Value : 1;
			clip.GetComponent<AudioSource>().pitch = Pitch.HasValue ? Pitch.Value : 1;

			clip.GetComponent<AudioSource>().PlayDelayed(delay);							// Only difference, played with a delay rather that right away
            Destroy(clip, clip.GetComponent<AudioSource>().clip.length);
        }
    }

    public void PlayRandomSoundFromRange(int LowerNumber, int HigherNumber)
    {
        int RandomSound;

        RandomSound = Random.Range(LowerNumber, HigherNumber);

        GameObject clip = Instantiate(Sound_Prefab);                        // Instantiate a Sound prefab
        clip.GetComponent<AudioSource>().clip = Sound_Clips[RandomSound];         // Get the prefab and add the requested clip to it
        clip.GetComponent<AudioSource>().Play();                            // play the audio from the prefab
        Destroy(clip, clip.GetComponent<AudioSource>().clip.length);        // Destroy the prefab once the clip has finished playing
    }
}