/**
 * This file is part of the materials for the course "Intelligent Computational Media" (formerly: "Computational Intelligence in Games")
 * of Aalto University, Finland.
 *
 * You are free to use this code for any purpose, but at your own risk.
 * */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ICM
{
    public class OptimizationSample : System.IComparable
    {
        public double[] x;
        public double objectiveFuncVal;
        public OptimizationSample(int dim = 0)
        {
            if (dim != 0)
            {
                x = new double[dim];
            }
            objectiveFuncVal = 0;
        }
        public int CompareTo(object obj)
        {
            OptimizationSample other = obj as OptimizationSample;
            if (objectiveFuncVal > other.objectiveFuncVal)
                return 1;
            else if (objectiveFuncVal == other.objectiveFuncVal)
                return 0;
            return -1;
        }
    }
    public enum OptimizationModes
    {
        minimize = 0, maximize
    }

} //namespace IGM
