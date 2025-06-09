
using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public struct AudioConfig
{
    public AudioClip entrada;
    public AudioClip centro;
    public AudioClip salida;
}

[Serializable]
public struct TransitionConfig
{
    public TransitionType tipo;
    public float duracion;
    
    public enum TransitionType
    {
        FadeIn,
        FadeOut,
        SlideLeft,
        SlideRight,
        None
    }
}

[Serializable]
public struct InteractionOption
{
    public string nombre;
    public bool esClickeable;
    public string escenaDestino;
    public string slideDestino;
    public string funcionEjecutar;
    public AudioConfig sonidos;
    public TransitionConfig transicion;
}

[CreateAssetMenu(fileName = "SceneData", menuName = "Game/Scene Data")]
public class SceneData : ScriptableObject
{
    [Header("Configuración General")]
    public string nombreEscena;
    public AudioClip musicaFondo;
    public AudioConfig sonidosGenerales;
    public TransitionConfig transicionEntrada;
    public TransitionConfig transicionSalida;
    
    [Header("Opciones de Interacción")]
    public List<InteractionOption> opciones = new List<InteractionOption>();
    
    [Header("Configuración de Slides")]
    public List<Texture2D> imagenesSlide = new List<Texture2D>();
    public List<float> tiemposEspera = new List<float>();
    public bool avanceAutomatico = true;
    public bool permitirSalida = false;
}