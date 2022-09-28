using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace prjCramSchoolSystemAdmin.Models
{
    [ModelMetadataTypeAttribute(typeof(TCourseInformationMD))]
    public partial class TCourseInformation
    {
        public class TCourseInformationMD
        {
            [DisplayName("班別id")]
            public string FEchelonId { get; set; }
            [DisplayName("班別名稱")]
            //[Remote("CheckName", "Course",AdditionalFields = "FEchelonId", ErrorMessage = "課程名稱已經存在！請重新輸入！！！")]
            public string FName { get; set; }
            [DisplayName("課程模板")]
            public string FCourseId { get; set; }
            [DisplayName("開班日期(啟)")]
            [DataType(DataType.Date)]
            public DateTime? FStartTime { get; set; }
            [DataType(DataType.Date)]
            [DisplayName("開班日期(迄)")]
            public DateTime? FEndTime { get; set; }
            [DisplayName("上課時間")]
            public string FTime { get; set; }
            [DisplayName("開班人數")]
            public int? FBasicPeople { get; set; }
            [DisplayName("額滿人數")]
            public int? FLimitPeople { get; set; }
            [DisplayName("課程狀態")]
            public int? FClassState { get; set; }
            [DisplayName("教室代號")]
            public string FClassRoom { get; set; }
            [DisplayName("課程老師")]
            public string FTeacher { get; set; }
            [DataType(DataType.Date)]
            [DisplayName("優惠價期限")]
            public DateTime? FDiscountDate { get; set; }
            public string FCreationUser { get; set; }
            public DateTime? FCreationDate { get; set; }
            public string FSaverUser { get; set; }
            public DateTime? FSaverDate { get; set; }
        }
    }

}
