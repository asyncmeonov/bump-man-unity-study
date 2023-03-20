using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AudioClip[] pickupSounds;
    
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = pickupSounds[Random.Range(0, pickupSounds.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Player")
        {
            transform.parent.SendMessage("PickUpBump");
            AudioSource.PlayClipAtPoint(audioSource.clip, gameObject.transform.position);
            Destroy(gameObject);
        }
    }
}
