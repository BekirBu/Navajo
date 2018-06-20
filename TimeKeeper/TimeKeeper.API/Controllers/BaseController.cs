using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TimeKeeper.API.Models;
using TimeKeeper.DAL.Repositories;

namespace TimeKeeper.API.Controllers
{
    public class BaseController : ApiController
    {
        UnitOfWork unit;        // = new UnitOfWork();
        ModelFactory factory;   //= new ModelFactory();

        public UnitOfWork TimeUnit {
            get
            { 
                if(unit == null)
                    unit = new UnitOfWork();
                return unit;
            }
        }
        public ModelFactory TimeFactory {
            get
            {
                if (factory == null)
                    factory = new ModelFactory();
                return factory;
            }
        }


    }
}
