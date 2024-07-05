using System;
using System.Collections.Generic;
using System.Linq;

namespace DefaultNamespace
{
    public static class MathUtils
    {
        public static int Quantile(float[] probabilities)
        {
            var rnd = new Random();
            var x = rnd.NextDouble();

            var sum = probabilities.Sum();
            var probabilitiesCount = probabilities.Length;

            var currentSum = 0f;
            var lastProbability = 0f;

            for (var i = 0; i < probabilitiesCount - 1; i++)
            {
                currentSum += probabilities[i];

                var maxProbability = currentSum / sum;
                if (lastProbability <= x && x < maxProbability)
                    return i;

                lastProbability = maxProbability;
            }

            return probabilitiesCount - 1;
        }
        
        public static int Quantile(List<float> probabilities)
        {
            var rnd = new Random();
            var x = rnd.NextDouble();

            var sum = probabilities.Sum();
            var probabilitiesCount = probabilities.Count;

            var currentSum = 0f;
            var lastProbability = 0f;

            for (var i = 0; i < probabilitiesCount - 1; i++)
            {
                currentSum += probabilities[i];

                var maxProbability = currentSum / sum;
                if (lastProbability <= x && x < maxProbability)
                    return i;

                lastProbability = maxProbability;
            }

            return probabilitiesCount - 1;
        }
        
        public static bool Approximately(float a, float b)
        {
            return Math.Abs(a - b) < 0.00001f;
        }
    }
}