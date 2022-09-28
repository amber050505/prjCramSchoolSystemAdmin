using LinqKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prjCramSchoolSystemAdmin.Models;
using prjCramSchoolSystemAdmin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace prjCramSchoolSystemAdmin.Controllers
{
    [Authorize]
    public class CourseModelController : Controller
    {
        private readonly CramSchoolDBContext _context;
        public CourseModelController(CramSchoolDBContext context)
        {
            _context = context;
        }

        //列表
        public IActionResult List()
        {
            CCourseModelListViewModel CModel = new CCourseModelListViewModel();
            //課程狀態 是否顯示
            CCourseModelShowState c = new CCourseModelShowState();
            int show = c.showCourse("Y");

            //var datas = from t in _context.TCourseModels.Where(c => c.FModleSate == show)
            var datas = from t in _context.TCourseModels.Where(c => c.FModleSate.Equals(show))
                        orderby t.FSaverDate descending
                        select new CCourseModel_List()
                        {
                            FCourseId = t.FCourseId,
                            FName = t.FName,
                            FCategory = t.FCategory.ToString(),//changeCategory_num(t.FCategory)
                            FTotalHours = t.FTotalHours.ToString(),
                            FTotalNumber = t.FTotalNumber.ToString(),
                            FOriginalPrice=t.FOriginalPrice,
                            FSpecialOffer=t.FSpecialOffer,
                            FGrade = t.FGrade,
                            FSchoolYear = t.FSchoolYear.ToString()
                        };
            CModel.course_model = datas.ToList();
            return View(CModel);
        }

        //列表搜尋
        //--執行順序、不能使用方法
        public IActionResult searchList(string txtCategory, string txtSearch)
        {
            #region
            //if (string.IsNullOrEmpty(txtSearch))
            //    data = searchCategory(txtCategory, c);
            //else
            //    data = searchKeyTxt(txtCategory, txtSearch, c);
            //    return Json(data);
            #endregion
            var pred = PredicateBuilder.New<TCourseModel>();
            pred = pred.And(p => p.FModleSate.Equals(new CCourseModelShowState().showCourse("Y")));
            if (txtCategory != "全部")
                pred = pred.And(p => p.FCategory.Equals(changeCategory_num(txtCategory)));

            var pred2 = PredicateBuilder.New<TCourseModel>(true);
            if (!string.IsNullOrEmpty(txtSearch))
            {
                pred2 = search_KeyWords(txtSearch, pred2);
            }

            var data = _context.TCourseModels.Where(pred).Where(pred2).OrderByDescending(t=>t.FSaverDate).Select(t => new CCourseModel_List()//.AsExpandable()
            {
                FCourseId = t.FCourseId,
                FName = t.FName,
                FCategory = t.FCategory.ToString(), //changeCategory_name(t.FCategory.ToString()), //,  //asenumerable()changeCategory_num((Convert.ToUInt32( t.FCategory)).ToString()), //
                FTotalHours = t.FTotalHours.ToString(),
                FTotalNumber = t.FTotalNumber.ToString(),
                FOriginalPrice = t.FOriginalPrice,
                FSpecialOffer = t.FSpecialOffer,
                FGrade = t.FGrade,
                FSchoolYear = t.FSchoolYear.ToString()
            });//.ToList();

            //.AsEnumerable().Select(t1=>new CCourseModel_List(){ 
            //FCategory = changeCategory_name(t1.FCategory)
            //});
            //foreach (var item in data)
            //{
            //    var a = item;
            //    item.FCategory = changeCategory_name(item.FCategory);
            //}
            return Json(data);
        }

        public string changeCategory_name(string category)
        {
            if (!string.IsNullOrEmpty(category))
            {
                int p = Array.IndexOf(CourseData.c_number, category);
                return CourseData.c_name[p];
            }
            return "";
        }

        //關鍵字搜尋
        [NonAction]
        private ExpressionStarter<TCourseModel> search_KeyWords(string txtSearch, ExpressionStarter<TCourseModel> pred2)
        {
            txtSearch = txtSearch.Substring(txtSearch.Length - 1, 1) == " " ? txtSearch.Substring(0, txtSearch.Length - 1) : txtSearch;
            string[] search_arr = txtSearch.Split(" ");
            foreach (var item in search_arr)
            {
                if (changeSearch_Category(item).Count != 0)
                {
                    List<int> search_List = changeSearch_Category(item);
                    foreach (var x in search_List)
                        pred2 = pred2.Or(p => p.FCategory.Equals(x));
                }
                if (changeSearch_SchoolYear(item).Count != 0)
                {
                    List<int> search_List = changeSearch_SchoolYear(item);
                    foreach (var y in search_List)
                        pred2 = pred2.Or(p => p.FSchoolYear.Equals(y));
                }
                pred2 = pred2.Or(p => p.FCourseId.Contains(item));
                pred2 = pred2.Or(p => p.FName.Contains(item));
                pred2 = pred2.Or(p => p.FTotalHours.ToString().Contains(item));
                pred2 = pred2.Or(p => p.FTotalNumber.ToString().Contains(item));
                pred2 = pred2.Or(p => p.FGrade.Contains(item));
                pred2 = pred2.Or(p => p.FOriginalPrice.ToString().Contains(item));
                pred2 = pred2.Or(p => p.FSpecialOffer.ToString().Contains(item));
            }
            return pred2;
        }

        //課程類別文字轉為代碼
        [NonAction]
        public int changeCategory_num(string category)
        {
            int p = Array.IndexOf(CourseData.c_name, category);
            return Convert.ToInt32(CourseData.c_number[p]);
        }

        //關鍵字搜尋_課程類別文字轉為代碼
        [NonAction]
        public List<int> changeSearch_Category(string search)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < CourseData.c_name.Length; i++)
            {
                if (CourseData.c_name[i].Contains(search))
                {
                    list.Add(Convert.ToInt32(CourseData.c_number[i]));
                }
            }
            return list;
        }

        //關鍵字搜尋_學期文字轉為代碼
        public List<int> changeSearch_SchoolYear(string search)
        {
            CourseData c = new CourseData();
            List<int> list = new List<int>();
            for (int i = 0; i < c.schoolyear_name.Length; i++)
            {
                if (c.schoolyear_name[i].Contains(search))
                {
                    list.Add(Convert.ToInt32(c.schoolyear_number[i]));
                }
            }
            return list;
        }


        //新增、修改
        //controllername/actionname/?id
        public IActionResult Edit(string id)
        {
            CCourseModelAllViewModel cmodel_all = new CCourseModelAllViewModel();
            //判斷新增或修改(有沒有id)
            if (string.IsNullOrEmpty(id))
                return View(cmodel_all);

            //判斷有沒有課程模板
            TCourseModel c_model = _context.TCourseModels.FirstOrDefault(m => m.FCourseId == id);
            if (c_model == null)
                return View("List");
            cmodel_all.c_model = c_model;

            //判斷有沒有課程細項
            var data = from t in _context.TCourseModelDetails.Where(c => c.FCourseId == id)
                       select t;
            if (data != null)
                cmodel_all.c_model_detail_list = data.ToList();
            return View(cmodel_all);
        }

        [HttpPost]
        public IActionResult Edit(CCourseModelAllViewModel c,string arrDetail)
        {
            c.c_model_detail_list = tidyDetailList(arrDetail);
            string courseID = "", userID = ""; DateTime now;
            readUserData(out userID, out now);

            if (string.IsNullOrEmpty(c.c_model.FCourseId))
                //新增課程模板
                courseID = newCaseModel(c, userID, now);

            else
                //編輯課程模板
                courseID = editCaseModel(c, userID, now);


            saveDetail(courseID, c.c_model_detail_list, userID, now);
            //return RedirectToAction("List");
            //return Json(new { redirectToUrl = Url.Action("List", "CourseModel") });
            return Ok();
        }

        //整理前端傳過來的CourseModelDetail Json字串
        private List<TCourseModelDetail> tidyDetailList(string arrDetail)
        {
            if (arrDetail == "[]")
                return null;
            List<arrDetail> detailList = JsonSerializer.Deserialize<List<arrDetail>>(arrDetail);
            List<TCourseModelDetail> List = new List<TCourseModelDetail>();
            foreach (var item in detailList)
            {
                List.Add(new TCourseModelDetail()
                {
                    FCourseNumber = item.FCourseNumber,
                    FSchedule=item.FSchedule,
                    FScheduleDetail=item.FScheduleDetail,
                    FTeachingMethod=item.FTeachingMethod,
                    FRemark=item.FRemark
                });
            }
            return List;
        }

        //新增課程模板
        [NonAction]
        private string newCaseModel(CCourseModelAllViewModel c, string userID, DateTime now)
        {
            string courseID = getCourseModelID(c.c_model.FCategory.ToString());
            c.c_model.FCourseId = courseID;
            c.c_model.FCreationUser = userID;
            c.c_model.FSaverUser = userID;
            c.c_model.FCreationDate = now;
            c.c_model.FSaverDate = now;
            c.c_model.FModleSate = new CCourseModelShowState().showCourse("Y");

            _context.TCourseModels.Add(c.c_model);
            _context.SaveChanges();
            return courseID;
        }

        //編輯課程模板
        [NonAction]
        private string editCaseModel(CCourseModelAllViewModel c, string userID, DateTime now)
        {
            string courseID = c.c_model.FCourseId;
            TCourseModel course = _context.TCourseModels.FirstOrDefault(m => m.FCourseId == c.c_model.FCourseId);
            course.FCategory = c.c_model.FCategory;
            course.FName = c.c_model.FName;
            course.FTotalHours = c.c_model.FTotalHours;
            course.FTotalNumber = c.c_model.FTotalNumber;
            course.FGrade = c.c_model.FGrade;
            course.FSchoolYear = c.c_model.FSchoolYear;
            course.FSummary = c.c_model.FSummary;
            course.FModleSate = c.c_model.FModleSate;
            //course.FTeachingMaterial = c.c_model.FTeachingMaterial;
            course.FOriginalPrice = c.c_model.FOriginalPrice;
            course.FSpecialOffer = c.c_model.FSpecialOffer;

            course.FCreationUser = userID;
            course.FSaverUser = userID;
            course.FCreationDate = now;
            course.FSaverDate = now;
            _context.SaveChanges();
            return courseID;
        }

        //儲存課程細項
        [NonAction]
        public void saveDetail(string courseID, List<TCourseModelDetail> detail_List, string userID, DateTime now)
        {
            clearModelDetail(courseID);
            if (detail_List != null && detail_List.Count > 0)
            {
                foreach (var item in detail_List)
                {
                    item.FCourseId = courseID;
                    item.FCreationUser = userID;
                    item.FCreationDate = now;
                    item.FSaverUser = userID;
                    item.FSaverDate = now;
                }

                _context.TCourseModelDetails.AddRange(detail_List);
                _context.SaveChanges();
            }
        }

        //new案件編號
        [NonAction]
        public string getCourseModelID(string category)
        {
            string now = DateTime.Now.ToString("yyyy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") 
                + DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss");
            Random r = new Random();
            return category.getCourseEName() + now + r.Next(0, 9);
        }

        //取出userID、現在時間
        [NonAction]
        public void readUserData(out string userID,out DateTime now)
        {
            userID = "";
            now = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LONGUNED_ID))
            {
                //讀Session-userID
                string json = HttpContext.Session.GetString(CDictionary.SK_LONGUNED_ID);
                userID = JsonSerializer.Deserialize<string>(json);
            }
        }

        //清除Detail舊資料
        [NonAction]
        private void clearModelDetail(string fCourseId)
        {
            var data = from t in _context.TCourseModelDetails.Where(c => c.FCourseId == fCourseId)
                       select t;
            if (data.ToList().Count == 0)
                return;
            _context.TCourseModelDetails.RemoveRange(data);
            _context.SaveChanges();
        }

        //刪除課程模板
        public IActionResult Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                TCourseModel modle = _context.TCourseModels.FirstOrDefault(m => m.FCourseId == id);
                if (modle != null)
                {
                    modle.FModleSate = new CCourseModelShowState().showCourse("N");
                    _context.SaveChanges();
                }
            }
            return Ok();
        }

        //目前沒使用
        //刪除課程模板
        public IActionResult Delete1(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("List");
            TCourseModel modle = _context.TCourseModels.FirstOrDefault(m => m.FCourseId == id);
            if (modle != null)
            {
                _context.Remove(modle);
                _context.SaveChanges();
            }
            return RedirectToAction("List");
        }

        //目前沒使用到
        //整理拿到的json成detail_list
        [NonAction]
        private List<TCourseModelDetail> tidyDetail(string courseID, string arrDetail)
        {
            //移除_Count屬性
            while (arrDetail.Contains("_Count"))
            {
                int p_start = arrDetail.IndexOf("_");
                int p_end = arrDetail.IndexOf(",", p_start);
                arrDetail = arrDetail.Remove(p_start - 1, p_end - (p_start - 1) + 1);
            }

            //DateTime now = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            //string userID = readUserID();
            string userID = "";
            DateTime now;
            readUserData(out userID, out now);

            List<TCourseModelDetail> list = JsonSerializer.Deserialize<List<TCourseModelDetail>>(arrDetail);
            foreach (var item in list)
            {
                item.FCourseId = courseID;
                item.FCreationUser = userID;
                item.FCreationDate = now;
                item.FSaverUser = userID;
                item.FSaverDate = now;
            }
            return list;
        }

        //目前沒使用到
        //儲存課程細項
        public IActionResult saveDetail(string courseID, string suject, string arrDetail)
        {

            //新增到主表

            //新增到Detials

            //new CourseModel 建立新資料
            if (string.IsNullOrEmpty(courseID))
            {
                courseID = getCourseModelID(suject);
                TCourseModel model = new TCourseModel();
                model.FCourseId = courseID;
                _context.TCourseModels.Add(model);
                _context.SaveChanges();
            }
            //子項目是否有資料
            if (string.IsNullOrEmpty(arrDetail))
                return Content(courseID);

            clearModelDetail(courseID);
            List<TCourseModelDetail> detail_List = tidyDetail(courseID, arrDetail);
            _context.TCourseModelDetails.AddRange(detail_List);
            _context.SaveChanges();
            return Content(courseID);
        }

        #region
        //新增、修改
        //public IActionResult Detail(string id)
        //{
        //    CCourseModelAllViewModel cmodel_all = new CCourseModelAllViewModel();
        //    //判斷新增或修改(有沒有id)
        //    if (string.IsNullOrEmpty(id))
        //        return View(cmodel_all);

        //    //判斷有沒有課程模板
        //    TCourseModel c_model = _context.TCourseModels.FirstOrDefault(m => m.FCourseId == id);
        //    if (c_model == null)
        //        return View("List");
        //    cmodel_all.c_model = c_model;

        //    //判斷有沒有課程細項
        //    var data = from t in _context.TCourseModelDetails.Where(c => c.FCourseId == id)
        //               select t;
        //    if (data != null)
        //        cmodel_all.c_model_detail_list = data.ToList();
        //    return View(cmodel_all);
        //}

        //[HttpPost]
        //public IActionResult Detail(CCourseModelAllViewModel c)
        //{
        //    string courseID = "", userID = ""; DateTime now;
        //    readUserData(out userID, out now);

        //    if (string.IsNullOrEmpty(c.c_model.FCourseId))
        //        //新增課程模板
        //        courseID = newCaseModel(c, userID, now);

        //    else
        //        //編輯課程模板
        //        courseID = editCaseModel(c, userID, now);

        //    if (c.c_model_detail_list != null && c.c_model_detail_list.Count > 0)
        //        saveDetail(courseID, c.c_model_detail_list, userID, now);
        //    return RedirectToAction("List");
        //}

        ////課程類別搜尋
        //[NonAction]
        //private IQueryable<CCourseModel_Search> searchCategory(string txtCategory, CCourseModelShowState c)
        //{
        //    return from t in _context.TCourseModels
        //           where t.FCategory.Equals(changeCategory_num(txtCategory)) && t.FModleSate.Equals(c.showCourse("Y"))
        //           select new CCourseModel_Search()
        //           {
        //               FCourseId = t.FCourseId,
        //               FName = t.FName,
        //               FCategory = changeCategory_name(t.FCategory),
        //               FTotalHours = t.FTotalHours.ToString(),
        //               FTotalNumber = t.FTotalNumber.ToString(),
        //               FGrade = t.FGrade,
        //               FSchoolYear = changeSchoolYear((int)t.FSchoolYear)
        //           };
        //}
        ////課程關鍵字搜尋
        //[NonAction]
        //private IQueryable<CCourseModel_Search> searchKeyTxt(string txtCategory, string txtSearch, CCourseModelShowState c)
        //{
        //    return from t in _context.TCourseModels
        //           where (t.FCategory.Equals(changeCategory_num(txtCategory))
        //           && t.FModleSate.Equals(c.showCourse("Y")) && (t.FCourseId.Contains(txtSearch) || t.FName.Contains(txtSearch)
        //           || t.FTotalHours.ToString().Contains(txtSearch) || t.FTotalNumber.ToString().Contains(txtSearch)
        //           || t.FGrade.Contains(txtSearch) || t.FOriginalPrice.ToString().Contains(txtSearch)
        //           || t.FSpecialOffer.ToString().Contains(txtSearch)))
        //           select new CCourseModel_Search()
        //           {
        //               FCourseId = t.FCourseId,
        //               FName = t.FName,
        //               FCategory = changeCategory_name(t.FCategory),
        //               FTotalHours = t.FTotalHours.ToString(),
        //               FTotalNumber = t.FTotalNumber.ToString(),
        //               FGrade = t.FGrade,
        //               FSchoolYear = changeSchoolYear((int)t.FSchoolYear)
        //           };
        //}

        #endregion

        public IActionResult Index(CCourseModelAllViewModel cmodel_all)
        {
            return View();
        }
    }
}
