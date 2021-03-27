using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class FollowPath : MonoBehaviour
{
    Transform goal;
    float speed = 5.0f;  //velocidade do objeto
    float accuracy = 1f; // verificção da distacia do ponto
    float rotSpeed = 2f;// velocidade da rotação

    public GameObject wpManager;//para pegar o objeto que tenha script wpmanager 
    GameObject[] wp;// lista de wp
    GameObject currentNode;
    int currentWP = 0;
    Graph g; 

    // Start is called before the first frame update
    void Start()
    {
        
        wp = wpManager.GetComponent<WpManager>().waypoints;        //peganndo os obejto focando no waypoints wpmanager
        g = wpManager.GetComponent<WpManager>().graph;        //peganndo os obejto graph dentro do wpmanager


        currentNode = wp[0];
    }

    // Update is called once per frame
  
    public void GotoHeli()
    {
        g.AStar(currentNode, wp[1]);
        currentWP = 0;
    }
    public void GoToRuin()
    {
        g.AStar(currentNode, wp[6]);
        currentWP = 0;
    }
    void LateUpdate()
    {
            if(g.getPathLength() == 0 || currentWP == g.getPathLength()) 
            return;
        //O nó que estará mais próximo neste momento
            currentNode = g.getPathPoint(currentWP);

        //se estivermos mais próximo bastante do nó o tanque se moverá para o próximo
        if(Vector3.Distance(g.getPathPoint(currentWP).transform.position, transform.position) < accuracy)
        { currentWP++; }
        // movimento do tank ate proximo no
        if (currentWP < g.getPathLength())
        {
            goal = g.getPathPoint(currentWP).transform;
            Vector3 lookAtGoal = new Vector3(goal.position.x, this.transform.position.y, goal.position.z); 
            Vector3 direction = lookAtGoal - this.transform.position; 
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);
        }

    }

}
