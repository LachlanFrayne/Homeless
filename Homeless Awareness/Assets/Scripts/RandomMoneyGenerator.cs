using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace RandomMoneyGenerator
{
    public class RandomMoneyGenerator : MonoBehaviour
    {
        public Text moneyTxt;
        public float playerMoney = 0;
        [HideInInspector] public float randomMoney;        //other classes might like to reference this

        [SerializeField] Vector2 randomMoneyRange = new Vector2(5, 20);        //Give 5 to 20 dollars
        [SerializeField] Vector2 randomTimeRange = new Vector2(1, 3);        //1 to 3 seconds between getting money

        float randomTime;

        void Start()
        {
            randomTime = UnityEngine.Random.Range(randomMoneyRange.x, randomMoneyRange.y);
            StartCoroutine(GenerateRandomMoney());
        }

        IEnumerator GenerateRandomMoney()
        {
            while (true)
            {
                //Keep 
                yield return new WaitForSeconds(randomTime);

                //Set new random time
                randomTime = UnityEngine.Random.Range(randomTimeRange.x, randomTimeRange.y);

                //Generate a random amount of money
                randomMoney = UnityEngine.Random.Range(randomMoneyRange.x, randomMoneyRange.y);

                //Give money
                playerMoney += randomMoney;
                moneyTxt.text = playerMoney.ToString();
            }
        }

        //These can be referenced outside to start of stop this script ie. unity events
        public void StartGeneratingMoney()
        {
            StartCoroutine(GenerateRandomMoney());
        }
        public void StopGeneratingMoney()
        {
            StopCoroutine(GenerateRandomMoney());
        }
    }
}