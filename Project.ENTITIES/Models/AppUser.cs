using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class AppUser:BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid ActivationCode { get; set; }//Yeni üye olacaklara üyelik onayı göndereceğimiz unique code'e şifrelemek için kullanıyoruz
        public bool Active { get; set; }
        public string Email { get; set; }
        public UserRole UserRole { get; set; }
        public AppUser()
        {
            UserRole = UserRole.Member;
            ActivationCode=Guid.NewGuid();
        }
        //Relational properties

        public virtual AppUserProfile AppUserProfile { get; set; }

    }
}
