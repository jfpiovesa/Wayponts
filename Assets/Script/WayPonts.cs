using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPonts : MonoBehaviour
{
    // variaveis
        public GameObject[] waypoints;// objetos que severam de ponto de trasissão
        int currentWP = 0;

    [SerializeField]
        float speed = 1.0f;// velocidade do objeto que ira patrulhar

    [SerializeField]
        float accuracy = 1.0f;// velocida de quando chega no objeto do waypoints
        float rotSpeed = 0.4f;// rotação na hora de ir pra outro ponto

         Animator aima; // controle de animação
        void Start()
        {
            waypoints = GameObject.FindGameObjectsWithTag("waypoint");// para ver o objeto a seguir 
            aima = GetComponent<Animator>(); // para poder pegar animação do objeto
        }
        void Update()
        {
            if (waypoints.Length == 0) return;
            Vector3 lookAtGoal = new Vector3(waypoints[currentWP].transform.position.x, this.transform.position.y, waypoints[currentWP].transform.position.z);// para poder andar nos eixos x e z ;

            Vector3 direction = lookAtGoal - this.transform.position;//  faz que mova na direção
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);// quando for ter rotação para virar  o objeto
          
       
            if (direction.magnitude < accuracy)
            {
                currentWP++;
                if (currentWP >= waypoints.Length)
                {
                    currentWP = 0;
                }
            }
            this.transform.Translate(0, 0, speed * Time.deltaTime);
             aima.SetBool("walk", true);
        } 
}
