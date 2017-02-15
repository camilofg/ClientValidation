using System.IO;
using System.Web;
using ZXing;
using ZXing.Common;
using System.Drawing.Imaging;
using ZXing.QrCode.Internal;
using ZXing.Rendering;
using System.Drawing;
using System;

namespace ClientValidation
{
    /// <summary>
    /// Summary description for QRHandler
    /// </summary>
    public class QRHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string urlToQRify = context.Server.UrlDecode(context.Request.QueryString["u"]);
            var tales = GenerateQR(250, 250, urlToQRify);
            context.Response.ContentType = "image/png";
            tales.Save(context.Response.OutputStream, ImageFormat.Png);
        }

        public Bitmap GenerateQR(int width, int height, string text)
        {
            var bw = new ZXing.BarcodeWriter();

            var encOptions = new ZXing.Common.EncodingOptions
            {
                Width = width,
                Height = height,
                Margin = 0,
                PureBarcode = false
            };
            try
            {
                encOptions.Hints.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);

                bw.Renderer = new BitmapRenderer();
                bw.Options = encOptions;
                bw.Format = ZXing.BarcodeFormat.QR_CODE;
                Bitmap bm = bw.Write(text);
                Bitmap overlay = new Bitmap(HttpContext.Current.Server.MapPath(@"~\Images\ArtesaniasColombia.jpg"));//@"C:\Users\camus\documents\visual studio 2015\Projects\ClientValidation\ClientValidation\Images\logo6.png");

                int deltaHeigth = bm.Height - overlay.Height;
                int deltaWidth = bm.Width - overlay.Width;

                Graphics g = Graphics.FromImage(bm);
                g.DrawImage(overlay, new Point(deltaWidth / 2, deltaHeigth / 2));

                return bm;
            }
            catch (Exception ex) {
                throw ex;
            }
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}