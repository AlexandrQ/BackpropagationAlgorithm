using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace интелл_системы_нейросеть_попытка2 {
    class perceptron3 {
        List<double> inList = new List<double>();
        List<double> outList = new List<double>();
        List<double> hiddenList = new List<double>();
        List<List<double>> w1List = new List<List<double>>();
        List<List<double>> w2List = new List<List<double>>();
        double alfa = 0.01;
        double h = 1;

        List<List<double>> error = new List<List<double>>();

        Random rnd = new Random();

        public perceptron3(int inCount, int hiddenCount, int outCount) {
            for (int i = 0; i < inCount; i++) {
                inList.Add(0);
            }
            for (int i = 0; i < inCount; i++) {
                w1List.Add(new List<double>());
                for (int j = 0; j < hiddenCount; j++) {
                    w1List[i].Add(rnd.NextDouble() / 10);
                }
            }
            error.Add(new List<double>());
            for (int i = 0; i < hiddenCount; i++) {
                w2List.Add(new List<double>());
                for (int j = 0; j < outCount; j++) {
                    w2List[i].Add(rnd.NextDouble() / 10);
                }
                error[0].Add(0);
            }
            for (int i = 0; i < hiddenCount; i++) {
                hiddenList.Add(0);
            }
            error.Add(new List<double>());
            for (int i = 0; i < outCount; i++) {
                outList.Add(0);
                error[1].Add(0);
            }
        }

        public void inData(List<double> listData) {
            for (int i = 0; i < inList.Count; i++) {
                inList[i] = listData[i];
            }
        }

        public List<double> work() {
            for (int i = 0; i < hiddenList.Count; i++) {
                //hiddenList[i] = Math.Tanh(-sumHidden(i) / h);
                hiddenList[i] = 1 / (1 + Math.Exp(-sumHidden(i) / h));
            }
            for (int i = 0; i < outList.Count; i++) {
                //outList[i] = Math.Tanh(-sumOut(i) / h);
                outList[i] = 1 / (1 + Math.Exp(-sumOut(i) / h));
            }
            return outList;
        }
        double sum_err = 0;
        public void train(List<double> standart) {
            for (int i = 0; i < hiddenList.Count; i++) {
                hiddenList[i] = 1 / (1 + Math.Exp(-sumHidden(i) / h));
            }
            for (int i = 0; i < outList.Count; i++) {
                outList[i] = 1 / (1 + Math.Exp(-sumOut(i) / h));
            }

            for (int i = 0; i < outList.Count; i++) {
                error[1][i] = outList[i] * (1 - outList[i]) * (standart[i] - outList[i]);
            }
            for (int j = 0; j < outList.Count; j++) {
                for (int i = 0; i < hiddenList.Count; i++) {
                    w2List[i][j] += alfa * error[1][j] * hiddenList[i];
                }
            }
            for (int i = 0; i < hiddenList.Count; i++) {
                sum_err = 0;
                for (int j = 0; j < outList.Count; j++) {
                    sum_err += error[1][j] * w2List[i][j];
                }
                error[0][i] = hiddenList[i] * (1 - hiddenList[i]) * sum_err;
            }
            for (int j = 0; j < hiddenList.Count; j++) {
                for (int i = 0; i < inList.Count; i++) {
                    w1List[i][j] += alfa * error[0][j] * inList[i];
                }
            }
        }

        public double sumHidden(int n) {
            double s = 0;
            for (int i = 0; i < inList.Count; i++) {
                s += w1List[i][n] * inList[i];
            }
            return s;
        }

        public double sumOut(int n) {
            double s = 0;
            for (int i = 0; i < hiddenList.Count; i++) {
                s += w2List[i][n] * hiddenList[i];
            }
            return s;
        }
    }
}
