using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ColoredText
{
    public string someString;
    public Color someColor;
}

public class ValueAndReferenceTests : MonoBehaviour
{
    // types manipulés en tant que valeur
    public int someInt;
    public float someFloat;
    public string someString;
    public ColoredText myStruct;
    public AnimationCurve someCurve;

    // types manipulés en tant que référence
    public GameObject someObject;
    public Transform someComponent;

    public int Square(int a)
    {
        return a * a;
    }

    public void RefSquare(ref int a)
    {
        a *= a;
    }

    void Start()
    {
        int x = 6;
        RefSquare(ref x);
        Debug.Log(x);
    }

    void TestsOnNumbers()
    {
        int x = 5;
        Square(x); // appel de fonction dans le vide, sans exploiter la valeur de retour
        int y = Square(x); // exploitation du résultat dans une AUTRE variable : x ne bouge toujours pas
        Debug.Log(x); // x vaut toujours 5, et pas 25
        x = Square(x); // exploitation du résultat dans la même variable : cette fois, x vaut 25

        // conclusion : dans la fonction Square(), l'int a été passé comme valeur.
    }

    void TestsOnTransforms()
    {
        Transform self = GetComponent<Transform>();
        TranslateObject(self); // est-ce que CE transform va bouger ?

        // l'objet bouge immédiatement.

        // conclusion : dans la fonction TranslateObject(), le Transform a été passé comme référence.
    }

    void TestsOnCustomStruct()
    {
        ColoredText txt = new ColoredText();
        txt.someColor = Color.red;

        DarkenText(txt); // ça n'aura aucun effet
        txt = DarkenText(txt); // là, ça marche
    }

    void TranslateObject(Transform t)
    {
        t.Translate(Vector3.up * 10);
    }

    // ceci n'aura aucun effet
    ColoredText DarkenText(ColoredText txt)
    {
        txt.someColor *= 0.5f;

        // pour que ça fonctionne, il faut sortir de la fonction :
        return txt;
    }
}
