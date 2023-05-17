﻿using Common.Models;

namespace WebUI.Users
{
    public class DefaultUserProvider : IUserProvider
    {
        public Guid GetCustomerId(HttpContext httpContext)
        {
            Guid.TryParse(httpContext.Session.GetString("CustomerId"), out var customerId);
            return customerId;
        }

        public void SetCustomer(HttpContext httpContext, Customer customer)
        {
            httpContext.Session.SetString("CustomerId", customer.Id.ToString());
            httpContext.Session.SetString("CustomerName", customer.Name);
        }
    }
}