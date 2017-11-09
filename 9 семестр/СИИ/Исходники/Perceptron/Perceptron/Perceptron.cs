using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron
{
    public class Perceptron
    {
        /// <summary>
        /// Массив входных нейронов
        /// </summary>
        private List<double> Enters { get; set; }

        /// <summary>
        /// Выходной нейрон
        /// </summary>
        private double Outer { get; set; }

        /// <summary>
        /// Весовые коэффициенты
        /// </summary>
        private List<double> Weights { get; set; }

        double[,] Patterns = {
            { 0, 0, 1 },
            { 0, 1, 0},
            { 1, 0, 0},
            { 1, 1, 0}
        };

        public Perceptron()
        {
            Enters = new List<double>();
            Weights = new List<double>();
        }

        public void GenerateNewWeights(int count)
        {
            Weights.Clear();
            for (int i = 0; i < count; ++i)
            {
                Weights.Add((new Random()).Next());
            }
            Weights.Add((new Random()).Next());
        }

        public void CalculateOuter()
        {
            Outer = 0;
            for (int i = 0; i < Enters.Count; ++i)
            {
                Outer += Enters[i] * Weights[i];
            }

            Outer = Outer > 0.5 ? 1 : 0;
        }

        public void Study(List<StudyPattern> patterns)
        {
            if (patterns == null || !patterns.Any())
            {
                return;
            }                

            double? globalError = null;
            GenerateNewWeights(patterns.First().StudyEnters.Count);
            
            while (globalError != 0)
            {
                globalError = 0;
                for (int p = 0; p < patterns.Count; ++p)
                {
                    var studyOut = patterns[p].StudyOut;
                    Enters.Clear();
                    Enters.AddRange(patterns[p].StudyEnters);

                    CalculateOuter();

                    var localError = studyOut - Outer;
                    globalError += Math.Abs(localError);

                    for (int i = 0; i < Enters.Count; ++i)
                    {
                        Weights[i] += 0.1 * localError * Enters[i];
                    }
                }
            }
        }

        public void Test()
        {
            var patterns = new List<StudyPattern>();
            patterns.Add(new StudyPattern { StudyEnters = new List<double> { 0, 0, 1 }, StudyOut = 1 });
            patterns.Add(new StudyPattern { StudyEnters = new List<double> { 0, 1, 1 }, StudyOut = 0 });
            patterns.Add(new StudyPattern { StudyEnters = new List<double> { 1, 0, 1 }, StudyOut = 0 });
            patterns.Add(new StudyPattern { StudyEnters = new List<double> { 1, 1, 1 }, StudyOut = 0 });
            Study(patterns);

            for (int p = 0; p < patterns.Count; ++p)
            {
                Enters.Clear();
                Enters.AddRange(patterns[p].StudyEnters);

                CalculateOuter();
            }
        }
    }

    public class StudyPattern
    {
        public List<double> StudyEnters { get; set; }

        public double StudyOut { get; set; }
    }
}
