using SpaceDoctor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDoctor.ViewModel
{
    class XParamVM : XViewModelBase
    {
        readonly XParam _param;
        readonly XParamTypeVM _paramType;

        public XParamVM()
        {
            _param = new XParam();
            _paramType = new XParamTypeVM();
        }

        public XParamVM(XParam param)
        {
            _param = param;
            _paramType = new XParamTypeVM(param.Type);
        }

        public Double Value 
        {
            get 
            {
                return Param.Value;
             }
             set
             {
                Param.Value = value;
                RaisePropertyChanged("Value");
             }
        }


        public XParam Param
        {
            get
            {
                return _param;
            }
        }

        public  XParamTypeVM ParamType
        {
            get
            {
                return _paramType;
            }
        }

    }
}
