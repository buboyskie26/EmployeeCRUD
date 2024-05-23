using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace SDS.App_Start
{
    using SDS.IRepository;
    using SDS.Repository;
    using System.Data.Entity.Core.Metadata.Edm;
    using System.Web.Mvc;
    using Unity;
    using Unity.AspNet.Mvc;
    using Unity.Mvc5;

    public class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            container.RegisterType<IEmployee, EmployeeRepository>();
            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));
        }
    }

}