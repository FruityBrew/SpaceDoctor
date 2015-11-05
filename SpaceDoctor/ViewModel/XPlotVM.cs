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
    public class XPlotVM
    {
        #region fields
        readonly PlotModel _plotModel;


        #endregion

        #region ctors
        public XPlotVM()
        {
            _plotModel = new PlotModel();
           // _plotModel.Series.Add(new FunctionSeries(Math.Cos, 0, 12, 0.1, "cos"));
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
        public void CreatePlot(IEnumerable<KeyValuePair <DateTime, Double>> paramCollection)
        {
            //преобразует в dataPoint
            List<DataPoint> dpList = new List<DataPoint>(50);

            foreach(var v in paramCollection)
            {
                dpList.Add(new DataPoint(DateTimeAxis.ToDouble(v.Key), v.Value));
            }
            //добавляет ось со значениями

            _plotModel.Axes.Add(new DateTimeAxis() { Position = AxisPosition.Bottom, StringFormat = "dd-MM hh-mm" });

            LineSeries ls = new LineSeries();

            ls.ItemsSource = dpList;
            _plotModel.Series.Add(ls);
        }
        #endregion

        #region eventHandlers
        #endregion

    }
}
