#pragma checksum "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/MovieOrders/Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "448c9b7f5439407d2bcf65a897963e9ad6d68a84"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(Final_Project.Views.MovieOrders.Views_MovieOrders_Details), @"mvc.1.0.view", @"/Views/MovieOrders/Details.cshtml")]
namespace Final_Project.Views.MovieOrders
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"448c9b7f5439407d2bcf65a897963e9ad6d68a84", @"/Views/MovieOrders/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b56097e44404581d4391ca47c31624c761a3d4d4", @"/Views/_ViewImports.cshtml")]
    public class Views_MovieOrders_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Final_Project.Models.MovieOrder>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 3 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/MovieOrders/Details.cshtml"
  
    ViewData["Title"] = "Details";

#line default
#line hidden
#nullable disable
            WriteLiteral("<br>\n\n<h1>Details</h1>\n\n<div>\n    <h4>MovieOrder</h4>\n    <hr />\n    <dl class=\"row\">\n        <dt class=\"col-sm-2\">\n            ");
#nullable restore
#line 15 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/MovieOrders/Details.cshtml"
       Write(Html.DisplayNameFor(model => model.OrderStatus));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        </dt>\n        <dd class=\"col-sm-10\">\n            ");
#nullable restore
#line 18 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/MovieOrders/Details.cshtml"
       Write(Html.DisplayFor(model => model.OrderStatus));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        </dd>\n        <dt class=\"col-sm-2\">\n            ");
#nullable restore
#line 21 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/MovieOrders/Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Customer.Email));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        </dt>\n        <dd class=\"col-sm-10\">\n            ");
#nullable restore
#line 24 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/MovieOrders/Details.cshtml"
       Write(Html.DisplayFor(model => model.Customer.Email));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        </dd>\n        <dt class=\"col-sm-2\">\n            ");
#nullable restore
#line 27 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/MovieOrders/Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Customer.FullName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        </dt>\n        <dd class=\"col-sm-10\">\n            ");
#nullable restore
#line 30 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/MovieOrders/Details.cshtml"
       Write(Html.DisplayFor(model => model.Customer.FullName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        </dd>\n        <dt class=\"col-sm-2\">\n            ");
#nullable restore
#line 33 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/MovieOrders/Details.cshtml"
       Write(Html.DisplayNameFor(model => model.ConfirmationNumber));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        </dt>\n        <dd class=\"col-sm-10\">\n            ");
#nullable restore
#line 36 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/MovieOrders/Details.cshtml"
       Write(Html.DisplayFor(model => model.ConfirmationNumber));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        </dd>\n        <dt class=\"col-sm-2\">\n            ");
#nullable restore
#line 39 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/MovieOrders/Details.cshtml"
       Write(Html.DisplayNameFor(model => model.isConfirmed));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        </dt>\n        <dd class=\"col-sm-10\">\n            ");
#nullable restore
#line 42 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/MovieOrders/Details.cshtml"
       Write(Html.DisplayFor(model => model.isConfirmed));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        </dd>\n        <dt class=\"col-sm-2\">\n            ");
#nullable restore
#line 45 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/MovieOrders/Details.cshtml"
       Write(Html.DisplayNameFor(model => model.isGift));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        </dt>\n        <dd class=\"col-sm-10\">\n            ");
#nullable restore
#line 48 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/MovieOrders/Details.cshtml"
       Write(Html.DisplayFor(model => model.isGift));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        </dd>\n");
#nullable restore
#line 50 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/MovieOrders/Details.cshtml"
         if (Model.isGift == true)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <dt class=\"col-sm-2\">\n                ");
#nullable restore
#line 53 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/MovieOrders/Details.cshtml"
           Write(Html.DisplayNameFor(model => model.Recipient));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </dt>\n            <dd class=\"col-sm-10\">\n                ");
#nullable restore
#line 56 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/MovieOrders/Details.cshtml"
           Write(Html.DisplayFor(model => model.Recipient.FullName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </dd>\n");
#nullable restore
#line 58 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/MovieOrders/Details.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("        <dt class=\"col-sm-2\">\n            ");
#nullable restore
#line 60 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/MovieOrders/Details.cshtml"
       Write(Html.DisplayNameFor(model => model.DateOrdered));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        </dt>\n        <dd class=\"col-sm-10\">\n            ");
#nullable restore
#line 63 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/MovieOrders/Details.cshtml"
       Write(Html.DisplayFor(model => model.DateOrdered));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
        </dd>

    </dl>
</div>
<h5>Movie Tickets on this Order</h5>
<table class=""table table-primary"">
    <tr>
        <th>Title</th>
        <th>Show Time</th>
        <th>Seat</th>
        <th>Price</th>
        <th>Type of Price</th>
        <th>Senior Discount</th>
    </tr>
");
#nullable restore
#line 78 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/MovieOrders/Details.cshtml"
     foreach (Ticket rd in Model.Tickets)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\n            <td>");
#nullable restore
#line 81 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/MovieOrders/Details.cshtml"
           Write(Html.DisplayFor(ModelItem => rd.MovieShowing.Movie.Title));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n            <td>");
#nullable restore
#line 82 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/MovieOrders/Details.cshtml"
           Write(Html.DisplayFor(ModelItem => rd.MovieShowing.StartTime));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n            <td>");
#nullable restore
#line 83 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/MovieOrders/Details.cshtml"
           Write(Html.DisplayFor(ModelItem => rd.Seat));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n            <td>");
#nullable restore
#line 84 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/MovieOrders/Details.cshtml"
           Write(Html.DisplayFor(ModelItem => rd.TicketPrice));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n            <td>");
#nullable restore
#line 85 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/MovieOrders/Details.cshtml"
           Write(Html.DisplayFor(ModelItem => rd.Discount));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n            <td>");
#nullable restore
#line 86 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/MovieOrders/Details.cshtml"
           Write(Html.DisplayFor(ModelItem => rd.SeniorDiscount));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n        </tr>\n");
#nullable restore
#line 88 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/MovieOrders/Details.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</table>\n\n<h5>Order Summary</h5>\n<table class=\"table table-sm table-bordered\" style=\"width:30%\">\n    <tr>\n        <th colspan=\"2\" style=\"text-align:center\">Order Summary</th>\n    </tr>\n    <tr>\n        <td>Order Subtotal:</td>\n        <td>");
#nullable restore
#line 98 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/MovieOrders/Details.cshtml"
       Write(Html.DisplayFor(model => model.SubTotal));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n    </tr>\n    <tr>\n        <td>Tax:</td>\n        <td>");
#nullable restore
#line 102 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/MovieOrders/Details.cshtml"
       Write(Html.DisplayFor(model => model.Tax));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n    </tr>\n    <tr>\n        <td>Senior Discount:</td>\n        <td>");
#nullable restore
#line 106 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/MovieOrders/Details.cshtml"
       Write(Html.DisplayFor(model => model.OrderDiscount));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n    </tr>\n    <tr>\n        <td>Using Popcorn Points:</td>\n        <td>");
#nullable restore
#line 110 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/MovieOrders/Details.cshtml"
       Write(Html.DisplayFor(model => model.UsingPopcornPoints));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n    </tr>\n");
#nullable restore
#line 112 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/MovieOrders/Details.cshtml"
     if (Model.UsingPopcornPoints == true)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\n            <td>Popcorn Points Paid:</td>\n            <td>");
#nullable restore
#line 116 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/MovieOrders/Details.cshtml"
           Write(Html.DisplayFor(model => model.PopcornPointsCost));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n        </tr>\n");
#nullable restore
#line 118 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/MovieOrders/Details.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("    <tr>\n        <td>Order Total:</td>\n        <td>");
#nullable restore
#line 121 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/MovieOrders/Details.cshtml"
       Write(Html.DisplayFor(model => model.Total));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n    </tr>\n\n</table>\n<div>\n");
#nullable restore
#line 126 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/MovieOrders/Details.cshtml"
     if (Model.isConfirmed == false || Model.OrderStatus == Status.Unfinished)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "448c9b7f5439407d2bcf65a897963e9ad6d68a8415707", async() => {
                WriteLiteral("Edit");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 128 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/MovieOrders/Details.cshtml"
                               WriteLiteral(Model.MovieOrderID);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n        <span>|</span>\n");
#nullable restore
#line 130 "/Users/michelle/git/mis333k/fa20team19finalproject/Final_Project/Final_Project/Views/MovieOrders/Details.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "448c9b7f5439407d2bcf65a897963e9ad6d68a8418110", async() => {
                WriteLiteral("Back to List");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n</div>\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Final_Project.Models.MovieOrder> Html { get; private set; }
    }
}
#pragma warning restore 1591
