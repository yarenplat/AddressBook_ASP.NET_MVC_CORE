using AddressBookEL.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AddressBookPL.DefaultData
{
    public static class DataDefault
    {
        public static IApplicationBuilder Data(this IApplicationBuilder app) // extension metot
        {
            //Manager'ları kullanabilmek için EXTENTION metot oluşturduk

            //Bir metod EXTENTION metot ise parametresinde THIS kelimesi vardır

            using var scopedServices = app.ApplicationServices.CreateScope();

            var serviceProvider = scopedServices.ServiceProvider;

            var roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            //city managerları da ilerleyen günlerde üreteceğiz illeri eklemek için


            CheckAndCreateRoles(roleManager); // roleManager oluştu ve şimdi rolleri oluşturan metodu çağırabiliriz
            return app;
        }

        public static void CheckAndCreateRoles(RoleManager<AppRole> roleManager)
        {
            try
            {
                //admin // customer  // misafir vb...
                string[] roles = new string[] { "admin", "customer", "guest" };

                // rolleri tek tek dönüp sisteme olup olmadığına bakacağız. Yoksa ekleyeceğiz.
                foreach (var item in roles)
                {
                    // ROLDEN YOK MU? 
                    if (!roleManager.RoleExistsAsync(item.ToLower()).Result)
                    {
                        //rolden yokmuş ekleyelim
                        AppRole role = new AppRole()
                        {
                            Name = item
                        };

                        var result = roleManager.CreateAsync(role).Result;
                    }
                }



            }
            catch (Exception ex)
            {
                // ex loglanabilşr
                // yazılımcıya acil başlıklı email gönderilebilir
            }
        }


        public static string AsalDurum(this int x)
        {
            return "evet asal";
        }

        public static bool AsalMidir(this int number)
        {
            //işlemler
            for (int i = number; i > 1; i--)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public static IApplicationBuilder Betul(this IApplicationBuilder app)
        {

            return app;
        }


    }
}
