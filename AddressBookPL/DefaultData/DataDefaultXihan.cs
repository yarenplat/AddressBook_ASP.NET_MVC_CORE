using AddressBookBL.ImplementationsOfManagers;
using AddressBookBL.InterfacesOfManagers;
using AddressBookEL.IdentityModels;
using AddressBookEL.ViewModels;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Identity;

namespace AddressBookPL.DefaultData
{
    public class DataDefaultXihan
    {
        public void CheckAndCreateRoles(RoleManager<AppRole> roleManager)
        {
            try
            {
                //admin // customer  // misafir vb...
                string[] roles = new string[] { "Admin", "Customer", "Guest" };

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


        public void CreateAllCities(ICityManager cityManager)
        {
            try
            {
                //1) Veritabanındaki illeri listeye alalım
                //2) Excele açıp satır satır okuyup 
                //3) Olmayan ili veritabanına ekleyelim

                var cityList = cityManager.GetAll(x => !x.IsRemoved).Data;  //1)
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Excels");
                string fileName = Path.GetFileName("Cities.xlsx"); // ???
                string filePath = Path.Combine(path, fileName);

                using (var excelBook = new XLWorkbook(filePath)) //C:Users/.../wwwroot/Excels/Cities.xlsx
                {

                    var rows = excelBook.Worksheet(1).RowsUsed(); //82 satır var

                    foreach (var item in rows)
                    {
                        if (item.RowNumber() > 1) // 1. satırda başlık var
                        {
                            // satırdaki hücrelere ulaşabiliriz.

                            string? cityName = item.Cell(1).Value.ToString()?.Trim();
                            string? plateCode = item.Cell(2).Value.ToString()?.Trim();

                            //bu cityName'den listede var mı yok mu ??
                            if (cityList.Count(x => x.Name.ToLower() == cityName?.ToLower()) == 0)
                            {
                                // il yok  ekleyeklim 
                                //ÖRN: Adana yok EKLEYECEĞİZ

                                CityVM c = new CityVM()
                                {
                                    CreatedDate = DateTime.Now,
                                    Name = cityName,
                                    PlateCode = plateCode
                                };

                                cityManager.Add(c);
                            } // if bitti
                        } // if bitti
                    } // foreach bitti

                }
            }
            catch (Exception ex)
            {
                //loglanacak

            }



        }



        public void CreateAllDistricts(IDistrictManager districtManager)
        {
            try
            {
                var districts = districtManager.GetAll(x => !x.IsRemoved).Data;

                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Excels");
                string filePath = Path.Combine(path, "Districts.xlsx");

                using (var excelBook = new XLWorkbook(filePath)) //C:Users/.../wwwroot/Excels/Cities.xlsx
                {
                    var rows = excelBook.Worksheet(1).RowsUsed();

                    foreach (var item in rows)
                    {
                        if (item.RowNumber() > 1) // 1. satırda başlık var
                        {

                            string districtName = item.Cell(1).Value.ToString().Trim();
                            // Beşiktaş

                            int cityId = Convert.ToInt32(item.Cell(2).Value.ToString().Trim()); //34


                            if (districts.Count(x => x.Name.ToLower() == districtName.ToLower()
                            && x.CityId == cityId) == 0)
                            {
                                DistrictVM d = new DistrictVM()
                                {
                                    CreatedDate = DateTime.Now,
                                    Name = districtName,
                                    CityId = cityId
                                };

                                districtManager.Add(d);
                            }




                        } // if bitt

                    } // foreach bitti
                } // using bitti


            }
            catch (Exception)
            {

            }
        }



        public void CreateSomeNeighbourhood(INeighbourhoodManager neighbourhoodManager, ICityManager cityManager, IDistrictManager districtManager)

        {
            try
            {
                //return; //NOT:
                //BU METODU YAZMAK BEST PRACTICE DEĞİLDİR! 70BİN DATASI OLAN EXCELİ 
                // PROJEYİ YORMAMAK İÇİN INSERT INTO İLE EKLEMEK DAHA MANTIKLIDIR!
                //ya da Console application ile excelei okuyup ekleyebiliriz.

                //Burada sadece birkaç tane ilin mahallesini ekleyeceğiz

                var neighbours = neighbourhoodManager.GetAll(x => !x.IsRemoved).Data;

                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Excels");
                string filePath = Path.Combine(path, "NeighborhoodPostalCode (1).xlsx");

                using (var excelBook = new XLWorkbook(filePath)) //C:Users/.../wwwroot/Excels/Cities.xlsx
                {
                    var rows = excelBook.Worksheet("istanbul").RowsUsed();

                    foreach (var item in rows)
                    {
                        if (item.RowNumber() > 1) // 1. satırda başlık var
                        {
                            //il
                            string cityName = item.Cell(1).Value.ToString().Trim();
                            //ilçe
                            string districtName = item.Cell(2).Value.ToString().Trim();

                            //mahalle
                            string neighbourName = item.Cell(3).Value.ToString().Trim();

                            var city = cityManager.GetByConditions(x => x.Name.ToLower() == cityName.ToLower()).Data;

                            var district = districtManager.GetByConditions(x => x.Name.ToLower() == districtName.ToLower() && x.CityId == city.Id).Data;


                            if (neighbours.Count(x => x.Name.ToLower() == neighbourName.ToLower() && x.DistrictId == district.Id) == 0)
                            {
                                NeighbourhoodVM n = new() // c# bilmem kaçla gelmiş özellik
                                {
                                    CreatedDate = DateTime.Now,
                                    Name = neighbourName,
                                    DistrictId = district.Id,
                                    PostCode = "12345"
                                };

                                neighbourhoodManager.Add(n);
                            } //if bitti

                        } // if bitt

                    } // foreach bitti
                } // using bitti


            }
            catch (Exception ex)
            {

            }
        }

    }
}
