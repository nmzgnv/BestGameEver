using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffect : MonoBehaviour
{
    private ParticleSystem bloodDrops;
    public SpriteRenderer[] bloodSpots;

    private IEnumerator SetBloodSpot()
    {
        yield return new WaitForSeconds(bloodDrops.main.startLifetime.constant);
        int spotIndex = Random.Range(0, bloodSpots.Length);
        Instantiate(bloodSpots[spotIndex].gameObject, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    
    void Start()
    {
        bloodDrops = GetComponent<ParticleSystem>();
        StartCoroutine(SetBloodSpot());
    }
}
