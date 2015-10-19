﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDoctor.Model
{
    internal class DAL
    {
        readonly XDBContext _dbContext;

        IEnumerable<XClient> _clientCollection;

        public DAL()
        {
            _dbContext = new XDBContext();

           // _clientCollection = new Collection<XClient>();
            //загрузить сразу все навигац. свойства клиента:
            var v = _dbContext.Clients
                                .Include("ExamsCollection.ParamsCollection")
                                .Include("ExamsCollection.ExamType")
                                .Include("ExamsCollection.ExamType.ParamsCollection");

            _clientCollection = v;
        }


        public XDBContext DbContext
        {
            get
            {
                return _dbContext;
            }
        }

        public IEnumerable<XClient> ClientCollection
        {
            get
            {
                return _clientCollection;
            }
        }


        public void AddExam(XExam exam)
        {
            
        }
    }
}