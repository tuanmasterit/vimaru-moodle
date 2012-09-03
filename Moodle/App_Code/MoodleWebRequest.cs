using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace Moodle
{
    public class MoodleWebRequest
    {
        private WebRequest request;
        private Stream dataStream;
        private string status;

        // Thuộc tính tình trạng
        public String Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }

        // Gán yêu cầu với chuỗi truy vấn
        public MoodleWebRequest(string url)
        {
            request = WebRequest.Create(url);
        }

        // Gán yêu cầu với chuỗi truy vấn và phương thức
        public MoodleWebRequest(string url, string method)
            : this(url)
        {
            if (method.Equals("GET") || method.Equals("POST"))
            {
                request.Method = method;
            }
            else
            {
                throw new Exception("Invalid Method Type");
            }
        }

        // Gán yêu cầu với chuỗi truy vấn, phương thức và dữ liệu
        public MoodleWebRequest(string url, string method, string data)
            : this(url, method)
        {
            // Tạo dữ liệu POST và chuyển đổi nó thành mảng byte
            string postData = data;
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            // Đặt thuộc tính loại nội dung của WebRequest
            request.ContentType = "application/x-www-form-urlencoded";

            // Đặt thuộc tính độ dài nội dung của the WebRequest
            request.ContentLength = byteArray.Length;

            // Gán luồng yêu cầu cho dataStream
            dataStream = request.GetRequestStream();

            // Ghi dữ liệu tới luồng dataStream
            dataStream.Write(byteArray, 0, byteArray.Length);

            // Đóng luồng dữ liệu
            dataStream.Close();

        }

        public string GetResponse()
        {
            WebResponse response;
            try
            {
                // Lấy dữ liệu trả về của yêu cầu
                response = request.GetResponse();
            }
            catch //(System.Exception ex)
            {
                // Không kết nối được đến host
                return "x";
            }

            // Lấy tình trạng trả về .
            this.Status = ((HttpWebResponse)response).StatusDescription;

            // Lấy luồng dữ liệu trả về từ server được yêu cầu
            dataStream = response.GetResponseStream();

            // Mở một luồng sử dụng StreamReader để đọc dữ liệu trả về
            StreamReader reader = new StreamReader(dataStream);

            // Đọc toàn bộ nội dung trả về
            string responseFromServer = reader.ReadToEnd();

            // Đóng tất cả các luồng
            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }
    }

}
