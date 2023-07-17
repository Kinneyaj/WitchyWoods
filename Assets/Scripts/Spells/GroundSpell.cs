using SpellSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpell : SpellParent {

    public int ttl;
    public MeshRenderer meshRenderer;

    private void Start() {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    public override void activate() {
        meshRenderer.enabled = true;
    }
    public override void activeUpdate() {
        ttl--;
        if (ttl <= 0) {
            Destroy(gameObject);
            activated = false;
        }
    }
}