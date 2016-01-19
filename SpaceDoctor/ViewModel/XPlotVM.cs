using System;
using System.Collections.Generic;
using System.Linq;
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
        public void CreatePlot(IEnumerable<KeyValuePair <DateTime, Double>> paramCollection, String legendTitle)
        {
            //преобразует в dataPoint
            List<DataPoint> dpList = new List<DataPoint>(50);

            List<Double> pointsDate = new List<Double>(dpList.Count);

            var p = new List<ScatterPoint>();



            foreach(var v in paramCollection)
            {
                dpList.Add(new DataPoint(DateTimeAxis.ToDouble(v.Key), v.Value));

                pointsDate.Add(DateTimeAxis.ToDouble(v.Key));
            }

            //добавляет ось со значениями

            _plotModel.Axes.Add(new DateTimeAxis() { Position = AxisPosition.Bottom, StringFormat = "dd-MM hh-mm" });
            _plotModel.Axes.Add(new LinearAxis());
            LineSeries ls = new LineSeries();

            ScatterSeries css = new ScatterSeries();

            foreach (var v in paramCollection)
            {
                ScatterPoint sp = new ScatterPoint(DateTimeAxis.ToDouble(v.Key), v.Value);     
                p.Add(sp);               
            }

            css.ItemsSource = p;
           
            ls.ItemsSource = dpList;

            ls.CanTrackerInterpolatePoints = false;
           
            
             _plotModel.Series.Add(ls);
            _plotModel.Series.Add(css);

            //легенда:
            _plotModel.LegendPosition = LegendPosition.TopCenter;
            _plotModel.LegendTitle = legendTitle;

            //сетка:
            _plotModel.Axes[0].MajorGridlineStyle = LineStyle.None;
            _plotModel.Axes[0].ExtraGridlines = pointsDate.ToArray<Double>();
            _plotModel.Axes[0].ExtraGridlineStyle = LineStyle.Dot;

            _plotModel.Series[0].SelectionMode = SelectionMode.Single;

            _plotModel.Axes[1].MajorStep = 10;
            _plotModel.Axes[1].Maximum = dpList.Max<DataPoint>(point => point.Y) + 10;
            _plotModel.Axes[1].Minimum = dpList.Min<DataPoint>(dp => dp.Y) - 10;
            
        }
        #endregion

        #region eventHandlers
        #endregion

    }
}
