using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integrador.Pix.Santander.Utils;

namespace Integrador.Pix.Santander.Extensions
{
    public static class PayloadExtensions
    {
        private static string GetMerchantAccountInformation(string payload) =>
            (GetMerchantAccountInformationGui().Length + GetMerchantAccountInformationPayloadLocation(payload).Length + 8).ToString();

        private static string GetMerchantAccountInformationGui() => "br.gov.bcb.pix";
        private static string GetMerchantAccountInformationPayloadLocation(string payload) => payload;
        private static string GetMerchantCategory() => "0000";
        private static string GetTransactionCurrency() => "986";
        private static string GetCountryCode() => "BR";

        public static string ConvertPayloadInStringQRCode(this string valor, string merchantCity = "VILA VELHA", string merchantName = "PIX")
        {
            if (string.IsNullOrEmpty(valor))
                return string.Empty;

            var payload = PayloadFormatIndicatorID +

                           MerchantAccountInformationID +
                           GetMerchantAccountInformation(valor) +

                           MerchantAccountInformationGuiID +
                           GetMerchantAccountInformationGui().ObterCampoFormatado() +

                           MerchantAccountInformationPayloadLocationID +
                           GetMerchantAccountInformationPayloadLocation(valor).ObterCampoFormatado() +

                           MerchantCategoryID +
                           GetMerchantCategory().ObterCampoFormatado() +

                           TransactionCurrencyID +
                           GetTransactionCurrency().ObterCampoFormatado() +

                           CountryCodeID +
                           GetCountryCode().ObterCampoFormatado() +

                           MerchantNameID +
                           merchantName.ObterCampoFormatado(25, limparCampos: true) +

                           MerchantCityID +
                           merchantCity.ObterCampoFormatado(15, limparCampos: true) +

                           AdditionalDataFieldTemplateID +
                           ReferenceLabelID +
                           CRC16ID;

            payload += Crc16Ccitt.ConvertString(payload);
            return payload;
        }

        public static string GerarBase64QRCode(this string valor, int width = 200, int height = 200)
        {
            var bw = new ZXing.BarcodeWriter();
            var encOptions = new ZXing.Common.EncodingOptions() { Width = width, Height = height, Margin = 0 };
            bw.Options = encOptions;
            bw.Format = ZXing.BarcodeFormat.QR_CODE;

            using (Bitmap bi = new Bitmap(bw.Write(valor)))
            using (MemoryStream ms = new MemoryStream())
            {
                bi.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] byteImage = ms.ToArray();
                return $"data:image/png;base64,{Convert.ToBase64String(byteImage)}";
            }
        }

        private const string PayloadFormatIndicatorID = "000201";
        private const string MerchantAccountInformationID = "26";
        private const string MerchantAccountInformationGuiID = "00";
        private const string MerchantAccountInformationPayloadLocationID = "25";
        private const string MerchantCategoryID = "52";
        private const string TransactionCurrencyID = "53";
        private const string CountryCodeID = "58";
        private const string MerchantNameID = "59";
        private const string MerchantCityID = "60";
        private const string AdditionalDataFieldTemplateID = "6207";
        private const string ReferenceLabelID = "0503***";
        private const string CRC16ID = "6304";
    }
}
