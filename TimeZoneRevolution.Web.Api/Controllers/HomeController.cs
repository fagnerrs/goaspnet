using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace TimeZoneRevolution.Web.Api.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        //public IHttpActionResult Get()
        //{

        //    var serverPath = Path.Combine("c:\\temp\\help.pdf");
        //    var fileInfo = new FileInfo(serverPath);

        //    if (!fileInfo.Exists)
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        return new FileResult(fileInfo.FullName);
        //    }
        //}

        [System.Web.Http.HttpGet]
        public HttpResponseMessage Get()
        {
            //var path = System.Web.HttpContext.Current.Server.MapPath(""); ;


            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new FileStream("c:\\temp\\help.pdf", FileMode.Open);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            result.Content.Headers.ContentDisposition.FileName = "help.pdf";// Path.GetFileName(path);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            result.Content.Headers.ContentLength = stream.Length;
            return result;
        }

        public HttpResponseMessage Test()
        {
            var stream = new MemoryStream();

            using (var stream1 = new FileStream("c:\\temp\\help.pdf", FileMode.Open))
            {
                stream1.CopyTo(stream);
            }


            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(stream.ToArray())
            };
            result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = "help.pdf"
            };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");


            return result;
        }



        public class FileResult : IHttpActionResult
        {
            private readonly string _filePath;
            private readonly string _contentType;

            public FileResult(string filePath, string contentType = null)
            {
                if (filePath == null) throw new ArgumentNullException("filePath");

                _filePath = filePath;
                _contentType = contentType;
            }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StreamContent(System.IO.File.OpenRead(_filePath))
                };

                var contentType = _contentType ?? MimeMapping.GetMimeMapping(Path.GetExtension(_filePath));
                response.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);

                return Task.FromResult(response);
            }
        }
    }
}
