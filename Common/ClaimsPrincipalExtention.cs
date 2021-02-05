using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Common
{
    public static class ClaimsPrincipalExtention
    {
        public static string GetMyId(this ClaimsPrincipal value)
        {
			try
			{
                var userId = value != null
                    && value.Identity.IsAuthenticated
                    ? value.Claims?
                    .Where(w => w.Type == ClaimTypes.NameIdentifier)
                    .FirstOrDefault()?.Value
                    : "";
                return userId;
            }
            catch (Exception Ex)
			{
                return "";
			}
        }

        public static string GetMyUserName(this ClaimsPrincipal value)
        {
			try
			{
                var userId = value != null
                    && value.Identity.IsAuthenticated
                    ? value.Claims?
                    .Where(w => w.Type == ClaimTypes.Name)
                    .FirstOrDefault()?.Value
                    : "";
                return userId;
            }
            catch (Exception Ex)
			{
                return "";
			}
        }
    }
}
