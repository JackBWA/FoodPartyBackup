using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour
{

    public Vector3 centerOffset;
    public Vector3 areaSize;

    public bool debug = true;

    public ParticleSystem smokePrefab;
    public ParticleSystem particlePrefab;
    public List<PositionRotation> particlePositions = new List<PositionRotation>();
    private List<ParticleSystem> smokeParticles = new List<ParticleSystem>();
    private List<ParticleSystem> particles = new List<ParticleSystem>();

    private void Awake()
    {
        foreach(PositionRotation pR in particlePositions)
        {
            ParticleSystem pS = Instantiate(particlePrefab);
            pS.Stop();
            pS.transform.SetParent(transform);
            pS.transform.position = transform.position + pR.position;
            pS.transform.rotation = Quaternion.Euler(pR.rotation);
            particles.Add(pS);
            pS = Instantiate(smokePrefab);
            pS.Stop();
            pS.transform.SetParent(transform);
            pS.transform.position = transform.position + pR.position;
            pS.transform.rotation = Quaternion.Euler(pR.rotation);
            smokeParticles.Add(pS);
        }
    }

    private void OnDrawGizmos()
    {
        if (!debug) return;
        Gizmos.color = new Color(255f/255f, 0f/255f, 0f/255f, 100f/255f);
        Gizmos.DrawCube(transform.position + centerOffset, areaSize);

        foreach(PositionRotation pR in particlePositions)
        {
            Gizmos.DrawIcon(transform.position + pR.position, "BuildSettings.Metro.Small");
        }
    }

    public void PreTrigger()
    {
        foreach (ParticleSystem pS in smokeParticles)
        {
            pS.Play();
        }
    }

    public void Trigger()
    {
        #region Particles
        foreach (ParticleSystem pS in particles)
        {
            pS.Play();
        }
        #endregion

        #region Area Overlap
        
        foreach (Collider c in Physics.OverlapBox(transform.position + centerOffset, areaSize))
        {
            DontGetBurntController controller;
            if(c.gameObject.TryGetComponent(out controller))
            {
                ((MiniGame_DontGetBurnt)MiniGame.singleton).RemovePlayer(controller.gameObject.GetComponent<PlayerCharacter>());
            }
        }
        #endregion
    }
}
