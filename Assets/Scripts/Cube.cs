using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
public class Cube : MonoBehaviour
{
    [SerializeField] MMF_Player feedbackPlayer;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlayShake()
    {
        feedbackPlayer.PlayFeedbacks();
    }
}
