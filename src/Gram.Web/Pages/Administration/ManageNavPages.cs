using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gram.Web.Pages.Administration
{
    public static class ManageNavPages
    {
        public static string Employees => "Employees";
        public static string Guides => "Guides";
        public static string Partners => "Partner";

        public static string EmployeesNavClass(ViewContext viewContext) => PageNavClass(viewContext, Employees);
        public static string GuidesNavClass(ViewContext viewContext) => PageNavClass(viewContext, Guides);
        public static string PartnersNavClass(ViewContext viewContext) => PageNavClass(viewContext, Partners);

        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}