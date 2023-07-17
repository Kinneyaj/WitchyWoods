using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpellSystem;

public class SentryBehavior : SpellParent {
    public int ttl;
    [SerializeField]
    public int shotTime = 200;
    public int shotTimer = 200;
    public int nShotsRemaining = 1;

    public MeshRenderer meshRenderer;
    public GameObject projectilePrefab;

    private void Start() {
        meshRenderer = GetComponent<MeshRenderer>();
        sequence.Dequeue();
    }
    public override void activate() {
        meshRenderer.enabled = true;
    }
    public override void activeUpdate() {
        shotTimer--;
        if(shotTimer <= 0) {
            fireShot();
            shotTimer = shotTime;
        }
        if(nShotsRemaining <= 0) {
            Destroy(gameObject);
        }
    }
    public void fireShot() {
        var projectile = Instantiate(projectilePrefab);
        projectile.transform.position = transform.position;
        projectile.GetComponent<SpellProjectile>().sequence = sequence;
        projectile.GetComponent<SpellProjectile>().spell = sequence.Peek().spell;
        nShotsRemaining--;
    }
}
