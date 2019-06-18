﻿using System;
using System.Net;
using System.Threading;
using B2BCoreApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Model.Model;
using Serilog;

namespace B2BCoreApi.Attributes
{
    public class EntityEditFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            var appUser = actionContext.HttpContext.Items["AppUser"] as ApplicationUser;
            //bool tryGetValue = actionContext.ActionArguments.TryGetValue("AppUser", out appUser);
            object data = actionContext.ActionArguments["model"];
            bool tryGetValue = appUser != null;
            // Log.Logger.Debug("Edit request: Data {@Data}", data);
            if (tryGetValue && data != null)
            {
                var user = appUser as ApplicationUser;
                string username = user.UserName;

                var isEntity = data is Entity;
                Entity entity = data as Entity;
                if (isEntity)
                {
                    entity.Modified = DateTime.Now;
                    entity.ModifiedBy = username;
                }

                var isShopChild = data is ShopChild;
                if (isShopChild)
                {
                    var shopChild = data as ShopChild;
                    shopChild.ShopId = user.ShopId;
                }

                ClientRequestModel client =
                    new ClientRequestModel(actionContext) { UserName = username, ShopId = user.ShopId };
                string clientConnectionId = client.ConnectionId;
                Thread.SetData(Thread.GetNamedDataSlot("ConnectionId"), clientConnectionId);
                appUser.ConnectionId = clientConnectionId;
                // Log.Logger.Debug("AppUser : {@AppUser}", appUser);
                Log.Logger.Debug("Client details: {@Client} ", client);

                dynamic controller = actionContext.Controller;
                controller.AppUser = appUser;
            }
            else
            {
                actionContext.Result = new BadRequestResult();
            }
        }
    }
}