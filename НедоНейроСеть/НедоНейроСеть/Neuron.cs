using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace НедоНейроСеть
{
    class Neuron
    {
        public Neuron(double[] inputs, double[] weights, NeuronType type)
        {
            _type = type;
            _weights = weights;
            _inputs = inputs;
        }
        private NeuronType _type;
        private double[] _weights;
        private double[] _inputs;
        public double[] Weights { get => _weights; set => _weights = value; }
        public double[] Inputs { get => _inputs; set => _inputs = value; }
        public double Output { get => Activator(_inputs, _weights); }
        private double Activator(double[] i, double[] w)        //преобразования
        {
            double sum = 0;
            for (int l = 0; l < i.Length; ++l)
                sum += i[l] * w[l];                  //линейные 
            return Math.Pow(1 + Math.Exp(-sum), -1); //нелинейные
        }
        public double Derivatinator(double outsignal) => outsignal * (1 - outsignal); //формула производно для текущей функции активации
        public double Gradientor(double error, double dif, double g_sum) => (_type == NeuronType.Output) ? error * dif : g_sum * dif; //g_sum - это сумма градиентов следующего слоя
    }
}
