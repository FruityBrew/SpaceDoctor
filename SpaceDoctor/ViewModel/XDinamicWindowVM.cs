using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDoctor.ViewModel
{
    public class XDinamicWindowVM
    {
        #region properties

        readonly IEnumerable<XParamTypeVM> _paramTypeCollection;

        XPlotVM _plot;

        #endregion

        #region ctors

        public XDinamicWindowVM(IEnumerable<XParamTypeVM> paramTypes)
        {
            _paramTypeCollection = paramTypes;
        }
        #endregion

        #region properties

        public IEnumerable<XParamTypeVM> ParamTypeCollection
        {
            get
            {
                return _paramTypeCollection;
            }
        }

        public XPlotVM Plot
        {
            get
            {
                return _plot;
            }

            set
            {
                _plot = value;
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
