using SpaceDoctor.Model;
using System;

namespace SpaceDoctor.ViewModel
{
    public class XParamTypeVM : XViewModelBase
    {

        readonly XParamsType _paramType;

        public XParamTypeVM ()
        {
            _paramType = new XParamsType();
        }

        public XParamTypeVM(XParamsType paramType)
        {
            _paramType = paramType;
        }

        public XParamsType ParamType
        {
            get
            {
                return _paramType;
            }
        }

        public String Name 
        {
            get
            {
                return ParamType.Name;
            }

            set 
            {
                ParamType.Name = value;
                RaisePropertyChanged("Name");
            }
        }

        public String Measure 
        {
            get 
            {
            return ParamType.Measure;
            }
            set 
            {
                ParamType.Measure = value;
                RaisePropertyChanged("Measure");
            }
        }


        public Boolean SelectToNewExam { get; set; }

    }
}
