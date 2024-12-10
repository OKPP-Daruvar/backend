using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forms.Repository.EmailService
{
    public interface IQRCodeServiceRepository
    {
        public byte[] GenerateQRCode(string surveyLink);

    }
}
