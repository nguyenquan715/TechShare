using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechShare.Models;

namespace TechShare.Infra
{
    sealed public class Utility
    {
        /*Định dạng dữ liệu có dạng "<Id>1</Id><Id>2</Id>*/
        public string[] FormatDataRows(string data, string tagStart, string tagEnd)
        {
            string[] res = data.Split(tagStart);
            string temp = string.Join("", res);
            res = temp.Split(tagEnd);
            res = res.Take(res.Length - 1).ToArray();
            return res;
        }

        /*Hiển thị dữ liệu dưới dạng 1 | 2 | 3*/
        public string DisplayCategories(string[] data)
        {
            return string.Join(" | ", data);
        }

        /*Định dạng ngày tháng năm*/
        public DateTime ConvertStringToDatetime(string data)
        {
            if (data.Length < 19||data==null) return new DateTime();
            int day = int.Parse(data.Substring(3, 2));
            int month = int.Parse(data.Substring(0, 2));
            int year = int.Parse(data.Substring(6, 4));
            int hour = int.Parse(data.Substring(11, 2));
            int minute = int.Parse(data.Substring(14, 2));
            int second = int.Parse(data.Substring(17, 2));
            DateTime time = new DateTime(year, month, day, hour, minute, second);
            return time;
        }

        /*Binding dữ liệu vào đối tượng PostViewModel*/
        public PostViewModel BindingDataIntoPostModel(dynamic res)
        {
            string[] data = FormatDataRows((string)res.CategoriesName, "<Name>", "</Name>");
            PostViewModel obj = new PostViewModel()
            {
                Id = new Guid((string)res.Id),
                Title = (string)res.Title,
                Content = (string)res.Content,
                CreatedAt = ConvertStringToDatetime((string)res.CreatedAt),
                SubmitedAt = ConvertStringToDatetime((string)res.SubmitedAt),
                UpdatedAt = ConvertStringToDatetime((string)res.UpdatedAt),
                PublishedAt = ConvertStringToDatetime((string)res.PublishedAt),
                Status = (int)res.Status,
                UserId = (string)res.UserId,
                Email = (string)res.Email,
                FirstName = (string)res.FirstName,
                LastName = (string)res.LastName,
                UserName = (string)res.UserName,
                Avatar = (string)res.Avatar,
                CategoriesId = FormatDataRows((string)res.CategoriesId, "<Id>", "</Id>"),
                CategoriesName = data,
                DisplayCategories = DisplayCategories(data)
            };
            return obj;
        }
    }
}
