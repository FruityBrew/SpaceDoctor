using SpaceDoctor.Model;
using System;


namespace SpaceDoctor.ViewModel
{
     class XParamVM : XViewModelBase
    {

        #region fields
        readonly XParam _param;
        readonly XParamTypeVM _paramType;

        #endregion

        #region ctors
        public XParamVM()
        {
            _param = new XParam();
            _param.Type = new XParamsType();
            _paramType = new XParamTypeVM();
        }

        public XParamVM(XParamTypeVM paramType) 
        {
            _param = new XParam();
            _param.Type = paramType.ParamType;
            _paramType = paramType;
        }

        public XParamVM(XParam param)
        {
            _param = param;
            _paramType = new XParamTypeVM(param.Type);
        }

        #endregion


        #region properties
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

        #endregion
    }
}
