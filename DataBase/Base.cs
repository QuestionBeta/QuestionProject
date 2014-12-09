using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataBase.AppData;

namespace DataBase
{
    public class Base
    {
        protected XFXDataContext datacontext = null;
        public Base()
        {
            datacontext = new XFXDataContext();            
        }
    }
}
