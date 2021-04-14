using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Drawing.Imaging;

namespace Drawing_Utilities
{
    public static class Image_Extensions
    {
        public static void SaveToDisk(this Bitmap image, string filePath)
        {
            ImageCodecInfo image_codec_info;
            System.Drawing.Imaging.Encoder encoder;
            EncoderParameter encoder_parameter;
            EncoderParameters encoder_parameters;

            // Get an ImageCodecInfo object that represents the JPEG codec.
            //myImageCodecInfo = GetEncoderInfo("image/jpeg");
            image_codec_info = GetEncoderInfo("image/png");

            // Create an Encoder object based on the GUID
            // for the Quality parameter category.
            encoder = System.Drawing.Imaging.Encoder.Quality;

            // Create an EncoderParameters object.
            // An EncoderParameters object has an array of EncoderParameter
            // objects. In this case, there is only one

            // EncoderParameter object in the array.
            encoder_parameters = new EncoderParameters(1);

            // Save the bitmap as a JPEG file with quality level 25.
            encoder_parameter = new EncoderParameter(encoder, 100L);
            encoder_parameters.Param[0] = encoder_parameter;

            image.Save(filePath, image_codec_info, encoder_parameters);
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
    }
}
