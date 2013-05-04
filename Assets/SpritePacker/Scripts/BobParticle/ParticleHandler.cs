using UnityEngine;
using System.Collections;

public class ParticleHandler : MonoBehaviour {

	// Use this for initialization
    public ParticleSystem starSystem;

    public float emissionRate = 1;

    public int maxStars = 1000;
    public float minLifetime = 10;
    public float maxLifetime = 10;
    public float minSpin = 100;
    public float maxSpin = 360;

    public int frameCount = 16;
    public float randomness = 1;

    Transform myTransform;

    public ParticleSystem.Particle[] starParticles;

    int starID = 0;

    public Star[] stars;
    bool firstFrame = true;
    float longLifetime = 1000;
    float toEmit = 0;

    public struct Star
    {
        public float lifetime;
        public Vector3 position;
    }
	void Start () {
        starSystem.transform.position = Vector3.zero;
        starSystem.transform.rotation = Quaternion.identity;

        myTransform = transform;
        starParticles = new ParticleSystem.Particle[maxStars];

        stars = new Star[maxStars];
        starSystem.emissionRate = 0;
        starSystem.startLifetime = longLifetime;
        starSystem.Emit(maxStars);
	}
	
	// Update is called once per frame
	void Update () {
        if (firstFrame)
        {
            starSystem.GetParticles(starParticles);
            for (int i = 0; i < maxStars; i++ )
            {
                stars[i].lifetime = longLifetime;
                starParticles[i].position = Vector3.zero;
                stars[i].position = starParticles[i].position;
            }
            starSystem.SetParticles(starParticles, maxStars);
            firstFrame = false;
            return;
        }

        starSystem.GetParticles(starParticles);

        toEmit += emissionRate * Time.deltaTime;
        while (toEmit > 0)
        {
            Emit();
            toEmit--;
        }

        if (!firstFrame)
        {
            for (int i = 0; i < maxStars; i++)
            {
                if (starParticles[i].velocity == Vector3.zero)
                {
                    starParticles[i].velocity = stars[i].position - Vector3.zero;
                    starParticles[i].velocity.Normalize();
                }
            }
        }

        starSystem.SetParticles(starParticles,maxStars);
	
	}

    void Emit()
    {
        stars[starID].lifetime = Random.Range(minLifetime, maxLifetime);
        Vector3 vel = (Random.onUnitSphere* 100) + (Vector3.up * 100);
        vel = new Vector3(vel.x, vel.y, 0);
        starParticles[starID].velocity = vel;
        starParticles[starID].position = myTransform.position;
        starParticles[starID].lifetime = minLifetime * 2;
        starParticles[starID].startLifetime = minLifetime * 2;
        starID++;
        if (starID >= maxStars) starID = 0;
    }
}
