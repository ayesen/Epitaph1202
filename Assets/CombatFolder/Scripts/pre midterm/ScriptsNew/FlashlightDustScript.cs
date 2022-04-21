using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class FlashlightDustScript: MonoBehaviour
{
    ParticleSystem ps;

    // these lists are used to contain the particles which match
    // the trigger conditions each frame.
    List<ParticleSystem.Particle> inside = new List<ParticleSystem.Particle>();
    List<ParticleSystem.Particle> outside = new List<ParticleSystem.Particle>();

    void OnEnable()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void OnParticleTrigger()
    {
        // get the particles which matched the trigger conditions this frame
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside);
        int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Outside, outside);

        // iterate through the particles which inside the trigger and make them white
        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = inside[i];
            p.startColor = new Color32(255, 255, 255, 255);
            inside[i] = p;
        }

        // iterate through the particles which outside the trigger and make them invisable
        for (int i = 0; i < numExit; i++)
        {
            ParticleSystem.Particle p = outside[i];
            p.startColor = new Color32(0, 0, 0, 0);
            outside[i] = p;
        }

        // re-assign the modified particles back into the particle system
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside);
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Outside, outside);
    }
}
