using AutoMapper;
using DBContext.Models;
using FuelServices.Api.Helpers;
using FuelServices.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FuelServices.Api.Controllers
{
    [ApiController]
    public class ContentsManagementController : BaseController
    {
        public ContentsManagementController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        [HttpGet]
        public object GetNews()
        {
            try
            {
                var newsItems = db.ContentManagement.Where(x => !x.IsDeleted && x.IsVisible && x.Name == "news").ToList();
                if (newsItems == null)
                {
                    return new Response<bool>(Constants.NOT_FOUND_CODE, false, Constants.NOT_FOUND);
                }

                var result = GetService<IMapper>().Map<List<ContentManagement>, List<NewsItem>>(newsItems);

                return new Response<List<NewsItem>>(Constants.SUCCESS_CODE, result, Constants.SUCCESS);
            }
            catch (Exception e)
            {
                return new Response<bool>(Constants.SOMETHING_WRONG_CODE, false, GetExceptionMessage(e));
            }
        }
    }
}