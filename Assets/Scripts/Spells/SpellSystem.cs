using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpellSystem {
    /// <summary>
    /// Generic Spell & Spell Decorator Objects
    /// </summary>
    public interface ISpell {
        void activate(Vector3 pos, int interval, Queue<SpellStage> sequence);
    }
    public class BaseSpell : ISpell {
        //passes in rune game object as spellObj containing spell sequence info.
        public void activate(Vector3 pos, int interval, Queue<SpellStage> sequence) {
            var peek = sequence.Peek();
            //spell is triggered
            var spell = GameObject.Instantiate(peek.spellPrefab);
            //set spell activation delay
            spell.gameObject.GetComponent<SpellParent>().timer = interval;
            spell.gameObject.GetComponent<SpellParent>().sequence = new Queue<SpellStage>(sequence);
            spell.transform.position = pos;
        }
    }
    public abstract class SpellDecorator : ISpell {
        protected ISpell _spell;
        public SpellDecorator(ISpell spell) {
            _spell = spell;
        }
        public virtual void activate(Vector3 pos, int interval, Queue<SpellStage> sequence) {
            _spell.activate(pos, interval, sequence);
        }
    }

    /// <summary>
    /// Unique Spell Implementations
    /// </summary>
    public class Burst : SpellDecorator {
        public Burst(ISpell spell) : base(spell) { }
        float radius = 4;
        int numNodes = 8;
        public override void activate(Vector3 pos, int interval, Queue<SpellStage> sequence) {
            for (int i = 0; i < numNodes; i++) {
                float angle = i * Mathf.PI * 2f / numNodes;
                Vector3 newPos = new Vector3(pos.x + Mathf.Cos(angle) * radius, pos.y, pos.z + Mathf.Sin(angle) * radius);
                _spell.activate(newPos, interval, sequence);
            }
        }
    }

    //parent class for North, South, East, West to inherit from
    public abstract class DirectionalSpell : SpellDecorator {
        public int numReps = 8;//number of spell nodes generated by rune
        public float separation = 2;//separation between nodes
        public DirectionalSpell(ISpell spell) : base(spell) { }
        public abstract Vector3 calcPosition(Vector3 pos, int i);
        //template activate function
        public override void activate(Vector3 pos, int interval, Queue<SpellStage> sequence) {
            int baseInterval = sequence.Peek().baseInterval;
            for (int i = 0; i < numReps; i++) {
                Vector3 newPos = calcPosition(pos, i);
                _spell.activate(newPos, interval + i * baseInterval, sequence);
            }
        }
    }
    public class South : DirectionalSpell {
        public South(ISpell spell) : base(spell) { }
        public override Vector3 calcPosition(Vector3 pos, int i) {
            return new Vector3(pos.x, pos.y, pos.z - (separation * i));
        }

    }
    public class North : DirectionalSpell {
        public North(ISpell spell) : base(spell) { }
        public override Vector3 calcPosition(Vector3 pos, int i) {
            return new Vector3(pos.x, pos.y, pos.z + (separation * i));
        }
    }
    public class West : DirectionalSpell {
        public West(ISpell spell) : base(spell) { }
        public override Vector3 calcPosition(Vector3 pos, int i) {
            return new Vector3(pos.x - (separation * i), pos.y, pos.z);
        }
    }
    public class East : DirectionalSpell {
        public East(ISpell spell) : base(spell) { }
        public override Vector3 calcPosition(Vector3 pos, int i) {
            return new Vector3(pos.x + (separation * i), pos.y, pos.z);
        }
    }

    /// <summary>
    /// Spell Control Classes
    /// </summary>
    /// 
    public abstract class SpellParent : MonoBehaviour {//template for triggerable spell classes
        public bool activated = false;
        public int timer;
        public Queue<SpellStage> sequence;

        // Start is called before the first frame update
        public abstract void activate();
        public abstract void activeUpdate();
        // Update is called once per frame
        void Update() {
            //unactivated objects trigger functionality after delay
            //once activated, they persist until ttl hits 0
            //consider making this a component to add instead of being in update function
            if (!activated) {
                timer--;
                if (timer <= 0) {
                    activate();
                    activated = true;
                }
            } else {
                activeUpdate();
            }
        }
    }
}