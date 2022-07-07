using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructMountain : MonoBehaviour
{
    public LevelControllerCont1J levelController;
    public GameObject[] scenarios = new GameObject[9];

    public int scenarioNumber;
    
    public void constructMountain(){
        //Seleccionar escenario aleatoriamente
        scenarioNumber = Random.Range(0, scenarios.Length);

        //Activar el escenario seleccionado aleatoriamente
        scenarios[scenarioNumber].SetActive(true);

        //Asignar valor de la respuesta correcta con Split para obtener solo el numero
        levelController.correctAnswer = int.Parse(scenarios[scenarioNumber].name.Split(' ')[1]);

        //Asignar carrito y rail final a objetos del level controller
        levelController.car = scenarios[scenarioNumber].transform.Find("Carrito").gameObject;
        levelController.finalRail = scenarios[scenarioNumber].transform.Find("Riel Final").gameObject;
    }
}
