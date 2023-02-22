using System;

namespace MirzaevLibrary.AppDataFolder.ClassFolder
{
    public partial class UserClass
    {
        public static int PersonalNumberUser { get; set; }
        public static string SurnameUser { get; set; }
        public static string NameUser { get; set; }
        public static string MiddlenameUser { get; set; }
        public static int pnTicketUser { get; set; }
        public static string AddressUser { get; set; }
        public static string PhoneUser { get; set; }
        public static string LoginUser { get; set; }
        public static string PasswordUser { get; set; }
        public static int pnRoleUser { get; set; }
        public static int pnImageUser { get; set; }

        public static void SetUser(int GetPersonalNumberUser, string GetSurnameUser, string GetNameUser, string GetMiddlenameUser, int GetpnTicketUser, string GetAddresUser, string GetPhoneUser, string GetLoginUser, string GetPasswordUser, int GetpnRoleUser, int GetpnImageUser)
        {
            PersonalNumberUser = GetPersonalNumberUser;
            SurnameUser= GetSurnameUser;
            NameUser= GetNameUser;
            MiddlenameUser= GetMiddlenameUser;
            pnTicketUser= GetpnTicketUser;
            AddressUser= GetAddresUser;
            PhoneUser= GetPhoneUser;
            LoginUser= GetLoginUser;
            PasswordUser= GetPasswordUser;
            pnRoleUser= GetpnRoleUser;
            pnImageUser= GetpnImageUser;
        }
    }
}
