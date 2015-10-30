using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;

namespace SpaceDoctor.ViewModel
{
    public  class XPlotVM
    {
        #region fields
        readonly PlotModel _plotModel;


        #endregion

        #region ctors
        public XPlotVM()
        {
            _plotModel = new PlotModel();
            _plotModel.Series.Add(new FunctionSeries(Math.Cos, 0, 12, 0.1, "cos"));
        }

        #endregion

        #region properties
        public PlotModel Plot
        {
            get
            {
                return _plotModel;
            }
        }
        #endregion

        #region commands
        #endregion

        #region methods
        #endregion

        #region eventHandlers
        #endregion

    }
}
