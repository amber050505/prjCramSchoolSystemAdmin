#pragma checksum "D:\prjadmin\prjCramSchoolSystemAdmin\Views\CourseModel\List.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "80a2ef795e79fbccd88524e92cb911883400a80d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_CourseModel_List), @"mvc.1.0.view", @"/Views/CourseModel/List.cshtml")]
namespace AspNetCore
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
#line 1 "D:\prjadmin\prjCramSchoolSystemAdmin\Views\_ViewImports.cshtml"
using prjCramSchoolSystemAdmin;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\prjadmin\prjCramSchoolSystemAdmin\Views\_ViewImports.cshtml"
using prjCramSchoolSystemAdmin.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"80a2ef795e79fbccd88524e92cb911883400a80d", @"/Views/CourseModel/List.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ac9533fc0f84af824e43c58752e00365d13455e9", @"/Views/_ViewImports.cshtml")]
    public class Views_CourseModel_List : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<prjCramSchoolSystemAdmin.ViewModel.CCourseModelListViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "全部", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("action", new global::Microsoft.AspNetCore.Html.HtmlString("#"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("margin-bottom:5px;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn_edit"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("target", new global::Microsoft.AspNetCore.Html.HtmlString("_blank"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 4 "D:\prjadmin\prjCramSchoolSystemAdmin\Views\CourseModel\List.cshtml"
  
    ViewData["Title"] = "課程模板";

#line default
#line hidden
#nullable disable
            DefineSection("Styles", async() => {
                WriteLiteral(@"
    <style>
        th {
            white-space: nowrap;
        }
        .list_distance {
            padding-top: 50px;
            padding-bottom: 50px;
        }
        .btn_edit {
            /*border: 2px solid #b7b7b7;
            background: transparent;
            color: #111111;*/
            /*border: 2px solid #b7b7b7;*/
            display: inline-block;
            font-size: 14px;
            font-weight: 600;
            text-transform: uppercase;
            padding: 14px 30px;
            color: #111111;
            background: transparent;
            letter-spacing: 2px;
        }
        /*.btn_edit{
            border: 1px solid black;
        }

        .btn_edit{
            background-color: #F2F2F2;
        }*/
    </style>
");
            }
            );
            WriteLiteral(@"<div class=""breadcrumb-option"">
    <div class=""container"">
        <div class=""row"">
            <div class=""col-lg-6 col-md-6 col-sm-6"">
                <div class=""breadcrumb__text"">
                    <h2>課程模板</h2>
                </div>
            </div>
            <div class=""col-lg-6 col-md-6 col-sm-6"">
                <div class=""breadcrumb__links"">
                    <a");
            BeginWriteAttribute("href", " href=\"", 1478, "\"", 1503, 1);
#nullable restore
#line 54 "D:\prjadmin\prjCramSchoolSystemAdmin\Views\CourseModel\List.cshtml"
WriteAttributeValue("", 1485, Url.Content("~/"), 1485, 18, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@">首頁</a>
                    <span>課程模板</span>
                </div>
            </div>
        </div>
    </div>
</div>
<!-- Wishlist Section Begin -->
<section class=""wishlist list_distance"">
    <div class=""container"">
        <div class=""row"" style=""padding-bottom:60px;"">
            <div class=""col-lg-7 col-md-7"">
                <div class=""shop__option__search"">
                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "80a2ef795e79fbccd88524e92cb911883400a80d7833", async() => {
                WriteLiteral("\r\n                        <select id=\"CategoryDDL\">\r\n                            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "80a2ef795e79fbccd88524e92cb911883400a80d8174", async() => {
                    WriteLiteral("全部");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
#nullable restore
#line 70 "D:\prjadmin\prjCramSchoolSystemAdmin\Views\CourseModel\List.cshtml"
                              
                                if (Model.CategoryDDL.Length > 0)
                                {

                                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 74 "D:\prjadmin\prjCramSchoolSystemAdmin\Views\CourseModel\List.cshtml"
                                     foreach (var item in Model.CategoryDDL)
                                    {

#line default
#line hidden
#nullable disable
                WriteLiteral("                                        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "80a2ef795e79fbccd88524e92cb911883400a80d10055", async() => {
#nullable restore
#line 76 "D:\prjadmin\prjCramSchoolSystemAdmin\Views\CourseModel\List.cshtml"
                                                         Write(item);

#line default
#line hidden
#nullable disable
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                BeginWriteTagHelperAttribute();
#nullable restore
#line 76 "D:\prjadmin\prjCramSchoolSystemAdmin\Views\CourseModel\List.cshtml"
                                           WriteLiteral(item);

#line default
#line hidden
#nullable disable
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
#nullable restore
#line 77 "D:\prjadmin\prjCramSchoolSystemAdmin\Views\CourseModel\List.cshtml"
                                    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 77 "D:\prjadmin\prjCramSchoolSystemAdmin\Views\CourseModel\List.cshtml"
                                     

                                }
                            

#line default
#line hidden
#nullable disable
                WriteLiteral("                        </select>\r\n                        <input id=\"txtSearch\" type=\"text\" placeholder=\"搜尋\">\r\n                        <button id=\"btnSearch\" type=\"button\"><i class=\"fa fa-search\"></i></button>\r\n                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                    <small style=""margin-left: 170px; color: #666666"">多關鍵字搜尋請用空白鍵隔開</small>
                </div>
            </div>
            <div class=""col-lg-5 col-md-5 col-sm-5"">
                <div class=""team__btn"" style=""border: none;"">
                    <a");
            BeginWriteAttribute("href", " href=\"", 3021, "\"", 3062, 1);
#nullable restore
#line 90 "D:\prjadmin\prjCramSchoolSystemAdmin\Views\CourseModel\List.cshtml"
WriteAttributeValue("", 3028, Url.Content("~/CourseModel/Edit"), 3028, 34, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" class=""primary-btn"" target=""_blank""><i class=""fa-solid fa-plus""></i>新增課程模板</a>
                </div>
            </div>
        </div>
        <div class=""row"">
            <div class=""col-lg-12"">
                <div class=""wishlist__cart__table"">
                    <table id=""table"">
                        <thead>
                            <tr>
                                <th style=""width:500px;"">
                                    ");
#nullable restore
#line 101 "D:\prjadmin\prjCramSchoolSystemAdmin\Views\CourseModel\List.cshtml"
                               Write(Html.DisplayNameFor(model => model.rowName.FName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </th>\r\n                                <th>\r\n                                    ");
#nullable restore
#line 104 "D:\prjadmin\prjCramSchoolSystemAdmin\Views\CourseModel\List.cshtml"
                               Write(Html.DisplayNameFor(model => model.rowName.FCategory));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </th>\r\n                                <th>\r\n                                    ");
#nullable restore
#line 107 "D:\prjadmin\prjCramSchoolSystemAdmin\Views\CourseModel\List.cshtml"
                               Write(Html.DisplayNameFor(model => model.rowName.FTotalHours));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </th>\r\n                                <th>\r\n                                    ");
#nullable restore
#line 110 "D:\prjadmin\prjCramSchoolSystemAdmin\Views\CourseModel\List.cshtml"
                               Write(Html.DisplayNameFor(model => model.rowName.FTotalNumber));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </th>\r\n                                <th style=\"width:200px;\">\r\n                                    ");
#nullable restore
#line 113 "D:\prjadmin\prjCramSchoolSystemAdmin\Views\CourseModel\List.cshtml"
                               Write(Html.DisplayNameFor(model => model.rowName.FGrade));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </th>\r\n                                <th>\r\n                                    ");
#nullable restore
#line 116 "D:\prjadmin\prjCramSchoolSystemAdmin\Views\CourseModel\List.cshtml"
                               Write(Html.DisplayNameFor(model => model.rowName.FSchoolYear));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </th>\r\n                                <th>\r\n                                    ");
#nullable restore
#line 119 "D:\prjadmin\prjCramSchoolSystemAdmin\Views\CourseModel\List.cshtml"
                               Write(Html.DisplayNameFor(model => model.rowName.FOriginalPrice));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </th>\r\n                                <th>\r\n                                    ");
#nullable restore
#line 122 "D:\prjadmin\prjCramSchoolSystemAdmin\Views\CourseModel\List.cshtml"
                               Write(Html.DisplayNameFor(model => model.rowName.FSpecialOffer));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </th>\r\n                                <th></th>\r\n                            </tr>\r\n                        </thead>\r\n                        <tbody>\r\n");
#nullable restore
#line 128 "D:\prjadmin\prjCramSchoolSystemAdmin\Views\CourseModel\List.cshtml"
                             foreach (var item in Model.course_model)
                            {
                                //decimal originalprice = item.FOriginalPrice == 0 ? 0 : (decimal)item.FOriginalPrice;
                                //decimal specialoffer = item.FSpecialOffer == 0 ? 0 : (decimal)item.FSpecialOffer;

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <tr>\r\n                                    <td style=\"display:none;\">");
#nullable restore
#line 133 "D:\prjadmin\prjCramSchoolSystemAdmin\Views\CourseModel\List.cshtml"
                                                         Write(item.FCourseId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                    <td class=\"cart__price\" style=\"width:500px;\">");
#nullable restore
#line 134 "D:\prjadmin\prjCramSchoolSystemAdmin\Views\CourseModel\List.cshtml"
                                                                            Write(Html.DisplayFor(modelItem => item.FName));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                    <td class=\"cart__price\">");
#nullable restore
#line 135 "D:\prjadmin\prjCramSchoolSystemAdmin\Views\CourseModel\List.cshtml"
                                                       Write(Html.DisplayFor(modelItem => item.FCategory));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                    <td class=\"cart__stock\">");
#nullable restore
#line 136 "D:\prjadmin\prjCramSchoolSystemAdmin\Views\CourseModel\List.cshtml"
                                                       Write(Html.DisplayFor(modelItem => item.FTotalHours));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                    <td class=\"cart__stock\" >");
#nullable restore
#line 137 "D:\prjadmin\prjCramSchoolSystemAdmin\Views\CourseModel\List.cshtml"
                                                        Write(Html.DisplayFor(modelItem => item.FTotalNumber));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                    <td class=\"cart__stock\" style=\"width:200px;\">");
#nullable restore
#line 138 "D:\prjadmin\prjCramSchoolSystemAdmin\Views\CourseModel\List.cshtml"
                                                                            Write(Html.DisplayFor(modelItem => item.FGrade));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                    <td class=\"cart__stock\">");
#nullable restore
#line 139 "D:\prjadmin\prjCramSchoolSystemAdmin\Views\CourseModel\List.cshtml"
                                                       Write(Html.DisplayFor(modelItem => item.FSchoolYear));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n");
            WriteLiteral("                                    <td class=\"cart__stock\">");
#nullable restore
#line 142 "D:\prjadmin\prjCramSchoolSystemAdmin\Views\CourseModel\List.cshtml"
                                                       Write(item.txtOriginalPrice);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                    <td class=\"cart__stock\">");
#nullable restore
#line 143 "D:\prjadmin\prjCramSchoolSystemAdmin\Views\CourseModel\List.cshtml"
                                                       Write(item.txtSpecialOffer);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                    <td class=\"cart__btn\">");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "80a2ef795e79fbccd88524e92cb911883400a80d22087", async() => {
                WriteLiteral("<i class=\"fa-solid fa-pen-to-square\" style=\"padding-right: 0px; font-size: 22px;\"></i>");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 144 "D:\prjadmin\prjCramSchoolSystemAdmin\Views\CourseModel\List.cshtml"
                                                                                                  WriteLiteral(item.FCourseId);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"</td>
                                    <td class=""cart__close""><button type=""button"" class=""btn_edit"" style=""padding-left: 0px; border: 2px blue none;""><i class=""fa-solid fa-trash"" style=""font-size:22px;""></i></button></td>
                                </tr>
");
#nullable restore
#line 147 "D:\prjadmin\prjCramSchoolSystemAdmin\Views\CourseModel\List.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        </tbody>\r\n                    </table>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</section>\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral(@" 
    <script>
        $(""#CategoryDDL"").change(function () {
            //let b = $(""#CategoryDDL"").find(""option: selected"").text();
            //console.log(b);

            Search();
        })
        $(""#btnSearch"").click(function () {
            Search();
        })
        $(""tbody"").on(""click"", "".cart__close"",async function (e) {
            let coursemodel_id = $(e.target).closest(""tr"").find(""td"").eq(0).html();
            console.log(coursemodel_id);
            let response = await fetch(""");
#nullable restore
#line 169 "D:\prjadmin\prjCramSchoolSystemAdmin\Views\CourseModel\List.cshtml"
                                   Write(Url.Content("~/CourseModel/Delete"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@""" + `?id=${coursemodel_id}`)
            Search();
        })
        let Search = async function () {
            let formData = new FormData();
            formData.append(""txtCategory"", $(""#CategoryDDL"").val());
            formData.append(""txtSearch"", $(""#txtSearch"").val());
            let response = await fetch(""");
#nullable restore
#line 176 "D:\prjadmin\prjCramSchoolSystemAdmin\Views\CourseModel\List.cshtml"
                                   Write(Url.Content("~/CourseModel/searchList"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@""", { method: ""POST"", body: formData })
            let data_arr = await response.json();
            console.log(data_arr);
            let docFrag = $(document.createDocumentFragment());
            $.each(data_arr, function (idx, data) {
                let { fCategory, fCourseId, fName, fGrade, txtOriginalPrice, fSchoolYear, txtSpecialOffer, fTotalHours, fTotalNumber } = data;
                let cell_id = $(""<td></td>"").text(fCourseId).attr(""style"", `display:none;`);
                let celll = $(""<td></td>"").addClass(""cart__price"").text(fName).attr(""style"", `width:500px;`);
                let cell2 = $(""<td></td>"").addClass(""cart__price"").text(fCategory);
                let cell3 = $(""<td></td>"").addClass(""cart__stock"").text(fTotalHours);
                let cell4 = $(""<td></td>"").addClass(""cart__stock"").text(fTotalNumber);
                let cell5 = $(""<td></td>"").addClass(""cart__stock"").text(fGrade).attr(""style"", `width:200px;`);
                let cell6 = $(""<td></td>"").addClass(""cart_");
                WriteLiteral(@"_stock"").text(fSchoolYear);
                let cell7 = $(""<td></td>"").addClass(""cart__stock"").text(txtOriginalPrice);
                let cell8 = $(""<td></td>"").addClass(""cart__stock"").text(txtSpecialOffer);
                let editicon = $(""<i></i>"").addClass(""fa-solid fa-pen-to-square"").attr(""style"", `padding-right: 0px; font-size:22px;`);
                let editlink = $(""<a></a>"").addClass(""btn_edit"").attr({ ""href"": """);
#nullable restore
#line 192 "D:\prjadmin\prjCramSchoolSystemAdmin\Views\CourseModel\List.cshtml"
                                                                            Write(Url.Content("~/CourseModel/Edit/"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@""" + `?id=${fCourseId}`, ""target"": ""_blank"" }).append(editicon);
                let cell9 = $(""<td></td>"").addClass(""cart__btn"").append(editlink);
                let icon = $(""<i></i>"").addClass(""fa-solid fa-trash"").attr(""style"", `font-size:22px;`);
                let btn = $(""<button></button>"").addClass(""btn_edit"").attr(""style"", `padding-left: 0px; border: 2px blue none;`).append(icon);
                let cell10 = $(""<td></td>"").addClass(""cart__close"").append(btn)
                let row = $(""<tr></tr>"").append([cell_id, celll, cell2, cell3, cell4, cell5, cell6, cell7, cell8, cell9, cell10]);
                docFrag.append(row);
            })
            $(""#table>tbody"").html(docFrag);
        }
    </script>
");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<prjCramSchoolSystemAdmin.ViewModel.CCourseModelListViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591