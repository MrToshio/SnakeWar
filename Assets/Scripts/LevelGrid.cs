using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid
{
    // Start is called before the first frame update
    private int width;
    private int height;

    public LevelGrid(int width, int height) {
        this.width = width;
        this.height = height;
    }

    public bool validateGridPosition(Vector2Int gridPoisition) {
        if (gridPoisition.x<0 || gridPoisition.y<0 || gridPoisition.x>width || gridPoisition.y>height) {
            return false;
        }
        return true;
    }
}
