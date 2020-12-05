#pragma checksum "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/Report/Customers.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b297ed84fdd93a46431dd33513a824eedd9a2af5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(Final_Project.Views.Report.Views_Report_Customers), @"mvc.1.0.view", @"/Views/Report/Customers.cshtml")]
namespace Final_Project.Views.Report
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 2 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/_ViewImports.cshtml"
using Final_Project.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b297ed84fdd93a46431dd33513a824eedd9a2af5", @"/Views/Report/Customers.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b56097e44404581d4391ca47c31624c761a3d4d4", @"/Views/_ViewImports.cshtml")]
    public class Views_Report_Customers : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Final_Project.Models.Ticket>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "FilteredCustomers", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 3 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/Report/Customers.cshtml"
  
    ViewData["Title"] = "Report";

#line default
#line hidden
#nullable disable
            WriteLiteral("<br>\n\n<h1>Customers Report</h1>\n\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b297ed84fdd93a46431dd33513a824eedd9a2af53725", async() => {
                WriteLiteral("\n    <div class=\"form-group\">\n        <label class=\"control-label\">Filter by Customer Username:</label>\n        ");
#nullable restore
#line 13 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/Report/Customers.cshtml"
   Write(Html.DropDownList("SelectedCustomer", (SelectList)ViewBag.AllCustomers, new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
                WriteLiteral("\n    </div>\n    <div class=\"form-group\">\n        <input type=\"submit\" value=\"Filter\" class=\"btn btn-primary\" />\n    </div>\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n\n<h4>Number of Seats Sold to Customer</h4>\n<p>");
#nullable restore
#line 21 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/Report/Customers.cshtml"
Write(ViewBag.TotalSeats);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\n<h4>Total Revenue from Customer</h4>\n<p>");
#nullable restore
#line 23 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/Report/Customers.cshtml"
Write(ViewBag.TotalRevenue);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\n<br />\n\n\n<table class=\"table\">\n    <thead>\n        <tr>\n            <th>\n                ");
#nullable restore
#line 31 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/Report/Customers.cshtml"
           Write(Html.DisplayNameFor(model => model.MovieOrder.ConfirmationNumber));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </th>\n            <th>\n                ");
#nullable restore
#line 34 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/Report/Customers.cshtml"
           Write(Html.DisplayNameFor(model => model.MovieOrder.DateOrdered));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </th>\n            <th>\n                Entire Order Subtotal\n            </th>\n            <th>\n                Movie Showing\n            </th>\n            <th>\n                ");
#nullable restore
#line 43 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/Report/Customers.cshtml"
           Write(Html.DisplayNameFor(model => model.Seat));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </th>\n            <th>\n                ");
#nullable restore
#line 46 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/Report/Customers.cshtml"
           Write(Html.DisplayNameFor(model => model.TicketPrice));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </th>\n            <th>\n                ");
#nullable restore
#line 49 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/Report/Customers.cshtml"
           Write(Html.DisplayNameFor(model => model.MovieOrder.UsingPopcornPoints));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </th>\n            <th>\n                ");
#nullable restore
#line 52 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/Report/Customers.cshtml"
           Write(Html.DisplayNameFor(model => model.MovieOrder.OrderStatus));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </th>\n            <th>\n                ");
#nullable restore
#line 55 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/Report/Customers.cshtml"
           Write(Html.DisplayNameFor(model => model.MovieOrder.Customer.UserName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </th>\n            <th>\n                Poporn Points For This Ticket\n            </th>\n            <th>\n                Entire Order Popcorn Points Paid\n            </th>\n        </tr>\n    </thead>\n    <tbody>\n");
#nullable restore
#line 66 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/Report/Customers.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\n                <td>\n                    ");
#nullable restore
#line 70 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/Report/Customers.cshtml"
               Write(Html.DisplayFor(modelItem => item.MovieOrder.ConfirmationNumber));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n                <td>\n                    ");
#nullable restore
#line 73 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/Report/Customers.cshtml"
               Write(Html.DisplayFor(modelItem => item.MovieOrder.DateOrdered));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n                <td>\n                    ");
#nullable restore
#line 76 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/Report/Customers.cshtml"
               Write(Html.DisplayFor(modelItem => item.MovieOrder.SubTotal));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n                <td>\n                    ");
#nullable restore
#line 79 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/Report/Customers.cshtml"
               Write(Html.DisplayFor(modelItem => item.MovieShowing.FullShowingName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n                <td>\n                    ");
#nullable restore
#line 82 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/Report/Customers.cshtml"
               Write(Html.DisplayFor(modelItem => item.Seat));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n                <td>\n                    ");
#nullable restore
#line 85 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/Report/Customers.cshtml"
               Write(Html.DisplayFor(modelItem => item.TicketPrice));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n                <td>\n                    ");
#nullable restore
#line 88 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/Report/Customers.cshtml"
               Write(Html.DisplayFor(modelItem => item.MovieOrder.UsingPopcornPoints));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n                <td>\n                    ");
#nullable restore
#line 91 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/Report/Customers.cshtml"
               Write(Html.DisplayFor(modelItem => item.MovieOrder.OrderStatus));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n                <td>\n                    ");
#nullable restore
#line 94 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/Report/Customers.cshtml"
               Write(Html.DisplayFor(modelItem => item.MovieOrder.Customer.UserName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n");
#nullable restore
#line 96 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/Report/Customers.cshtml"
                 if (item.MovieOrder.UsingPopcornPoints == true)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <td>\n                        100\n                    </td>\n");
#nullable restore
#line 101 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/Report/Customers.cshtml"
                }
                else
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <td>\n                        0\n                    </td>\n");
#nullable restore
#line 107 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/Report/Customers.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                <td>\n                    ");
#nullable restore
#line 109 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/Report/Customers.cshtml"
               Write(Html.DisplayFor(modelItem => item.MovieOrder.PopcornPointsPaid));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n            </tr>\n");
#nullable restore
#line 112 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/Report/Customers.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\n</table>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Final_Project.Models.Ticket>> Html { get; private set; }
    }
}
#pragma warning restore 1591
