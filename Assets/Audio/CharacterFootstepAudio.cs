using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFootstepAudio : MonoBehaviour
{
    [SerializeField] AudioClip[] footstepSounds;

    [SerializeField] AudioSource footsStepSource;

    [SerializeField] float pitchVariance = 0.5f;

    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    public void PlayFootstep()
    {
        footsStepSource.clip = footstepSounds[0];

        footsStepSource.pitch = Random.Range(1.0f - pitchVariance, 1.0f + pitchVariance);

        footsStepSource.Play();
    }
}
