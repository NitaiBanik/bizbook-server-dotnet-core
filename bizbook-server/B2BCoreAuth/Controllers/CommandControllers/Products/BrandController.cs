﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model;
using Model.Model.Products;
using RequestModel.Shops;
using ServiceLibrary.Shops;
using ViewModel.Shops;

namespace B2BCoreApi.Controllers.CommandControllers.Products
{
    [Route("api/Brand")]
    public class BrandController : BaseCommandController<Brand, BrandRequestModel, BrandViewModel>
    {

        public BrandController(BizBookInventoryContext db, ILogger<BrandController> logger) : base(
           new BrandService(new BaseRepository<Brand>(db)), logger)
        {

        }
    }
}
