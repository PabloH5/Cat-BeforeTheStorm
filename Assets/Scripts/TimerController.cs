using UnityEngine;
using UnityEngine.UI;
namespace DefaultNamespace
{
    public class TimerController : MonoBehaviour
    {
        public float timeLeft;
        [SerializeField] private Text timerTxt;

        void Update()
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime; 
                timeLeft = Mathf.Max(timeLeft, 0); 

                timerTxt.text = Mathf.CeilToInt(timeLeft).ToString(); 
            }
            else
            {
                timerTxt.text = "0"; 
            }
        }
    }
}