using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpellSystem;

public class SpellProjectile : MonoBehaviour
{
    public float speed = 5f;
    public Vector3 direction;
    public int ttl;
    public ISpell spell;
    public GameObject spellPrefab;
    public int interval;
    public Queue<SpellStage> sequence;

    // Start is called before the first frame update
    void Start()
    {
        direction = transform.forward;
        interval = 100;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveAmount = direction * speed * Time.deltaTime;
        transform.Translate(moveAmount);
        ttl--;
        if(ttl <= 0) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.name == "Enemy") {
            spell.activate(transform.position, interval, sequence);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
