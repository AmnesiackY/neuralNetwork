using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace НедоНейроСеть
{
    class InputLayer
    {
        private (double[], double[])[] _trainset = new (double[], double[])[]
        {
            (new double[]{0, 0}, new double[]{0, 1}),
            (new double[]{0, 0}, new double[] { 0, 1 }),
            (new double[]{ 0, 0 }, new double[] { 0, 1 }),
            (new double[]{ 0, 0 }, new double[] { 0, 1 }),
        };
        public (double[], double[])[] Trainset {  get => _trainset; }
    }
}
