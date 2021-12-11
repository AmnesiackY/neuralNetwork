using System.Xml;
using static System.Math;
using static System.Console;

namespace НедоНейроСеть
{
    abstract class Layer
    {
        protected Layer (int non, int nopn, NeuronType nt, string type)
        {
            numOfNeuros = non;
            numOfPrevneurons = nopn;
            Neurons = new Neuron[non];
            double[,] Weights = WeightInitialize(MemoryMode.GET, type);
            for (int i = 0; i < non; ++i)
            {
                double[] temp_weights = new double[nopn];
                for (int j = 0; j < nopn; ++j)
                    temp_weights[j] = Weights[i,j];
                Neurons[i] = new Neuron(null, temp_weights, nt);
                //подали  null на вход нейтронов, т.к. сначала нужно будет преобразовать информацию(видео, изображения, etc.),
                //а загружать input'ы нейронов надо не сразу,
                //а только после вычисления выходов предыдущего слоя
            }
        }
        protected int numOfNeuros; // число нейронов текущего слоя
        protected int numOfPrevneurons; // число нейронов предыдущего слоя
        protected const double learningGrate = 0.1d; //скорость обучения
        Neuron[] _neurons;
        public Neuron[] Neurons { get => _neurons; set => _neurons = value; }
        public double[] Data
        {
            set
            {
                for (int i = 0; i < Neurons.Length; ++i)
                    Neurons[i].Inputs = value;
            }
        }
        public double[,] WeightInitialize (MemoryMode mm, string type)
        {
            double[,] _weights = new double[numOfNeuros, numOfPrevneurons];
            Console.WriteLine($"{type} _weights are being initialized...");
            XmlDocument memory_doc = new XmlDocument();
            memory_doc.Load($"{type}_memory.xml");
            XmlElement memory_el = memory_doc.DocumentElement;
            switch (mm)
            {
                case MemoryMode.GET:
                    for (int l = 0; l < _weights.GetLength(0); ++l)
                        for(int k = 0; k < _weights.GetLength(1); ++k)
                            _weights[l, k] = double.Parse(memory_el.ChildNodes.Item(k + _weights.GetLength(1) * l).InnerText.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
                            // parsing stuff
                        break;
                case MemoryMode.SET:
                    for (int l = 0; l < _weights.GetLength(0); ++l)
                        for (int k = 0; k < _weights.GetLength(1); ++k)
                            memory_el.ChildNodes.Item(k + numOfPrevneurons * l).InnerText = Neurons[l].Weights[k].ToString();
                            break;
            }
            memory_doc.Save($"{type} _memory.xml");
            Console.WriteLine($"{type} weights have been initialized...");
            return _weights;
        }
        abstract public void Recognize(Network net, Layer nextLayer); //для прямых проходов
        abstract public double[] BackwarPass(double[] stuff); //и для обратных
    }
}
