using System;
using System.Windows.Forms;

namespace DrawingShapes.Sample
{
    public class RunnningScope : IDisposable
    {
        public RunnningScope()
        {
            MessageBox.Show("開始します。");
        }

        public void Dispose()
        {
            MessageBox.Show("終了します。");
        }
    }
}