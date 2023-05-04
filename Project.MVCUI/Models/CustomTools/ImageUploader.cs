using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Project.MVCUI.Models.CustomTools
{
    public static class ImageUploader
    {
        public static string UploadImage(string serverPath, HttpPostedFileBase file, string name)
        {
            if (file!=null)
            {
                Guid uniqueName= Guid.NewGuid();
                string[] fileArrays=file.FileName.Split(','); //burada Split metodu sayesinde ilgili yapının uzantısının da iceride bulundugu bir string dizisi almıs olduk...Split metodu belirttiginiz char karakterinden metni bölerek size bir array sunar...
                string extension = fileArrays[fileArrays.Length - 1].ToLower();//Dosya uzantısını yakalarak kücük harflere cevirdik...
                string fileName = $"{uniqueName}.{name}.{extension}"; //normal şartlarda biz burada Guid kullandıgımız icin asla bir dosya ismi aynı olmayacaktır.Lakin siz Guid kullanmazsanız(sadece kullanıyıa yüklemek istedigi dosyanın ismini girdirmek isterseniz) Böyle bir durumda aynı isimde dosya upload'u mümkün hale gelecektir.Dolayısıyla öyle bir durumda ek olarak bir kontrol yapmanız gerekecektir.Tabii ki böyle bir senaryo olsun veya olmasın önce extension kontrol edilmelidir.Bahsettigimiz ek kontrol daha sonra yapılmalıdır.
                if (extension=="jpg"|| extension=="jpeg"||extension=="png"||extension=="gif")
                {
                    // eğer dosya ismi varsa
                    if (File.Exists(HttpContext.Current.Server.MapPath(serverPath+fileName)))
                    {
                        return "1";// ancak guid kullandığımız için bu açıdan zaten güvendeyiz(dosya zaten var kodu)
                    }
                    else 
                    {
                        string filePath = HttpContext.Current.Server.MapPath(serverPath+fileName);
                        file.SaveAs(filePath);
                        return $"{serverPath}{fileName}";
                    }
                }
                else 
                {
                    return "2";// seçilen dosya resim değildir kodu
                }


            }
            else
            {
                return "3"; // dosya boş kodu
            }
        
        }
        public static bool DeleteImage(string imagePath) 
        {
            if (File.Exists(HttpContext.Current.Server.MapPath(imagePath)))
            {
                File.Delete(HttpContext.Current.Server.MapPath(imagePath));
                return true;
            }
            else
            {
                return false;
            }
        
        }
    }
}