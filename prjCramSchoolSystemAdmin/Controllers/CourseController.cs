using Imgur.API.Authentication;
using Imgur.API.Endpoints;
using Imgur.API.Models;
using LinqKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using prjCramSchoolSystemAdmin.Models;
using prjCramSchoolSystemAdmin.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace prjCramSchoolSystemAdmin.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private CramSchoolDBContext _context;
        private IWebHostEnvironment _enviroment;
        public CourseController(CramSchoolDBContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _enviroment = environment;
        }

        //列表
        public IActionResult List()
        {
            //顯示未刪除的資料
            CCourseListViewModel c = new CCourseListViewModel();
            int p = Array.IndexOf(new CourseData().classstate_name, "已刪除");
            int delete_num = Convert.ToInt32(new CourseData().classstate_number[p]);
            var data = from t in _context.TCourseInformations.Where(c => c.FClassState != delete_num)
                       orderby t.FSaverDate descending
                       select new CCourse_List()
                       {
                           FEchelonId = t.FEchelonId,
                           FName = t.FName,
                           FCourse_FName = t.FCourse.FName,
                           FStartTime = t.FStartTime,
                           FEndTime = t.FEndTime,
                           FClassState = t.FClassState,
                           FTeacher = t.FTeacher,
                           FDiscountDate = t.FDiscountDate,
                           FOriginalPrice = t.FCourse.FOriginalPrice,
                           FSpecialOffer = t.FCourse.FSpecialOffer
                       };

            //List<TCourseInformation> List = data.ToList();
            c.course = data.ToList();

            //轉換課程狀態放進 c.classstate
            //c.classstate = changeCourseState(List);
            return View(c);
        }

        //列表搜尋
        public IActionResult searchList(string txtSearch)
        {
            var pred1 = PredicateBuilder.New<TCourseInformation>();
            pred1 = pred1.And(p => p.FClassState!=(new CCourseModelShowState().showCourse("N")));

            var pred2 = PredicateBuilder.New<TCourseInformation>(true);
            if (!string.IsNullOrEmpty(txtSearch))                
                pred2 = search_KeyWords(pred2, txtSearch);

            var data = _context.TCourseInformations.Where(pred1).Where(pred2).OrderByDescending(t => t.FSaverDate).Select(t => new CCourse_List()
            {
                FEchelonId = t.FEchelonId,
                FName = t.FName,
                FCourse_FName = t.FCourse.FName,
                FStartTime = t.FStartTime,
                FEndTime = t.FEndTime,
                FClassState = t.FClassState,
                FTeacher = t.FTeacher,
                FDiscountDate = t.FDiscountDate,
                FOriginalPrice=t.FCourse.FOriginalPrice,
                FSpecialOffer=t.FCourse.FSpecialOffer
            });
            return Json(data);
        }

        //關鍵字搜尋
        private ExpressionStarter<TCourseInformation> search_KeyWords(ExpressionStarter<TCourseInformation> pred2, string txtSearch)
        {
            DateTime now = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            txtSearch = txtSearch.Substring(txtSearch.Length - 1, 1) == " " ? txtSearch.Substring(0, txtSearch.Length - 1) : txtSearch;
            string[] search_arr = txtSearch.Split(" ");

            foreach (var item in search_arr)
            {
                if (changeSearch_DataTime(item) != null)
                {
                    DateTime? _DataTime = changeSearch_DataTime(item);
                    pred2 = pred2.Or(p => p.FStartTime.Equals(_DataTime));
                    pred2 = pred2.Or(p => p.FEndTime.Equals(_DataTime));
                }
                if (changeSearch_ClassState(item).Count != 0)
                {
                    List<int> search_List = changeSearch_ClassState(item);
                    foreach (var x in search_List)
                        pred2 = pred2.Or(p => p.FClassState.Equals(x));
                }
                pred2 = pred2.Or(p => p.FEchelonId.Contains(item));
                pred2 = pred2.Or(p => p.FName.Contains(item));
                pred2 = pred2.Or(p => p.FCourse.FName.Contains(item));
                pred2 = pred2.Or(p => p.FTeacher.Contains(item));
                pred2 = pred2.Or(p => p.FDiscountDate > now && p.FCourse.FSpecialOffer.ToString().Contains(item));
                pred2 = pred2.Or(p => p.FDiscountDate < now && p.FCourse.FOriginalPrice.ToString().Contains(item));
            }

            return pred2;
        }
        
        //關鍵字搜尋_時間文字轉為DateTime
        public DateTime? changeSearch_DataTime(string search)
        {
            try
            {
                return Convert.ToDateTime(search);
            }
            catch
            {
                return null;
            }
        }

        //關鍵字搜尋_課程狀態文字轉為代碼
        [NonAction]
        public List<int> changeSearch_ClassState(string search)
        {
            CourseData c = new CourseData();
            List<int> list = new List<int>();
            for (int i = 0; i < c.classstate_name.Length; i++)
            {
                if (c.classstate_name[i].Contains(search))
                {
                    list.Add(Convert.ToInt32(c.classstate_number[i]));
                }
            }
            return list;
        }

        //轉換課程狀態放進 (int->string)
        [NonAction]
        private List<string> changeCourseState(List<TCourseInformation> List)
        {
            int count = List.Count;
            List<string> c_state = null;
            if (count != 0)
            {
                c_state = new List<string>();
                CourseData c_data = new CourseData(); ;
                for (int i = 0; i < count; i++)
                {
                    //string a = List[0].FClassState.ToString();
                    int p = Array.IndexOf(c_data.classstate_number, List[0].FClassState.ToString());
                    c_state.Add(c_data.classstate_name[p]);
                }
            }
            return c_state;
        }

        //新增、修改
        public IActionResult Edit(string id)
        {
            CCourseViewModel c = courseDDL("new");
            //c.courseimg_arr = new TCourseInformationImg[6];
            CCourseShowState cShow = new CCourseShowState();
            //編輯
            if (!string.IsNullOrEmpty(id))
            {
                TCourseInformation course = _context.TCourseInformations.FirstOrDefault(c => c.FEchelonId.Equals(id));
                if (course == null)
                    return RedirectToAction("List");

                c.course = course;
                //多張圖片
                c.coverimg = "";
                //if (!string.IsNullOrEmpty(course.FCoverImg))
                //    c.coverimg = "img" + course.FCoverImg.Substring(course.FCoverImg.IndexOf("_") + 1, 1);
                //string[] photoarr = showCourseImg(c.course.FEchelonId);
                //for (int i = 0; i < c.courseimg_arr.Length; i++)
                //    c.courseimg_arr[i] = new TCourseInformationImg() { FEchelonId = c.course.FEchelonId, FCourseImageName = photoarr[i] };
                c.courseimg = showImg(c.course.FEchelonId);
            }
            else//新增
            {
                //多張圖片
                //for (int i = 0; i < c.courseimg_arr.Length; i++)
                //    c.courseimg_arr[i] = new TCourseInformationImg() { FCourseImageName = "NullImg.jpg" };
                c.courseimg = "NullImg.jpg";
                c.course = new TCourseInformation();
            }
            return View(c);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CCourseViewModel c)
        {
            //var a = ModelState;
            //var b = ModelState.IsValid;

            CCourseShowState cShow = new CCourseShowState();
            string userID = "";
            DateTime now;
            readUserData(out userID, out now);
            c.course.FSaverUser = userID;
            c.course.FSaverDate = now;

            #region
            //if (!string.IsNullOrEmpty(c.course.FEchelonId))//編輯
            //    reviseInformation(c.course); //修改開課資訊
            //else//新增
            //{
            //    c.course.FEchelonId = getCourseID();//取得案件編號
            //    c.course.FCreationUser = userID;
            //    c.course.FCreationDate = now;
            //    _context.TCourseInformations.Add(c.course);
            //    _context.SaveChanges();
            //}
            #endregion
            Task task = null;
            switch (!string.IsNullOrEmpty(c.course.FEchelonId))
            {
                //編輯
                case true:
                    if(c.uploadPhoto!=null)
                        clearCourseImg(c.course.FEchelonId);
                    task = SaveImg(c.course.FEchelonId, c.uploadPhoto);
                    reviseInformation(c.course); //修改開課資訊
                    break;
                //新增
                case false:
                    string course_id = getCourseID();//取得案件編號
                    task = SaveImg(course_id, c.uploadPhoto);
                    c.course.FEchelonId = course_id;
                    c.course.FCreationUser = userID;
                    c.course.FCreationDate = now;
                    _context.TCourseInformations.Add(c.course);
                    _context.SaveChanges();
                    break;
            }

            //原本將圖片存到指定資料夾
            //SavePhoto(c.course.FEchelonId, c.uploadPhoto);
            //串imgurl
            //SaveImg(c.course.FEchelonId, c.uploadPhoto);
            //目前限制上傳一張照片 所以沒使用
            //checkCoverImgName(c.course.FEchelonId, c.coverimg);
            await task;
            return RedirectToAction("List");
        }

        //刪除課程模板
        public IActionResult Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                TCourseInformation course = _context.TCourseInformations.FirstOrDefault(c => c.FEchelonId == id);
                if (course != null)
                {
                    course.FClassState = new CCourseModelShowState().showCourse("N");
                    _context.SaveChanges();
                }
            }
            return Ok();
        }

        //todo
        //目前沒使用
        //檢查課程名稱是否重複
        public IActionResult CheckName(CCourseViewModel model)
        {
            bool check;
            bool v;

            switch (string.IsNullOrEmpty(model.course.FEchelonId))
            {
                case true:
                    //新增
                    v = _context.TCourseInformations.Any(c => c.FName.Equals(model.course.FName));
                    check = !v;
                    break;
                case false:
                    //修改
                    TCourseInformation course = _context.TCourseInformations.FirstOrDefault(c => c.FEchelonId.Equals(model.course.FEchelonId));
                    if (course != null && course.FName == model.course.FName)
                        check = true;
                    else
                    {
                        v = _context.TCourseInformations.Any(c => c.FName.Equals(model.course.FName));
                        check = !v;
                    }
                    break;
            }
            return Json(check);
        }

        //儲存封面圖檔名
        public void checkCoverImgName(string EchelonId,string coverImg)
        {
            string[] img_id = new CourseData().courseimg_id;
            string coverimg_name = "NullImg.jpg";
            int p = Convert.ToInt32(coverImg.Substring(coverImg.Length - 1, 1));
            TCourseInformationImg img = _context.TCourseInformationImgs.FirstOrDefault(c => c.FEchelonId == EchelonId && c.FCourseImageName == EchelonId + $"_{p}.jpg");
            if (img != null)
                coverimg_name = img.FCourseImageName;
            TCourseInformation course = _context.TCourseInformations.FirstOrDefault(t => t.FEchelonId.Equals(EchelonId));
            if (course != null)
            {
                course.FCoverImg = coverimg_name;
                _context.SaveChanges();
            }
        }

        //new案件編號
        [NonAction]
        public string getCourseID()
        {
            string now = DateTime.Now.ToString("yyyy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss");
            Random r = new Random();
            return "CI" + now + r.Next(0, 9);
        }

        //修改開課資訊
        [NonAction]
        private void reviseInformation(TCourseInformation c)
        {
            TCourseInformation course = _context.TCourseInformations.FirstOrDefault(t => t.FEchelonId.Equals(c.FEchelonId));
            if (course != null)
            {
                course.FName = c.FName;
                course.FCourseId = c.FCourseId;
                course.FStartTime = c.FStartTime;
                course.FEndTime = c.FEndTime;
                course.FTime = c.FTime;
                course.FBasicPeople = c.FBasicPeople;
                course.FLimitPeople = c.FLimitPeople;
                course.FClassState = c.FClassState;
                course.FClassRoom = c.FClassRoom;
                course.FTeacher = c.FTeacher;
                course.FDiscountDate = c.FDiscountDate;
                course.FSaverUser = c.FSaverUser;
                course.FSaverDate = c.FSaverDate;
                _context.SaveChanges();
            }
        }

        //顯示圖片
        private string showImg(string fEchelonId)
        {
            string photoarr = "NullImg.jpg";
            TCourseInformationImg data = _context.TCourseInformationImgs.FirstOrDefault(c => c.FEchelonId.Equals(fEchelonId));
            if (data == null)
                return photoarr;
            return data.FCourseImageName;
        }

        //圖片 串接
        private async Task SaveImg(string fEchelondId,IFormFile uploadPhoto)
        {
            await Task.Run(() =>
            {
                if (uploadPhoto == null)
                    return;

                string s = "";
                if (uploadPhoto.Length > 0)
                {
                    using(var ms=new MemoryStream())
                    {
                        uploadPhoto.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        s = Convert.ToBase64String(fileBytes);
                    }
                }

                var apiClient = new ApiClient("f2ffee5392a55b7", "3a4bb8a77c42255c4a33bf0ecb98a6fbd5c5405d");
                var httpClient = new HttpClient();
                var endpoint = new ImageEndpoint(apiClient, httpClient);
                IImage image;
                //using (var fs = new FileStream(photoPath, FileMode.Open))
                //{
                //    image = endpoint.UploadImageAsync(fs).GetAwaiter().GetResult();
                //}
                //var b = image.Link;

                image =endpoint.UploadImageAsync(s).GetAwaiter().GetResult();
                //var b = image.Link;
                TCourseInformationImg saveImg = new TCourseInformationImg() { FEchelonId = fEchelondId, FCourseImageName = image.Link };
                _context.TCourseInformationImgs.Add(saveImg);
                _context.SaveChanges();
            });
        }

        //清除tCourseInformationImg資料
        [NonAction]
        private void clearCourseImg(string fEchelonId)
        {
            var data = from t in _context.TCourseInformationImgs.Where(m => m.FEchelonId == fEchelonId)
                       select t;
            if (data.ToList().Count == 0)
                return;
            _context.TCourseInformationImgs.RemoveRange(data.ToList());
            _context.SaveChanges();
        }

        //下拉(課程模板)
        [NonAction]
        private CCourseViewModel courseDDL(string state)
        {
            //CourseModelDDL 課程模板->顯示(Y)
            CCourseViewModel c = new CCourseViewModel();
            //判斷DDL是否顯示存在的CourseModel
            #region
            //IQueryable<TCourseModel> data = null;
            //switch (state)
            //{
            //    case "new":
            //        data = from t in _context.TCourseModels.Where(c => c.FModleSate.Equals(new CCourseModelShowState().showCourse("Y")))
            //                   select t;
            //        break;
            //    default:
            //        data = from t in _context.TCourseModels
            //               select t;
            //        break;
            //}
            #endregion
            var data = from t in _context.TCourseModels.Where(c => c.FModleSate.Equals(new CCourseModelShowState().showCourse("Y")))
                       select t;
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in data.ToList())
                list.Add(new SelectListItem() { Text = item.FName, Value = item.FCourseId });
            c.CourseDDL = list;
            return c;
        }

        //下拉(課程模板) change get模板資料
        public IActionResult getModelData(string fCourseId)
        {
            CShowModelData c = new CShowModelData();
            TCourseModel m = _context.TCourseModels.FirstOrDefault(c => c.FCourseId.Equals(fCourseId));
            if (m != null)
            {
                if (m.FTotalHours == null)
                    c.FTotalHours = 0;
                else
                    c.FTotalHours = (int)m.FTotalHours;
                if (m.FTotalNumber == null)
                    c.FTotalNumber = 0;
                else
                    c.FTotalNumber = (int)m.FTotalNumber;
                if (string.IsNullOrEmpty(m.FGrade))
                    c.FGrade = "";
                else
                    c.FGrade = m.FGrade;
                if (m.FSchoolYear == null)
                    c.FSchoolYear = "";
                else
                {
                    CourseData c_data = new CourseData();
                    int p = Array.IndexOf(c_data.schoolyear_number, m.FSchoolYear.ToString());
                    c.FSchoolYear = c_data.schoolyear_name[p];
                }
                if (string.IsNullOrEmpty(m.FSummary))
                    c.FSummary = "";
                else
                    c.FSummary = m.FSummary;
                if (m.FOriginalPrice == null)
                    c.FOriginalPrice = 0;
                else
                    c.FOriginalPrice = (decimal)m.FOriginalPrice;
                if (m.FSpecialOffer == null)
                    c.FSpecialOffer = 0;
                else
                    c.FSpecialOffer = (decimal)m.FSpecialOffer;
            }

            c.FDetail_List = getModelDetail(fCourseId);
            return Json(c);
        }

        //下拉(課程模板) change get模板細項
        [NonAction]
        private List<CShowModelDetail> getModelDetail(string fCourseId)
        {
            var data = from t in _context.TCourseModelDetails
                       where t.FCourseId.Equals(fCourseId)
                       select new CShowModelDetail()
                       {
                           FCourseNumber = t.FCourseNumber.ToString(),
                           FSchedule = t.FSchedule,
                           FScheduleDetail = t.FScheduleDetail,
                           FTeachingMethod = t.FTeachingMethod,
                           FRemark = t.FRemark
                       };
            return data.ToList();
        }

        //取出userID、現在時間
        [NonAction]
        public void readUserData(out string userID, out DateTime now)
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

        //目前沒使用
        //多張圖 串接
        private void SaveImg1(string fEchelondId, List<IFormFile> uploadPhoto)
        {
            #region
            //test
            //List<IFormFile> photoarr = c.uploadPhoto;
            //for (int i = 0; i < photoarr.Count; i++)
            //{
            //    string photoName = "CourseImg" + ".jpg";
            //    string photoPath = _enviroment.WebRootPath + @"\images\" + photoName;
            //    using (var fs = new FileStream(photoPath, FileMode.Create))
            //    {
            //        photoarr[i].CopyTo(fs);
            //    }

            //    var apiClient = new ApiClient("f2ffee5392a55b7", "3a4bb8a77c42255c4a33bf0ecb98a6fbd5c5405d");
            //    var httpClient = new HttpClient();
            //    var endpoint = new ImageEndpoint(apiClient, httpClient);
            //    IImage image;
            //    using (var fs = new FileStream(photoPath, FileMode.Open))
            //    {
            //        image = endpoint.UploadImageAsync(fs).GetAwaiter().GetResult();
            //    }
            //    var b = image.Link;
            //}
            //
            #endregion
            if (uploadPhoto == null)
                return;
            if (uploadPhoto.Count == 0 || uploadPhoto == null)
                return;
            clearCourseImg(fEchelondId);
            List<IFormFile> photoarr = uploadPhoto;
            List<TCourseInformationImg> saveImg_List = new List<TCourseInformationImg>();
            for (int i = 0; i < photoarr.Count; i++)
            {
                if (photoarr[i] == null)
                    continue;
                string photoName = "CourseImg" + ".jpg";
                string photoPath = _enviroment.WebRootPath + @"\images\" + photoName;
                using (var fs = new FileStream(photoPath, FileMode.Create))
                {
                    photoarr[i].CopyTo(fs);
                }

                var apiClient = new ApiClient("f2ffee5392a55b7", "3a4bb8a77c42255c4a33bf0ecb98a6fbd5c5405d");
                var httpClient = new HttpClient();
                var endpoint = new ImageEndpoint(apiClient, httpClient);
                IImage image;
                using (var fs = new FileStream(photoPath, FileMode.Open))
                {
                    image = endpoint.UploadImageAsync(fs).GetAwaiter().GetResult();
                }
                //var b = image.Link;
                saveImg_List.Add(new TCourseInformationImg() { FEchelonId = fEchelondId, FCourseImageName = image.Link });
            }
            if (saveImg_List.Count == 0)
                return;
            _context.TCourseInformationImgs.AddRange(saveImg_List);
            _context.SaveChanges();
        }

        //目前沒使用
        //顯示多張圖片
        [NonAction]
        private string[] showCourseImg(string fEchelonId)
        {
            string[] photoarr = new string[6] { "NullImg.jpg", "NullImg.jpg", "NullImg.jpg", "NullImg.jpg", "NullImg.jpg", "NullImg.jpg" };
            var data = from t in _context.TCourseInformationImgs.Where(c => c.FEchelonId.Equals(fEchelonId))
                       select t;
            List<TCourseInformationImg> data_List = data.ToList();

            if (data_List.Count > 0)
            {
                for (int i = 0; i < data_List.Count; i++)
                {
                    string imgName = data_List[i].FCourseImageName;
                    int p = imgName.IndexOf("_");
                    int num = Convert.ToInt32(imgName.Substring(p + 1, 1));
                    photoarr[num - 1] = imgName;
                }
            }
            return photoarr;
        }

        //目前沒有使用
        //儲存tCourseInformationImg圖片資料
        [NonAction]
        private void SavePhoto(string fEchelondId, List<IFormFile> uploadPhoto)
        {
            if (uploadPhoto == null)
                return;
            if (uploadPhoto.Count == 0 || uploadPhoto == null)
                return;
            clearCourseImg(fEchelondId);
            //IFormFile[] photoarr = new IFormFile[6] { c.photo1, c.photo2, c.photo3, c.photo4, c.photo5, c.photo6 };
            List<IFormFile> photoarr = uploadPhoto;
            List<TCourseInformationImg> saveImg_List = new List<TCourseInformationImg>();
            for (int i = 0; i < photoarr.Count; i++)
            {
                if (photoarr[i] == null)
                    continue;
                //string photoName = Guid.NewGuid().ToString() + ".jpg";
                string photoName = $"{fEchelondId}_{i + 1}" + ".jpg";
                photoarr[i].CopyTo(new FileStream(_enviroment.WebRootPath + @"\images\" + photoName, FileMode.Create));
                saveImg_List.Add(new TCourseInformationImg() { FEchelonId = fEchelondId, FCourseImageName = photoName });
            }
            if (saveImg_List.Count == 0)
                return;
            _context.TCourseInformationImgs.AddRange(saveImg_List);
            _context.SaveChanges();
        }

        #region
        //瀏覽
        //public IActionResult Detail(string id)
        //{
        //    CCourseViewModel c = courseDDL("");
        //    c.courseimg_arr = new TCourseInformationImg[6];
        //    CCourseShowState cShow = new CCourseShowState();
        //    //編輯
        //    if (!string.IsNullOrEmpty(id))
        //    {
        //        TCourseInformation course = _context.TCourseInformations.FirstOrDefault(c => c.FEchelonId.Equals(id));
        //        if (course == null)
        //            return RedirectToAction("List");

        //        c.casestatus = cShow.showCourse("old");
        //        c.course = course;

        //        string[] photoarr = showCourseImg(c.course.FEchelonId);
        //        for (int i = 0; i < c.courseimg_arr.Length; i++)
        //            c.courseimg_arr[i] = new TCourseInformationImg() { FEchelonId = c.course.FEchelonId, FCourseImageName = photoarr[i] };
        //    }
        //    else//新增
        //    {
        //        c.casestatus = cShow.showCourse("new");
        //        for (int i = 0; i < c.courseimg_arr.Length; i++)
        //            c.courseimg_arr[i] = new TCourseInformationImg() { FCourseImageName = "NullImg.jpg" };
        //        c.course = new TCourseInformation();
        //    }
        //    return View(c);
        //}

        //[HttpPost]
        //public IActionResult Detail(CCourseViewModel c)
        //{
        //    CCourseShowState cShow = new CCourseShowState();
        //    string userID = "";
        //    DateTime now;
        //    readUserData(out userID, out now);
        //    if (c.casestatus == cShow.showCourse("old"))//編輯
        //    {
        //        TCourseInformation course = _context.TCourseInformations.FirstOrDefault(t => t.FEchelonId.Equals(c.course.FEchelonId));
        //        if (course != null)
        //        {
        //            c.course.FSaverUser = userID;
        //            c.course.FSaverDate = now;
        //            _context.SaveChanges();
        //        }

        //    }
        //    else if (c.casestatus == cShow.showCourse("new"))//新增
        //    {
        //        c.course.FCreationUser = userID;
        //        c.course.FCreationDate = now;
        //        c.course.FSaverUser = userID;
        //        c.course.FSaverDate = now;
        //        _context.TCourseInformations.Add(c.course);
        //        _context.SaveChanges();
        //    }
        //    SavePhoto(c);
        //    return RedirectToAction("List");
        //}

        ////加入購物車
        //public IActionResult AddShoppinCat(string fEchelonId)
        //{
        //    string json = "";
        //    //string test = _context.TCourseInformations.FirstOrDefault(c => c.FEchelonId.Equals(fEchelonId)).FCourse.FCourseId;
        //    TCourseInformation course = _context.TCourseInformations.FirstOrDefault(c => c.FEchelonId.Equals(fEchelonId));

        //    if (course != null)
        //    {
        //        string echelonId = course.FEchelonId;
        //        DateTime discountDate = (DateTime)course.FDiscountDate;
        //        decimal price = (decimal)checkPrice(course.FCourse, course.FDiscountDate);
        //        string name = course.FCourse.FName;
        //        List<CShoppingCart> cart = null;

        //        if (HttpContext.Session.Keys.Contains(CDictionary.SK_COURSE_PURCHASED_LIST))
        //        {
        //            json = HttpContext.Session.GetString(CDictionary.SK_COURSE_PURCHASED_LIST);
        //            cart = JsonSerializer.Deserialize<List<CShoppingCart>>(json);
        //            checkBought(fEchelonId, echelonId, price, cart, name);

        //        }
        //        else
        //        {
        //            cart = new List<CShoppingCart>();
        //            CShoppingCart item = addBuy(echelonId, price, name);
        //            cart.Add(item);
        //        }

        //        json = JsonSerializer.Serialize(cart);
        //        HttpContext.Session.SetString(CDictionary.SK_COURSE_PURCHASED_LIST, json);
        //    }
        //    return Content("");
        //}

        ////加入商品到購物車Session(已加入過)
        //private static void checkBought(string fEchelonId, string echelonId, decimal price, List<CShoppingCart> cart, string name)
        //{
        //    for (int i = 0; i < cart.Count; i++)
        //    {
        //        if (cart[i].EchelonId.Equals(fEchelonId, StringComparison.OrdinalIgnoreCase))
        //        {
        //            cart[i].Count++;
        //            cart[i].Price = price;
        //            break;
        //        }
        //        else if (cart[i].EchelonId != fEchelonId && cart.Count == i + 1)
        //        {
        //            CShoppingCart item = addBuy(echelonId, price, name);
        //            cart.Add(item);
        //            break;
        //        }
        //    }
        //}

        ////加入商品到購物車Session(加入新商品)
        //private static CShoppingCart addBuy(string echelonId, decimal price, string name)
        //{
        //    return new CShoppingCart()
        //    {
        //        EchelonId = echelonId,
        //        Count = 1,
        //        Price = price,
        //        Name = name
        //    };
        //}

        ////購物車 確認價錢
        //private decimal? checkPrice(TCourseModel fCourse, DateTime? fDiscountDate)
        //{
        //    DateTime now = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //    if (fDiscountDate >= now)
        //        return fCourse.FSpecialOffer;
        //    else
        //        return fCourse.FOriginalPrice;
        //}
        #endregion

        //test db 要放在函式裡
        public IActionResult Test()
        {
            //CramSchoolDBContext db = new CramSchoolDBContext();
            CourseMenu1 vm = new CourseMenu1(_context);
            vm.Menu();

            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
