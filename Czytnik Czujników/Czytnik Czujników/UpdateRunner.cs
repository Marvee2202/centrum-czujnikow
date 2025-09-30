using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Czytnik_Czujników.ViewModels;

namespace Czytnik_Czujników
{
    public class UpdateRunner
    {
        public void Subscribe(MainViewModel m)
        {
            m.IntervalReached += c_IntervalReached;
            m.GenerateList();
        }

        public void Unsubscribe(MainViewModel m)
        {
            m.IntervalReached -= c_IntervalReached;
        }

        void c_IntervalReached(object sender, EventArgs e)
        {
            if (sender is MainViewModel m)
            {
                m.GenerateList();
            }
        }
    }
}
