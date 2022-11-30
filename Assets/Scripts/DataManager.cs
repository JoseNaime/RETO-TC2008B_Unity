using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//
public class DataManager : MonoBehaviour
{
    //
    [SerializeField]
    private Carro[] _cars;

    [SerializeField]
    private Semaforo[] _trafic_lights;

    private GameObject[] _carrosGO = null;

    private Light[] _semaforosGO = null;

    private Vector3[] _direcciones;

    private void PosicionarCarros()
    {
        // Iniciar Cars
        if (_carrosGO == null || _carrosGO.Length == 0)
        {
            print("Iniciar carros");
            _carrosGO = new GameObject[_cars.Length];
            print("PosicionarCarros carrosGo Lenght: " + _carrosGO.Length);
            for (int i = 0; i < _carrosGO.Length; i++)
            {
                _carrosGO[i] =
                    CarPoolManager.Instance.ActivarObjeto(Vector2.zero);
            }
        }

        // Finalizar cars
        print("Carros Go: " + _carrosGO.Length);
        print("PosicionarCarros _lenght: " + _cars.Length);
        for (int cr = 0; cr < _cars.Length; cr++)
        {
            CarProperties carComponent =
                _carrosGO[cr].GetComponent<CarProperties>();
            float _x = _cars[cr].position[0];
            float _z = _cars[cr].position[1];
            _carrosGO[cr].transform.position = new Vector3(_x, 0, _z);
        }
    }

    // private void CambiarSemaforos()
    // {
        // Iniciar Traffic Lights
        // if (_semaforosGO == null || _semaforosGO.Length == 0)
        // {
        //     print("Iniciar semaforos");
        //     _semaforosGO = new Light[_trafic_lights.Length];
        //     print("CambiarSemaforos semaforosGo Lenght: " +
        //     _semaforosGO.Length);
        //     for (int tl = 0; tl < _semaforosGO.Length; tl++)
        //     {
        //         int _s = _trafic_lights[tl].state;
        //         _semaforosGO[tl] = SMPoolManager.Instance.ActivarObjeto(_s);
        //     }
        // }

        // Finalizar Traffic Lights
        //print("Semaforos Go: " + _semaforosGO.Length);
        //print("CambiarSemaforos _lenght: " + _trafic_lights.Length);
        //for (int tl = 0; tl < _trafic_lights.Length; tl++)
      // {
         //   SMProperties smComponent =
           //     _semaforosGO[tl].GetComponent<SMProperties>();
           //int _s = _trafic_lights[tl].state;
           
       //}
    //}

    public void EscucharRequestConArgumentos(ListSim datos)
    {
        print("DATOS: " + datos);

        StartCoroutine(ConsumirSteps(datos));
    }

    private IEnumerator ConsumirSteps(ListSim datos)
    {
        for (int i = 0; i < datos.steps.Length - 1; i++)
        {
            print("Step: " + i);
            _cars = datos.steps[i].cars;
            //_direcciones = new Vector3[_cars.Length];
            _trafic_lights = datos.steps[i].traffic_lights;
            print("ConsumirSteps cars: " + _cars.Length);

            // print("ConsumirSteps traffic lights: " + _trafic_lights.Lenght);
            PosicionarCarros();
            SMPoolManager.Instance.ActualizarEstados(_trafic_lights);
            //En cada paso calcular vector direccion de cada carro
            

            yield return new WaitForSeconds(0.4f);
        }
    }
}
