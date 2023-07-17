using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpellSystem;

//class for single stage in sequence of spell
public class SpellStage {
    public ISpell spell;//decoorated spell object
    public GameObject spellPrefab;//spell gameObject to be instantiated
    public int baseInterval;//base separation between spell stage activations
    public SpellStage(ISpell spell, GameObject spellPrefab, int baseInterval) {
        this.spell = spell;
        this.spellPrefab = spellPrefab;
        this.baseInterval = baseInterval;
    }
}

public class Rune : MonoBehaviour
{
    [SerializeField]
    public GameObject spellPrefab;
    public int baseInterval;
    public int interval;

    public Queue<SpellStage> sequence = new Queue<SpellStage>();

    public GameObject sentryPrefab;

    // Start is called before the first frame update
    void Start()
    {
        generateSpell();
    }

    // Update is called once per frame
    void Update()
    {
        //spells are built in this update function at runtime
    }

    private void OnTriggerEnter(Collider other) {
        
    }

    /// <summary>
    /// Example of generating spells
    /// </summary>
    public void generateSpell() {
        
        //first sentry stage
        ISpell spell = new BaseSpell();
        //spell = new Burst(spell);
        sequence.Enqueue(new SpellStage(spell, sentryPrefab, baseInterval));

        //second sentry stage
        spell = new BaseSpell();
        spell = new Burst(spell);
        sequence.Enqueue(new SpellStage(spell, sentryPrefab, baseInterval));

        //final stage
        spell = new BaseSpell();
        spell = new South(new West(new Burst(new Burst(spell))));
        sequence.Enqueue(new SpellStage(spell, spellPrefab, baseInterval));

        //begin spell by activating first stage
        var peek = sequence.Peek();
        peek.spell.activate(transform.position, peek.baseInterval, sequence);
    }

    public void startNextStage() {

    }
}
