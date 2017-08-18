using System;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.Mvc;
using ZXing;
using ZXing.Common;

namespace control_portal_empleos_v2.Helpers
{
    public static class qrHelper
    {
        public static IHtmlString GenerateQrCode(this HtmlHelper html, string url, string alt = "QR code", int height = 150, int width = 150, int margin = 0)
        {
            var qrWriter = new BarcodeWriter();
            qrWriter.Format = BarcodeFormat.QR_CODE;
            qrWriter.Options = new EncodingOptions() { Height = height, Width = width, Margin = margin };

            using (var q = qrWriter.Write(url))
            {
                using (var ms = new MemoryStream())
                {
                    q.Save(ms, ImageFormat.Png);
                    var img = new TagBuilder("img");
                    img.Attributes.Add("src", String.Format("data:image/png;base64,{0}", Convert.ToBase64String(ms.ToArray())));
                    img.Attributes.Add("alt", alt);
                    return MvcHtmlString.Create(img.ToString(TagRenderMode.SelfClosing));
                }
            }
        }
    }
}