using System.Runtime.CompilerServices;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

enum StateBack 
{
    FIRST, 
    SECOND
};

public class Background : MonoBehaviour
{

    [SerializeField] Transform firstBack;
    [SerializeField] Transform secondBack;
    [SerializeField] Transform racoon;

    [SerializeField] float firstOffset = 43.0f;
    [SerializeField] float secondOffset = 43.0f;
    [SerializeField] float backPos;


    private StateBack state = StateBack.SECOND;


    private void Update()
    {
        switch(state)
        {
            case StateBack.FIRST:
                {
                    if (racoon.transform.position.x > (firstBack.transform.position.x + backPos))
                    {
                        secondBack.position = new Vector3(secondBack.position.x + secondOffset, 0.0f, 0.0f);
                        state = StateBack.SECOND;
                        Debug.Log("Second");
                    }
                }
                break;

            case StateBack.SECOND:
                {
                    if (racoon.transform.position.x > (secondBack.transform.position.x + backPos))
                    {
                        firstBack.position = new Vector3(firstBack.position.x + firstOffset, 0.0f, 0.0f);
                        state = StateBack.FIRST;
                        Debug.Log("Primero");
                    }
                }
                break;

            default: break;
        }
    }
}
