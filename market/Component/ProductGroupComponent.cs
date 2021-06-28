using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using market.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace market.Component
{
    public class ProductGroupComponent:ViewComponent
    {
        private IGroupRepositories _groupRepositories;

        public ProductGroupComponent(IGroupRepositories groupRepositories)
        {
            _groupRepositories = groupRepositories;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            return View("/views/Components/ProductGroupComponent.cshtml", _groupRepositories.GetGrouptForShow());
        }
    }
}
