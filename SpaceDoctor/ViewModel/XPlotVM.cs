using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;

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
            List<DataPoint> dpList = new List<DataPoint>();



           // for (int i = 0; i <= 10; ++i)
                dpList.Add(new DataPoint(DateTimeAxis.ToDouble(DateTime.Parse("12/03/2015 22:12")) , 0.25));
         
            dpList.Add(new DataPoint(DateTimeAxis.ToDouble(DateTime.Parse("12/03/2015 02:12")), 10.25));



            _plotModel = new PlotModel();
            _plotModel.Axes.Add(new DateTimeAxis() { Position = AxisPosition.Bottom, StringFormat = "dd-MM hh-mm"});
           // _plotModel.Axes.Add(new LinearAxis ());
            //_plotModel.Series.Add(new FunctionSeries(Math.Cos, 0, 12, 0.1, "cos"));
            LineSeries ls = new LineSeries();

            ls.ItemsSource = dpList;
            _plotModel.Series.Add(ls);
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
