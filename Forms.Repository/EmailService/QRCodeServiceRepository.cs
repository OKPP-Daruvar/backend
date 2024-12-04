using QRCoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using ZXing.QrCode.Internal;
using System.Web;


namespace Forms.Repository.EmailService
{
    public class QRCodeServiceRepository : IQRCodeServiceRepository
    {
        public byte[] GenerateQRCode(string surveyLink)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(surveyLink, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode pngByteQRCode = new PngByteQRCode(qrCodeData);

            return pngByteQRCode.GetGraphic(20);
        }
    }
}
