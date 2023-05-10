using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    int screenWidthInUnits = 16;
    [SerializeField] float paddleYPosition = 0.4f;
    [SerializeField] float minX = 1f;
    [SerializeField] float maxX = 15f;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        // Step One. FInd the position of the mouse (X)

        //Debug.Log(Input.mousePosition.x);

        // Step Two. Convert mouse position into unity units

        //float mousePositionInUnits = Input.mousePosition.x / Screen.width * screenWidthInUnits;
        // Step Three. Apply units into the paddle
        transform.position = new Vector2(Mathf.Clamp(GetXPosition(), minX, maxX), paddleYPosition);
    }

    private float GetXPosition()
    {
        // If auto play is true  
        if (FindObjectOfType<GameStatus>().GetIsAutoPlayEnabled() == true)
        {
        // then return the ball position
            return FindObjectOfType<Ball>().transform.position.x;
        }

        //otherwise 
        else
        {
        //return mouse position 
           return Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }
        
    }
}
