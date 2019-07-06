using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Security.Cryptography;
using System.Text;
using System.Net.Http.Headers;

namespace WebApi.Models
{
    public class AuthFilterAttribute : AuthorizationFilterAttribute
    {
        private string key = ":!123@321008";
        private bool verifyResult = false;

        public static string GetMd5_32byteUF8(string str)
        {
            string pwd = string.Empty;

            //实例化一个md5对像
            MD5 md5 = MD5.Create();

            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(str));

            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将每个字节转成十六进制后再转成字符串使用。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                string temp = s[i].ToString("x");
                if (temp.Length < 2)
                {
                    temp = "0" + temp;
                }
                pwd = pwd + temp;
            }

            return pwd;
        }


        public static string GetMd5_32byteUnicode(string str)
        {
            string pwd = string.Empty;

            //实例化一个md5对像
            MD5 md5 = MD5.Create();

            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.Unicode.GetBytes(str));

            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将每个字节转成十六进制后再转成字符串使用。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                string temp = s[i].ToString("x");
                if (temp.Length < 2)
                {
                    temp = "0" + temp;
                }
                pwd = pwd + temp;
            }

            return pwd;
        }

        public static string GetMd5_32byteANSI(string str)
        {
            string pwd = string.Empty;

            //实例化一个md5对像
            MD5 md5 = MD5.Create();

            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.Default.GetBytes(str));

            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将每个字节转成十六进制后再转成字符串使用。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                string temp = s[i].ToString("x");
                if (temp.Length < 2)
                {
                    temp = "0" + temp;
                }
                pwd = pwd + temp;
            }

            return pwd;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //如果Action带有AllowAnonymousAttribute，则不进行授权验证
            if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
            {
                return;
            }

            HttpRequestHeaders Headers= actionContext.Request.Headers;            
            if (Headers!= null)
            {
                IEnumerable<string> users = null;
                IEnumerable<string> tokens = null;
                if (Headers.TryGetValues("user", out users) && Headers.TryGetValues("token", out tokens))
                {
                    string key2 = key + users.ElementAt(0);
                    string key3 = GetMd5_32byteANSI(key2);
                    if (tokens.ElementAt(0) == key3)
                    {
                        verifyResult = true;
                    }
                    else
                    {
                        verifyResult = false;
                    }
                }
            }
            if (!verifyResult)
            {
                //如果验证不通过，则返回401错误，并且Body中写入错误原因
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Token 不正确");
            }
        }
    }
}