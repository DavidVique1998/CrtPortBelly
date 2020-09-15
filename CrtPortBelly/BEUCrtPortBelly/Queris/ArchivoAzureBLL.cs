using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BEUCrtPortBelly.Queris
{
    public class ArchivoAzureBLL
    {
        public string SubirImagen(HttpPostedFile postedFile)
        {
            string imageName = "";

            if (postedFile != null && postedFile.ContentLength > 0)
                try
                {

                    //Create custom fileName
                    imageName = new string(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
                    imageName = imageName + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(postedFile.FileName);
                    //postedFile.SaveAs(filePath);
                }
                catch (Exception)
                {
                    imageName = "";
                }
            return imageName;
        }
    }
}
