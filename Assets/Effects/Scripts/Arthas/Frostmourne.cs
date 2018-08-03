using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArthasDomain
{
    public class Frostmourne : Weapon
    {
        AudioSource audioSource;
        private float phyDamageVal = 100;
        private bool isPhyAttack;
        public List<GameObject> colliderObject = new List<GameObject>();
        public string colliderName;
        public bool isCollide;
        public Hero hero;

        protected override void OnEnable()
        {
            base.OnEnable();
            audioSource = GetComponent<AudioSource>();
            //audioSource.Stop();
        }

        public float PhyDamageVal
        {
            get
            {
               return phyDamageVal;
            }

            set
            {
                phyDamageVal = value;
            }
        }

        public void TurnOnPhyAttack(float delayTeime = 0)
        {
            StartCoroutine(openWeapon(delayTeime));
        }
        IEnumerator openWeapon(float time)
        {
            yield return new WaitForSeconds(time);
            isPhyAttack = true;
        }
        public void TurnOffPhyAttack()
        {
            WaitingthenDelete();
            isPhyAttack = false;
        }
        public bool IsPhyAttack()
        {
            return isPhyAttack;
        }

        protected void Start()
        {
            isCollide = false;
        }
        private void Update()
        {
            //if (isPhyAttack)
            //    Debug.Log("aaa");
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != gameObject.layer && other.GetComponent<State>() != null && isPhyAttack)
            {
                Debug.Log(other.name);
                isCollide = true;
                if(!colliderObject.Contains(other.gameObject))
                {
                    colliderObject.Add(other.gameObject);
                    colliderName = other.name;
                    Debug.Log(other.name);
                    Damage phyDamage = new Damage(PhyDamageVal, DamageType.Physical);
                    other.GetComponent<State>().TakeSkillContent(phyDamage);
                }
            }
        }
        void WaitingthenDelete()
        {
            colliderObject.Clear();
            colliderName = null;
            isCollide = false;
        }
    }
}
