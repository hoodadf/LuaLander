using System;
using UnityEngine;

public class LanderVisuals : MonoBehaviour {
   [SerializeField] private ParticleSystem leftThrusterParticleSystem;
   [SerializeField] private ParticleSystem middleThrusterParticleSystem;
   [SerializeField] private ParticleSystem rightThrusterParticleSystem;

   private Lander lander;
   
   private void Awake() {
      lander = GetComponent<Lander>();

      lander.OnUpForce += Lander_OnUpForce;
      lander.OnLeftRot += Lander_OnLeftRot;
      lander.OnRightRot += Lander_OnRightRot;
      lander.OnBeforeForce += Lander_OnBeforeForce;
      
      setEnabledThrusterParticleSystems(leftThrusterParticleSystem,false);
      setEnabledThrusterParticleSystems(middleThrusterParticleSystem,false);
      setEnabledThrusterParticleSystems(rightThrusterParticleSystem,false);
   }

   

   private void setEnabledThrusterParticleSystems(ParticleSystem particleSystem, bool enabled) {
      ParticleSystem.EmissionModule leftEmissionModule= particleSystem.emission;
      leftEmissionModule.enabled = enabled;
   }
   
   private void Lander_OnRightRot(object sender, EventArgs e) {
      setEnabledThrusterParticleSystems(rightThrusterParticleSystem,true);   
   }

   private void Lander_OnLeftRot(object sender, EventArgs e) {
      setEnabledThrusterParticleSystems(leftThrusterParticleSystem,true);
   }

   private void Lander_OnUpForce(object sender,System.EventArgs e) {
      setEnabledThrusterParticleSystems(leftThrusterParticleSystem,true);
      setEnabledThrusterParticleSystems(middleThrusterParticleSystem,true);
      setEnabledThrusterParticleSystems(rightThrusterParticleSystem,true);
   }
   private void Lander_OnBeforeForce(object sender, EventArgs e) {
      setEnabledThrusterParticleSystems(leftThrusterParticleSystem,false);
      setEnabledThrusterParticleSystems(middleThrusterParticleSystem, false);
      setEnabledThrusterParticleSystems(rightThrusterParticleSystem,false);   
   }
   
   
}
