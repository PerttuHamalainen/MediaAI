/*

Aalto Game Tools license

Copyright (C) 2012 Perttu Hämäläinen

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

*/

using UnityEngine;
using System.Collections;
using System;

namespace AaltoGames
{
    //Holt-Winters double exponential smoothing
    public class DoubleExponential
    {
        float x;
        float dx;  //currently estimated trend
        float oldInput,oldOldInput;
        public float smoothness, trendSmoothness;
        public float damping;
        public DoubleExponential(float initialValue, float smoothness, float trendSmoothness, float trendDamping)
        {
            x = initialValue;
            this.smoothness = smoothness;
            this.trendSmoothness = trendSmoothness;
            dx = 0;
            damping = trendDamping;
            oldInput = initialValue;
            oldOldInput = initialValue;
        }
        public float putSample(float newInput, float timeStepSeconds = 1.0f)
        {
            float trendSmoothnessPow = trendSmoothness;
            float smoothnessPow = smoothness;
            float dampingPow = damping;
            //account for variable time step if necessary
            if (timeStepSeconds != 1.0f)
            {
                trendSmoothnessPow = Mathf.Pow(trendSmoothnessPow, timeStepSeconds);
                smoothnessPow = Mathf.Pow(smoothnessPow, timeStepSeconds);
                dampingPow = Mathf.Pow(damping, timeStepSeconds);
            }
            float oldx = x;
            //update output
            x = smoothnessPow * (x + dx * timeStepSeconds) + (1 - smoothnessPow) * newInput;
//            x = smoothnessPow * x + (1 - smoothnessPow) * newInput + dx * timeStepSeconds;  //alternative, the smoothness parameter does not have an effect on the contribution of the estimated trend
            //update trend estimate using a second single-exponential filter
            dx = trendSmoothnessPow * dx + (1.0f - trendSmoothnessPow) * (x - oldx) / timeStepSeconds;
            //damping biases the trend (velocity) estimate towards 0
            dx = dx * (1.0f - dampingPow);
            oldOldInput = oldInput;
            oldInput = newInput;
            return x;
        }
        public float output
        {
            get { return x; }
        }
        public void reset(float initialValue)
        {
            x = initialValue;
        }
    };
    public class SingleExponential
    {
        public float x;
        public float smoothness;
        public SingleExponential(float initialValue, float smoothness)
        {
            x = initialValue;
            this.smoothness = smoothness;
        }
        public float putSample(float newInput, float timeStepSeconds = 1.0f)
        {
            float smoothnessPow = smoothness;
            if (timeStepSeconds != 1.0f)
            {
                smoothnessPow = Mathf.Pow(smoothnessPow, timeStepSeconds);
            }
            x = smoothnessPow * x + (1.0f - smoothnessPow) * newInput;
            return x;
        }
        public float putSampleAndOldOutput(float newInput, float oldOutput, float timeStepSeconds = 1.0f)
        {
            x = oldOutput;
            return putSample(newInput, timeStepSeconds);
        }
        public float output
        {
            get { return x; }
        }
        public void reset(float initialValue)
        {
            x = initialValue;
        }
    };

    //Single exponential but for angles, wraps the angles to closest
    public class AngleSingleExponential
    {
        public float x;
        public float smoothness;
        public AngleSingleExponential(float initialValue, float smoothness)
        {
            x = initialValue;
            this.smoothness = smoothness;
        }
        public float putSample(float value)
        {
            return putSample(value, 1.0f);
        }
        public float putSample(float newInput, float timeStepSeconds = 1.0f)
        {
            newInput = GeometryUtils.wrapAngleToClosest(newInput, x);
            float smoothnessPow = smoothness;
            if (timeStepSeconds != 1.0f)
            {
                smoothnessPow = Mathf.Pow(smoothnessPow, timeStepSeconds);
            }
            x = smoothnessPow * x + (1.0f - smoothnessPow) * newInput;
            x = GeometryUtils.wrapAngleToClosest(x, 0);
            return x;
        }
        public float output
        {
            get { return x; }
        }
        public void reset(float initialValue)
        {
            x = initialValue;
        }
    };
    public class Vector3SingleExponential
    {
        public Vector3 x;
        float oldInput;
        public float smoothness;
        public Vector3SingleExponential(Vector3 initialValue, float smoothness)
        {
            x = initialValue;
            this.smoothness = smoothness;
        }
        public Vector3 putSample(Vector3 value)
        {
            return putSample(value, 1.0f);
        }
        public Vector3 putSample(Vector3 newInput, float timeStepSeconds)
        {
            float smoothnessPow = smoothness;
            if (timeStepSeconds != 1.0f)
            {
                smoothnessPow = Mathf.Pow(smoothnessPow, timeStepSeconds);
            }
            x = smoothnessPow * x + (1.0f - smoothnessPow) * newInput;
            return x;
        }
        public Vector3 output
        {
            get { return x; }
        }
        public void reset(Vector3 initialValue)
        {
            x = initialValue;
        }
    };

    //moving average (running mean)
    public class RunningMean
    {
        float[] delay;
        int delayPos;
        public float output;
        public int N;  //this must stay between 0 and the initial N given to constructor
        public RunningMean(int N)
        {
            init(N);
        }
        void init(int N)
        {
            this.N = N;
            delay = new float[N];
            for (int i = 0; i < N; i++)
            {
                delay[i] = 0;
            }
            delayPos = 0;

        }
        ///Inserts a new input sample to the filter.
        public void putSample(float x)
        {
            if (delay.Length != N)
                init(N);

            delayPos = (delayPos + 1) % delay.Length;
            delay[delayPos] = x;
            float out_tmp = 0;
            for (int i = 0; i < N; i++)
            {
                int index = (delayPos + delay.Length - i) % delay.Length;
                out_tmp += delay[index];
            }
            output = out_tmp / (float)N;
        }
    };

    public class RunningVector3Mean
    {
        Vector3[] delay;
        int delayPos;
        public Vector3 output;
        public int N;  //this must stay between 0 and the initial N given to constructor
        public RunningVector3Mean(int N)
        {
            this.N = N;
            delay = new Vector3[N];
            for (int i = 0; i < N; i++)
            {
                delay[i] = Vector3.zero;
            }
            delayPos = 0;
        }
        ///Inserts a new input sample to the filter.
        public Vector3 putSample(Vector3 x)
        {
            delayPos = (delayPos + 1) % delay.Length;
            delay[delayPos] = x;
            Vector3 out_tmp = Vector3.zero;
            for (int i = 0; i < N; i++)
            {
                int index = (delayPos + delay.Length - i) % delay.Length;
                out_tmp += delay[index];
            }
            output = out_tmp * (1.0f / (float)N);
            return output;
        }
    };
    public class Vector3Delay
    {
        Vector3[] delay;
        int delayPos;
        public Vector3 output;
        int N;
        public Vector3Delay(int N)
        {
            this.N = N;
            delay = new Vector3[N];
            for (int i = 0; i < N; i++)
            {
                delay[i] = Vector3.zero;
            }
            delayPos = 0;
        }
        ///Inserts a new input sample to the filter.
        public void putSample(Vector3 x)
        {
            delayPos = (delayPos + 1) % N;
            delay[delayPos] = x;
        }
        public Vector3 getDelayed(int delayAmount)
        {
            delayAmount = Mathf.Min(delayAmount, N - 1);
            int index = (delayPos + N - delayAmount) % N;
            return delay[index];
        }
    };

    public class FloatDelay
    {
        float[] delay;
        int delayPos;
        public float output;
        int N;
        public FloatDelay(int N)
        {
            this.N = N;
            delay = new float[N];
            for (int i = 0; i < N; i++)
            {
                delay[i] = 0;
            }
            delayPos = 0;
        }
        ///Inserts a new input sample to the filter.
        public void putSample(float x)
        {
            delayPos = (delayPos + 1) % N;
            output = delay[delayPos];
            delay[delayPos] = x;
        }
        public float getDelayed(int delayAmount)
        {
            delayAmount = Mathf.Min(delayAmount, N - 1);
            int index = (delayPos + N - delayAmount) % N;
            return delay[index];
        }
    };

    //from: http://stackoverflow.com/questions/8079526/lowpass-and-high-pass-filter-in-c-sharp
    public class ResonantLowpass
    {
        /// <summary>
        /// rez amount, from sqrt(2) to ~ 0.1
        /// </summary>
        private float resonance;

        private float frequency;
        private float sampleRate;

        private float c, a1, a2, a3, b1, b2;

        /// <summary>
        /// Array of input values, latest are in front
        /// </summary>
        private float[] inputHistory = new float[2];

        /// <summary>
        /// Array of output values, latest are in front
        /// </summary>
        private float[] outputHistory = new float[3];
        private float[] savedInputHistory=new float[2];
        private float[] savedOutputHistory = new float[3];
        public void saveState()
        {
            Array.Copy(inputHistory, savedInputHistory, 2);
            Array.Copy(outputHistory, savedOutputHistory, 3);
        }
        public void restoreState()
        {
            Array.Copy(savedInputHistory, inputHistory, 2);
            Array.Copy(savedOutputHistory, outputHistory, 3);
        }
        public ResonantLowpass(float frequency, float sampleRate, float resonance)
        {
            setParams(frequency, sampleRate, resonance);
        }
        //resonance from 0...100
        public void setParams(float frequency, float sampleRate, float resonance)
        {
            //resonance = Mathf.Sqrt(2.0f)/(1.0f+resonance);
            resonance = 2.0f/(1.0f+resonance);
            this.resonance = resonance;
            this.frequency = frequency;
            this.sampleRate = sampleRate;

            c = 1.0f / (float)Mathf.Tan(Mathf.PI * frequency / sampleRate);
            a1 = 1.0f / (1.0f + this.resonance * c + c * c);
            a2 = 2f * a1;
            a3 = a1;
            b1 = 2.0f * (1.0f - c * c) * a1;
            b2 = (1.0f - resonance * c + c * c) * a1;
        }

        public float putSample(float newInput)
        {
            float newOutput = a1 * newInput + a2 * this.inputHistory[0] + a3 * this.inputHistory[1] - b1 * this.outputHistory[0] - b2 * this.outputHistory[1];

            this.inputHistory[1] = this.inputHistory[0];
            this.inputHistory[0] = newInput;

            this.outputHistory[2] = this.outputHistory[1];
            this.outputHistory[1] = this.outputHistory[0];
            this.outputHistory[0] = newOutput;
            return output;
        }

        public float output
        {
            get { return this.outputHistory[0]; }
        }
    }
} //AaltoGames