using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

public class Snake : MonoBehaviour
{
    private Vector2Int gridPosition;
    private float gridMoveTimer;
    private float gridMoveTimerMax;
    private Vector2Int gridMoveDirection;
    private List<Vector2Int> movePositionGridList;

    private LevelGrid levelGrid;
    private float ShieldedTimer;
    private enum Spell {
        TP,
        Shield,
        SpeedBoost,
        Cameha,
        Nothing
    }
    private enum State {
        Alive,
        Dead,
        Shielded
    }

    private Spell spell;
    
    private State state;

    private void Awake()
    {
        gridPosition = new Vector2Int(10,10);
        gridMoveTimerMax = 0.1f;
        gridMoveTimer = gridMoveTimerMax;
        gridMoveDirection = new Vector2Int(1,0);
        movePositionGridList = new List<Vector2Int>();
        state = State.Alive;
        levelGrid = new LevelGrid(20,20);
    
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            spell = Spell.TP;
        }

        if (state == State.Alive){
            HandleInput();
            HandleMovement();
        } else {
            CMDebug.TextPopup("game over!", transform.position);
        }
    }
    
    private void HandleInput() {
        if (Input.GetKeyDown(KeyCode.UpArrow)){
            if (gridMoveDirection.y != -1){
                if (spell == Spell.TP){
                    gridMoveDirection.y = 4;
                    gridMoveDirection.x = 0;
                } else {
                gridMoveDirection.y = 1;
                gridMoveDirection.x = 0;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)){
            if (gridMoveDirection.y != 1){
                if (spell == Spell.TP){
                    gridMoveDirection.y = -4;
                    gridMoveDirection.x = 0;
                } else {
                gridMoveDirection.y = -1;
                gridMoveDirection.x = 0;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)){
            if (gridMoveDirection.x != 1){
                if (spell == Spell.TP){
                    gridMoveDirection.y = 0;
                    gridMoveDirection.x = -4;
                } else {
                gridMoveDirection.y = 0;
                gridMoveDirection.x = -1;
                }
            }

        }
        if (Input.GetKeyDown(KeyCode.RightArrow)){
            if (gridMoveDirection.x != -1){
                if (spell == Spell.TP){
                    gridMoveDirection.y = 0;
                    gridMoveDirection.x = 4;
                } else {
                    gridMoveDirection.y = 0;
                    gridMoveDirection.x = 1;
                }
            }
        }
    }
    private void HandleMovement()
    {
        gridMoveTimer += Time.deltaTime;
        if (spell == Spell.TP)
        {
            gridPosition += gridMoveDirection;
            spell = Spell.Nothing;

        }

        if (gridMoveTimer > gridMoveTimerMax){

            movePositionGridList.Insert(0,gridPosition);

            gridPosition += gridMoveDirection;
            gridMoveTimer -= gridMoveTimerMax;
            
            for (int i = 0; i < movePositionGridList.Count; i++){
                Vector2Int movePosition = movePositionGridList[i];
                World_Sprite worldSprite = World_Sprite.Create(new Vector3(movePosition.x, movePosition.y),Vector3.one * .5f, Color.white);
            }

            foreach (Vector2Int movePosition in movePositionGridList){
                if (gridPosition == movePosition) {
                    CMDebug.TextPopup("DEAD!", transform.position);
                    state = State.Dead;
                }
            }
            transform.position = new Vector3(gridPosition.x, gridPosition.y);
            if (!levelGrid.validateGridPosition(gridPosition)){
                CMDebug.TextPopup("DEAD", transform.position);
                state = State.Dead;
            }
            Debug.Log("removal tp");
            transform.eulerAngles = new Vector3(0,0,getAngleFromVector(gridMoveDirection)-90);

        }
    }
    private float getAngleFromVector(Vector2Int dir)
    {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n <0) n += 360 ;
        return n;
    }
}
