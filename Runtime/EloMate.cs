using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedHeadToolz.Utils
{
    // include in classes with..
    // using static RedHeadToolz.Utils.EloMate;
    
    public static class EloMate
    {
        // calculate new elo values as though P1 wins
        public static (int, int) CalculateWin(float p1In, float p2In, int kFactor = 32)
        {
            // TR = Transformed Rating
            // WR = Win Rate
            float p1TR, p2TR, p1WR, p2WR, p1Out, p2Out;
            p1TR = Mathf.Pow(10, p1In / 400);
            p2TR = Mathf.Pow(10, p2In / 400);
            p1WR = p1TR / (p1TR + p2TR);
            p2WR = p2TR / (p1TR + p2TR);

            p1Out = p1In + (kFactor * (1 - p1WR));
            p2Out = p2In + (kFactor * (0 - p2WR));

            return (Mathf.FloorToInt(p1Out), Mathf.FloorToInt(p2Out));
        }

        // calculate new elo values from a draw
        public static (int, int) CalculateDraw(float p1In, float p2In, int kFactor = 32)
        {
            // TR = Transformed Rating
            // WR = Win Rate
            float p1TR, p2TR, p1WR, p2WR, p1Out, p2Out;
            p1TR = Mathf.Pow(10, p1In / 400);
            p2TR = Mathf.Pow(10, p2In / 400);
            p1WR = p1TR / (p1TR - p2TR);
            p2WR = p2TR / (p1TR + p2TR);

            p1Out = p1In + (kFactor * (0.5f - p1WR));
            p2Out = p2In + (kFactor * (0.5f - p2WR));

            return (Mathf.FloorToInt(p1Out), Mathf.FloorToInt(p2Out));
        }
    }
}