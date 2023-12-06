using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.Common;

namespace ELibra.Classes
{
    /// <summary>
    ///  Шифрование и дешифрование QR-кода 
    /// </summary>
    static class QrHelper
    {
        /// <summary>
        ///  Преобразует строку в QR-код
        /// </summary>
        public static Bitmap encode(string plane_text)
        {
            BarcodeWriter barcodeWriter = new BarcodeWriter();
            barcodeWriter.Format = BarcodeFormat.QR_CODE;
            barcodeWriter.Options.Height = 720;
            barcodeWriter.Options.Width = 720;
            Bitmap result = barcodeWriter.Write(plane_text);

            return result;
        }

        /// <summary>
        ///  Преобразует QR-код в строку
        /// </summary>
        public static string decode(Bitmap qr_code)
        {
            BarcodeReader barcodeReader = new BarcodeReader();
            Result result = barcodeReader.Decode(qr_code);

            return result.Text;
        }

    }
}
